using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;
using UnityEngine;

public class DatabaseStatisticsManager : MonoBehaviour, IStatisticsProvider
{
    private static DatabaseStatisticsManager instance;
    private bool isInitialized = false;
    private bool isInitializing = false;

    // Evento para notificar quando as estatísticas estiverem prontas
    public delegate void StatisticsReadyHandler();
    public static event StatisticsReadyHandler OnStatisticsReady;

    public static DatabaseStatisticsManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = GameObject.Find("FirebaseManager");
                if (go == null)
                {
                    go = new GameObject("FirebaseManager");
                }
                
                instance = go.GetComponent<DatabaseStatisticsManager>();
                if (instance == null)
                {
                    instance = go.AddComponent<DatabaseStatisticsManager>();
                }
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsInitialized => isInitialized;

    public async Task Initialize()
    {
        if (isInitialized || isInitializing) return;
        
        isInitializing = true;
        Debug.Log("Inicializando DatabaseStatisticsManager...");

        try
        {
            // Espera um frame para garantir que outros managers foram inicializados
            await Task.Yield();

            // Carrega estatísticas de todos os bancos de dados
            await LoadAllDatabaseStatistics();

            isInitialized = true;
            isInitializing = false;
            
            // Notifica que as estatísticas estão prontas
            OnStatisticsReady?.Invoke();
            
            Debug.Log("DatabaseStatisticsManager inicializado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro na inicialização do DatabaseStatisticsManager: {e.Message}");
            isInitializing = false;
            isInitialized = false;
        }
    }

    private async Task LoadAllDatabaseStatistics()
    {
        string[] allDatabases = new string[]
        {
            "AcidBaseBufferQuestionDatabase",
            "AminoacidQuestionDatabase",
            "BiochemistryIntroductionQuestionDatabase",
            "CarbohydratesQuestionDatabase",
            "EnzymeQuestionDatabase",
            "LipidsQuestionDatabase",
            "MembranesQuestionDatabase",
            "NucleicAcidsQuestionDatabase",
            "ProteinQuestionDatabase",
            "WaterQuestionDatabase"
        };

        Dictionary<string, int> questionCounts = new Dictionary<string, int>();

        foreach (string databankName in allDatabases)
        {
            try
            {
                Type databaseType = Type.GetType(databankName);
                if (databaseType == null)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        databaseType = assembly.GetType(databankName);
                        if (databaseType != null) break;
                    }
                }

                if (databaseType != null)
                {
                    GameObject tempGO = new GameObject($"Temp_{databankName}");
                    var database = tempGO.AddComponent(databaseType) as IQuestionDatabase;

                    if (database != null)
                    {
                        var questions = QuestionFilterService.FilterQuestions(database);
                        int count = questions != null ? questions.Count : 0;

                        QuestionBankStatistics.SetTotalQuestions(databankName, count);
                        questionCounts[databankName] = count;

                        Debug.Log($"Carregado banco {databankName}: {count} questões");
                    }

                    Destroy(tempGO);
                }
                else
                {
                    Debug.LogWarning($"Não foi possível encontrar o tipo para {databankName}");
                    QuestionBankStatistics.SetTotalQuestions(databankName, 50);
                    questionCounts[databankName] = 50;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Erro ao carregar estatísticas para {databankName}: {e.Message}");
                QuestionBankStatistics.SetTotalQuestions(databankName, 50);
                questionCounts[databankName] = 50;
            }
        }

        await Task.Delay(100);

        int totalQuestions = 0;
        foreach (var kvp in questionCounts)
        {
            totalQuestions += kvp.Value;
            Debug.Log($"Estatísticas: {kvp.Key} tem {kvp.Value} questões");
        }

        Debug.Log($"[DatabaseStatisticsManager] Total geral: {totalQuestions} questões");

        if (totalQuestions > 0 && UserDataStore.CurrentUserData != null)
        {
            UserDataStore.UpdateTotalQuestionsInAllDatabanks(totalQuestions);
            
            await AppContext.Firestore.UpdateUserField(
                UserDataStore.CurrentUserData.UserId,
                "TotalQuestionsInAllDatabanks",
                totalQuestions
            );
            
            Debug.Log($"[DatabaseStatisticsManager] Total salvo no UserData e Firebase");
        }
    }
    
    public int GetTotalQuestionsCount()
    {
        string[] allDatabases = new string[]
        {
            "AcidBaseBufferQuestionDatabase",
            "AminoacidQuestionDatabase",
            "BiochemistryIntroductionQuestionDatabase",
            "CarbohydratesQuestionDatabase",
            "EnzymeQuestionDatabase",
            "LipidsQuestionDatabase",
            "MembranesQuestionDatabase",
            "NucleicAcidsQuestionDatabase",
            "ProteinQuestionDatabase",
            "WaterQuestionDatabase"
        };

        int total = 0;
        foreach (string databankName in allDatabases)
        {
            total += QuestionBankStatistics.GetTotalQuestions(databankName);
        }

        return total;
    }
}