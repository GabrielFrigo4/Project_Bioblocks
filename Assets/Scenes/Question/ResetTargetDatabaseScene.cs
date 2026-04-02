using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;
using System.Linq;

public class ResetTargetDatabaseScene : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI resetButtonText;

    public TextMeshProUGUI databankNameText;
    private string databankName;
    private UserData currentUserData;
    private INavigationService _navigation;
    private ISceneDataService _sceneData;
    private IFirestoreRepository _firestore;

    private void Start()
    {
        _navigation = AppContext.Navigation;
        _sceneData  = AppContext.SceneData;
        _firestore  = AppContext.Firestore;

        var sceneData = _sceneData.GetData();
        if (sceneData != null && sceneData.TryGetValue("databankName", out object value))
        {
            databankName = value as string;
            Debug.Log($"databankName recebido do SceneDataManager: {databankName}");

            if (!string.IsNullOrEmpty(databankName))
            {
                bool isDatabaseInDevelopment = false;
                if (sceneData.TryGetValue("isDatabaseInDevelopment", out object devModeValue))
                {
                    isDatabaseInDevelopment = (bool)devModeValue;
                }

                UpdateDatabankNameText();
                currentUserData = UserDataStore.CurrentUserData;
                _sceneData.ClearData();

                if (isDatabaseInDevelopment)
                {
                    ShowDevModeMessage();
                }
            }
            else
            {
                Debug.LogError("databankName está vazio mesmo após conversão");
                return;
            }
        }
        else
        {
            Debug.LogError("Nenhum databankName encontrado nos dados da cena");
            return;
        }
    }

    private void UpdateDatabankNameText()
    {
        if (databankNameText == null)
        {
            Debug.LogError("databankNameText não está referenciado no Inspector");
            return;
        }

        if (string.IsNullOrEmpty(databankName))
        {
            Debug.LogError("databankName está vazio ou nulo");
            return;
        }

        IQuestionDatabase database = FindDatabaseByName(databankName);                                                                                              
        string displayName = database != null
        ? database.GetDisplayName()
        : databankName; // fallback para o nome técnico

        databankNameText.text = $"Tópico: {displayName}";
    }

    private IQuestionDatabase FindDatabaseByName(string name)
    {
        // Busca entre todos os bancos registrados na cena
        var databases = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IQuestionDatabase>();

        foreach (var db in databases)
        {
            if (db.GetDatabankName() == name)
                return db;
        }

        return null;
    }

    public async void ResetAnsweredQuestions()
    {
        try
        {
            if (resetButton != null) resetButton.interactable = false;
            if (resetButtonText != null) resetButtonText.text = "Resetando...";

            string userId = currentUserData.UserId;
            await _firestore.ResetAnsweredQuestions(userId, databankName);
            await _firestore.UpdateUserField(userId, $"ResetDatabankFlags.{databankName}", true);
            UserDataStore.MarkDatabankAsReset(databankName, true);
            AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(userId, databankName, 0);

            if (resetButtonText != null) resetButtonText.text = "Sucesso!";
            UpdateUIAfterReset(databankName);

            try
            {
              if (AppContext.PlayerLevel != null)
                await AppContext.PlayerLevel.RecalculateTotalAnswered();   
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[ResetDatabase] Erro ao recalcular level (não crítico): {e.Message}");
            }

            await Task.Delay(500);
            NavigateToPathway();
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao resetar questões: {e.Message}");

            if (resetButton != null)
            {
                resetButton.interactable = true;
                resetButtonText.text = "Tentar Novamente";
            }
        }
    }

    private void UpdateUIAfterReset(string databaseNameToReset)
    {
        string objectName = $"{databaseNameToReset}PorcentageText";
        GameObject textObject = GameObject.Find(objectName);

        if (textObject != null)
        {
            TextMeshProUGUI tmpText = textObject.GetComponent<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = "0%";
                Debug.Log($"{databankName}PorcentageText reset to 0%");
            }
        }
    }

    public void NavigateToPathway()
    {
        Debug.Log("Navegando para PathwayScene");
        _navigation.NavigateTo("PathwayScene");
    }

    private void ShowDevModeMessage()
    {
        if (databankNameText != null)
        {
            databankNameText.text = "Modo de desenvolvimento. A funcionalidade está indisponível.";
        }
        
        if (resetButton != null)
        {
            resetButton.interactable = false;
        }
        
        if (resetButtonText != null)
        {
            resetButtonText.text = "Indisponível";
        }
        
        Debug.LogWarning($"[ResetDatabase] Banco '{databankName}' está em modo desenvolvimento - Reset bloqueado");
    }
}