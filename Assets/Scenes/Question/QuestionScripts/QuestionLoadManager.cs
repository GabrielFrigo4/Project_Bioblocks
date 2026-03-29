using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;
using System;
using System.Linq;
 
public class QuestionLoadManager : MonoBehaviour
{
    private List<Question> questions;
    public string databankName;
    private bool isInitialized = false;
    public string DatabankName => databankName;
 
    private async void Start()
    {
        await Initialize();
    }
 
    private async Task Initialize()
    {
        if (isInitialized) return;
 
        try
        {
            await WaitForAnsweredQuestionsManager();
            isInitialized = true;
            Debug.Log("[QuestionLoadManager] Inicializado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLoadManager] Erro ao inicializar: {e.Message}");
        }
    }
 
    private async Task WaitForAnsweredQuestionsManager()
    {
        if (AppContext.AnsweredQuestions == null)
            throw new Exception("[QuestionLoadManager] AnsweredQuestionsManager não registrado no AppContext.");
 
        int maxAttempts = 10;
        int currentAttempt = 0;
 
        while (!AppContext.AnsweredQuestions.IsManagerInitialized && currentAttempt < maxAttempts)
        {
            Debug.Log($"[QuestionLoadManager] Aguardando AnsweredQuestionsManager... tentativa {currentAttempt + 1}/{maxAttempts}");
            await Task.Delay(500);
            currentAttempt++;
        }
 
        if (!AppContext.AnsweredQuestions.IsManagerInitialized)
            throw new Exception("[QuestionLoadManager] AnsweredQuestionsManager não inicializou a tempo.");
 
        Debug.Log("[QuestionLoadManager] AnsweredQuestionsManager pronto.");
    }
 
