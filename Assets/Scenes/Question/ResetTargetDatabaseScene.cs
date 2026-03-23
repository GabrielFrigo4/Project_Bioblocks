using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;

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

        Dictionary<string, string> databankNameMap = new Dictionary<string, string>()
        {
            {"AcidBaseBufferQuestionDatabase", "Ácidos, bases e tampões"},
            {"AminoacidQuestionDatabase", "Aminoácidos e peptídeos"},
            {"BiochemistryIntroductionQuestionDatabase", "Introdução à Bioquímica"},
            {"CarbohydratesQuestionDatabase", "Carboidratos"},
            {"EnzymeQuestionDatabase", "Enzimas"},
            {"LipidsQuestionDatabase", "Lipídeos"},
            {"MembranesQuestionDatabase", "Mambranas Biológicas"},
            {"NucleicAcidsQuestionDatabase", "Ácidos nucleicos"},
            {"ProteinQuestionDatabase", "Proteínas"},
            {"WaterQuestionDatabase", "Água"}
        };

        string displayName;
        bool found = databankNameMap.TryGetValue(databankName, out displayName);

        if (found)
        {
            databankNameText.text = $"Tópico: {displayName}";
        }
        else
        {
            databankNameText.text = $"Tópico: {databankName}";
        }
    }

    public async void ResetAnsweredQuestions()
    {
        try
        {
            if (resetButton != null) resetButton.interactable = false;
            if (resetButtonText != null) resetButtonText.text = "Resetando...";

            string userId = currentUserData.UserId;
            Debug.Log($"Resetando questões respondidas - UserId: {userId}, databankName: {databankName}");

            await _firestore.ResetAnsweredQuestions(userId, databankName);
            Debug.Log("Questões resetadas com sucesso");
            await _firestore.UpdateUserField(userId, $"ResetDatabankFlags.{databankName}", true);
            UserDataStore.MarkDatabankAsReset(databankName, true);
            
            if (PlayerLevelManager.Instance != null)
            {
                await PlayerLevelManager.Instance.RecalculateTotalAnswered();
            }

            Debug.Log($"[ResetDatabase] Banco {databankName} marcado como resetado. Level recalculado.");

            if (resetButtonText != null) resetButtonText.text = "Sucesso!";

            AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(userId, databankName, 0);
            UpdateUIAfterReset(databankName);
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