    public async Task<List<Question>> LoadQuestionsForSet(QuestionSet targetSet)
    {
        try
        {
            if (!isInitialized)
                await Initialize();
 
            IQuestionDatabase database = FindQuestionDatabase(targetSet);
 
            if (database == null)
            {
                Debug.LogError($"[QuestionLoadManager] ❌ Nenhum database encontrado para: {targetSet}");
                return new List<Question>();
            }
 
            return await LoadQuestionsFromDatabase(database);
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLoadManager] ❌ Erro em LoadQuestionsForSet: {e.Message}\n{e.StackTrace}");
            return new List<Question>();
        }
    }
 
    private IQuestionDatabase FindQuestionDatabase(QuestionSet targetSet)
    {
        try
        {
            MonoBehaviour[] allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
 
            foreach (MonoBehaviour behaviour in allBehaviours)
            {
                if (behaviour is IQuestionDatabase database)
                {
                    if (database.GetQuestionSetType() == targetSet)
                        return database;
                }
            }
 
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLoadManager] Erro ao procurar database: {e.Message}");
            return null;
        }
    }
 
    private async Task<List<Question>> LoadQuestionsFromDatabase(IQuestionDatabase database)
    {
        if (database == null)
        {
            Debug.LogError("[QuestionLoadManager] Database é null");
            return new List<Question>();
        }
 
        try
        {
            if (!AppContext.AnsweredQuestions.IsManagerInitialized)
            {
                Debug.LogError("[QuestionLoadManager] AnsweredQuestionsManager não está inicializado");
                return new List<Question>();
            }
 
            List<Question> allQuestions = QuestionFilterService.FilterQuestions(database);
 
            if (allQuestions == null || allQuestions.Count == 0)
            {
                Debug.LogError("[QuestionLoadManager] ❌ Database retornou lista nula ou vazia");
                return new List<Question>();
            }
 
            Debug.Log($"\n📚 PASSO 1: BANCO LOCAL");
            Debug.Log($"  Total de questões: {allQuestions.Count}");
 
            if (string.IsNullOrEmpty(databankName))
            {
                databankName = database.GetDatabankName();
                Debug.Log($"  Nome do banco: {databankName}");
            }
 
            int totalQuestions = allQuestions.Count;
            QuestionBankStatistics.SetTotalQuestions(databankName, totalQuestions);
 
            var questionsByLevel = GetQuestionCountByLevel(allQuestions);
            QuestionBankStatistics.SetQuestionsPerLevel(databankName, questionsByLevel);
 
            foreach (var kvp in questionsByLevel.OrderBy(x => x.Key))
                Debug.Log($"    Nível {kvp.Key}: {kvp.Value} questões");
 
            string userId = UserDataStore.CurrentUserData?.UserId;
 
            if (string.IsNullOrEmpty(userId))
            {
                Debug.LogWarning("[QuestionLoadManager] ⚠️ UserId não disponível, carregando apenas questões de nível 1");
                allQuestions = allQuestions.Where(q => GetQuestionLevel(q) == 1).ToList();
                questions = allQuestions;
                return questions;
            }
 
            // Busca questões respondidas diretamente pelo AppContext
            string dbName = database.GetDatabankName();
            List<string> answeredQuestionsFromFirebase = await AppContext.AnsweredQuestions
                .FetchUserAnsweredQuestionsInTargetDatabase(dbName);
 
            Debug.Log($"\n🔥 PASSO 2: FIREBASE (AnsweredQuestions)");
            Debug.Log($"  Questões respondidas corretamente: {answeredQuestionsFromFirebase.Count}");
 
            if (answeredQuestionsFromFirebase.Count > 0 && answeredQuestionsFromFirebase.Count <= 20)
                Debug.Log($"  IDs: [{string.Join(", ", answeredQuestionsFromFirebase)}]");
 
            Debug.Log($"\n🔢 PASSO 3: CÁLCULO DO NÍVEL ATUAL");
 
            int currentLevel = LevelCalculator.CalculateCurrentLevel(
                allQuestions,
                answeredQuestionsFromFirebase
            );
 
            HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase);
 
            List<Question> questionsNotAnswered = allQuestions
                .Where(q => !answeredSet.Contains(q.questionNumber.ToString()))
                .ToList();
 
            Debug.Log($"\n🗑️ PASSO 4: REMOVER QUESTÕES RESPONDIDAS");
            Debug.Log($"  Questões restantes: {questionsNotAnswered.Count}");
 
            List<Question> questionsForCurrentLevel = questionsNotAnswered
                .Where(q => GetQuestionLevel(q) == currentLevel)
                .ToList();
 
            Debug.Log($"\n✅ PASSO 5: FILTRAR POR NÍVEL {currentLevel}");
            Debug.Log($"  Questões disponíveis: {questionsForCurrentLevel.Count}");
 
            if (questionsForCurrentLevel.Count > 0)
            {
                var questionNumbers = questionsForCurrentLevel
                    .Select(q => q.questionNumber)
                    .OrderBy(n => n)
                    .ToList();
 
                if (questionNumbers.Count <= 20)
                    Debug.Log($"  IDs: [{string.Join(", ", questionNumbers)}]");
                else
                    Debug.Log($"  IDs: [{string.Join(", ", questionNumbers.Take(10))}... +{questionNumbers.Count - 10} mais]");
            }
            else
            {
                Debug.Log($"  ⚠️ NENHUMA questão disponível no nível {currentLevel}!");
 
                var stats = LevelCalculator.GetLevelStats(allQuestions, answeredQuestionsFromFirebase);
                Debug.Log($"\n📊 ESTATÍSTICAS:");
                foreach (var stat in stats.Values.OrderBy(s => s.Level))
                    Debug.Log($"  {stat}");
            }
 
            Debug.Log($"╚══════════════════════════════════════════════════════╝\n");
 
            questions = questionsForCurrentLevel;
            return questions;
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLoadManager] ❌ Erro em LoadQuestionsFromDatabase: {e.Message}\n{e.StackTrace}");
            return new List<Question>();
        }
    }
 
    private int GetQuestionLevel(Question question)
    {
        return question.questionLevel <= 0 ? 1 : question.questionLevel;
    }
 
    private Dictionary<int, int> GetQuestionCountByLevel(List<Question> allQuestions)
    {
        var stats = new Dictionary<int, int>();
 
        if (allQuestions == null || allQuestions.Count == 0)
            return stats;
 
        foreach (var question in allQuestions)
        {
            int level = GetQuestionLevel(question);
            if (!stats.ContainsKey(level))
                stats[level] = 0;
            stats[level]++;
        }
 
        return stats;
    }
}