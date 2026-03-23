using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using QuestionSystem;

public class QuestionBonusManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QuestionScoreManager scoreManager;
    [SerializeField] private QuestionAnswerManager answerManager;
    [SerializeField] private QuestionCanvasGroupManager canvasGroupManager;
    [SerializeField] private QuestionBottomUIManager questionBottomUIManager;

    [Header("UI Components")]
    [SerializeField] private QuestionBonusUIFeedback questionBonusUIFeedback;

    [Header("Bonus Configuration")]
    [SerializeField] private int consecutiveCorrectAnswersNeeded = 5;
    [SerializeField] private float bonusDuration = 600f;

    private int combinedMultiplier = 1;
    private int consecutiveCorrectAnswers = 0;
    private bool isBonusActive = false;
    private float currentBonusTime = 0f;
    private QuestionSceneBonusManager bonusManager;
    private UserHeaderManager _userHeaderManager;

    private void Start()
    {
        bonusManager = new QuestionSceneBonusManager();
        _userHeaderManager = FindFirstObjectByType<UserHeaderManager>();

        if (!ValidateComponents())
        {
            Debug.LogError("QuestionBonusManager: Falha na validação dos componentes necessários.");
            return;
        }

        if (answerManager != null)
        {
            answerManager.OnAnswerSelected += CheckAnswer;
        }

        if (questionBottomUIManager != null)
        {
            questionBottomUIManager.OnExitButtonClicked += HideBonusFeedback;
            questionBottomUIManager.OnNextButtonClicked += HideBonusFeedback;
        }
        else
        {
            Debug.LogWarning("QuestionBonusManager: BottomUIManager não encontrado. O feedback de bônus não será escondido ao navegar.");
        }

        QuestionManager questionManager = FindFirstObjectByType<QuestionManager>();
        if (questionManager == null)
        {
            Debug.LogWarning("QuestionManager não encontrado");
        }

        // Inscreve-se no evento do UserTopBarManager se existir
        if (_userHeaderManager != null)
        {
            _userHeaderManager.OnBonusMultiplierUpdated += OnBonusMultiplierUpdated;
            Debug.Log("QuestionBonusManager: Inscrito no UserTopBarManager");
        }
        else
        {
            Debug.LogWarning("UserTopBarManager.Instance não encontrado. A visualização de bônus pode não funcionar corretamente.");
        }

        StartCoroutine(InitCheckForBonus());
    }

    private bool ValidateComponents()
    {
        if (questionBonusUIFeedback == null)
        {
            questionBonusUIFeedback = FindFirstObjectByType<QuestionBonusUIFeedback>();
            if (questionBonusUIFeedback == null)
            {
                Debug.LogError("QuestionBonusManager: QuestionBonusUIFeedback não encontrado. Por favor, adicione-o à cena.");
                Debug.LogWarning("QuestionBonusManager: Você pode criar um GameObject com o script QuestionBonusUIFeedback.");
                return false;
            }
        }

        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<QuestionScoreManager>();
            if (scoreManager == null)
            {
                Debug.LogError("QuestionBonusManager: QuestionScoreManager não encontrado.");
                return false;
            }
        }

        if (answerManager == null)
        {
            answerManager = FindFirstObjectByType<QuestionAnswerManager>();
            if (answerManager == null)
            {
                Debug.LogError("QuestionBonusManager: QuestionAnswerManager não encontrado.");
                return false;
            }
        }

        if (canvasGroupManager == null)
        {
            canvasGroupManager = FindFirstObjectByType<QuestionCanvasGroupManager>();
            if (canvasGroupManager == null)
            {
                Debug.LogWarning("QuestionBonusManager: QuestionCanvasGroupManager não encontrado. Feedback de bônus pode não ser exibido corretamente.");
            }
        }

        if (questionBottomUIManager == null)
        {
            questionBottomUIManager = FindFirstObjectByType<QuestionBottomUIManager>();
            if (questionBottomUIManager == null)
            {
                Debug.LogWarning("QuestionBonusManager: BottomUIManager não encontrado. O feedback de bônus não será escondido automaticamente ao navegar.");
            }
        }

        CanvasGroup feedbackCanvasGroup = questionBonusUIFeedback.GetComponent<CanvasGroup>();
        if (feedbackCanvasGroup == null)
        {
            Debug.LogWarning("QuestionBonusUIFeedback não tem um componente CanvasGroup. Adicionando automaticamente.");
            feedbackCanvasGroup = questionBonusUIFeedback.gameObject.AddComponent<CanvasGroup>();
        }

        return true;
    }

    private IEnumerator InitCheckForBonus()
    {
        yield return new WaitForSeconds(0.5f);

        if (UserDataStore.CurrentUserData == null || string.IsNullOrEmpty(UserDataStore.CurrentUserData.UserId))
        {
            yield break;
        }

        string userId = UserDataStore.CurrentUserData.UserId;
        CheckForActiveBonuses(userId);
    }

    private async void CheckForActiveBonuses(string userId)
    {
        try
        {
            bool hasBonus = await bonusManager.HasAnyActiveBonus(userId);

            if (hasBonus)
            {
                List<Dictionary<string, object>> activeBonuses = await bonusManager.GetActiveBonuses(userId);

                if (activeBonuses.Count > 0)
                {
                    combinedMultiplier = await bonusManager.GetCombinedMultiplier(userId);
                    float remainingTime = await bonusManager.GetRemainingTime(userId);

                    if (remainingTime > 0)
                    {
                        isBonusActive = true;
                        
                        // Atualiza o UserTopBarManager se existir
                        if (_userHeaderManager != null)
                        {
                            _userHeaderManager.RefreshActiveBonuses();
                        }
                    }
                    else
                    {
                        isBonusActive = false;
                    }
                }
                else
                {
                    isBonusActive = false;
                }
            }
            else
            {
                isBonusActive = false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"QuestionBonusManager: Erro ao verificar bônus: {e.Message}");
        }
    }

    private void OnBonusMultiplierUpdated(int newMultiplier)
    {
        combinedMultiplier = newMultiplier;
        Debug.Log($"QuestionBonusManager: Multiplicador atualizado para {newMultiplier}");
    }

    private async void ActivateBonus()
    {
        if (UserDataStore.CurrentUserData == null || string.IsNullOrEmpty(UserDataStore.CurrentUserData.UserId))
        {
            Debug.LogError("QuestionBonusManager: Usuário não está logado");
            return;
        }

        string userId = UserDataStore.CurrentUserData.UserId;

        try
        {
            // Ativa o bônus no Firestore
            await bonusManager.ActivateBonus(userId, "correctAnswerBonus", bonusDuration, 2);
            await bonusManager.IncrementBonusCounter(userId, "correctAnswerBonusCounter");
            isBonusActive = true;
            currentBonusTime = bonusDuration;

            // Mostra feedback visual
            if (canvasGroupManager != null)
            {
                canvasGroupManager.ShowBonusFeedback(true);

                if (questionBonusUIFeedback != null)
                {
                    questionBonusUIFeedback.ShowBonusActivatedFeedback();
                }
            }
            else
            {
                if (questionBonusUIFeedback != null)
                {
                    questionBonusUIFeedback.gameObject.SetActive(true);
                    questionBonusUIFeedback.ShowBonusActivatedFeedback();
                }
                else
                {
                    Debug.LogError("questionBonusUIFeedback é null no momento de ativar!");
                }
            }

            // Notifica o UserTopBarManager para atualizar o timer na TopBar
            if (_userHeaderManager != null)
            {
                _userHeaderManager.RefreshActiveBonuses();
                Debug.Log("QuestionBonusManager: UserTopBarManager notificado sobre novo bônus");
            }
            else
            {
                Debug.LogWarning("QuestionBonusManager: UserTopBarManager.Instance não encontrado para atualizar timer");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"QuestionBonusManager: Erro ao ativar bônus: {e.Message}");
        }
    }

    private void HideBonusFeedback()
    {
        if (questionBonusUIFeedback != null && questionBonusUIFeedback.IsVisible())
        {
            if (canvasGroupManager != null)
            {
                canvasGroupManager.ShowBonusFeedback(false);
            }
            else
            {
                questionBonusUIFeedback.ForceVisibility(false);
            }
        }
    }

    private void OnDestroy()
    {
        if (answerManager != null)
        {
            answerManager.OnAnswerSelected -= CheckAnswer;
        }

        if (questionBottomUIManager != null)
        {
            questionBottomUIManager.OnExitButtonClicked -= HideBonusFeedback;
            questionBottomUIManager.OnNextButtonClicked -= HideBonusFeedback;
        }

        // Desinscreve-se do evento do UserTopBarManager
        if (_userHeaderManager != null)
        {
            _userHeaderManager.OnBonusMultiplierUpdated -= OnBonusMultiplierUpdated;
        }
    }

    public async Task<bool> CheckIfUserHasActiveBonus(string bonusType)
    {
        if (UserDataStore.CurrentUserData == null || string.IsNullOrEmpty(UserDataStore.CurrentUserData.UserId))
        {
            return false;
        }

        string userId = UserDataStore.CurrentUserData.UserId;

        try
        {
            return await bonusManager.IsBonusActive(userId, bonusType);
        }
        catch (Exception e)
        {
            Debug.LogError($"QuestionBonusManager: Erro ao verificar bônus ativo: {e.Message}");
            return false;
        }
    }

    public bool IsBonusActive()
    {
        // Verifica tanto no estado local quanto no UserTopBarManager
        if (_userHeaderManager != null && _userHeaderManager.IsAnyBonusActive())
        {
            return true;
        }
        
        return isBonusActive;
    }

    public int GetCurrentScoreMultiplier()
    {
        // Prioriza o multiplicador do UserTopBarManager (fonte da verdade)
        if (_userHeaderManager != null)
        {
            return _userHeaderManager.GetTotalMultiplier();
        }
        
        return isBonusActive ? combinedMultiplier : 1;
    }

    public int ApplyBonusToScore(int baseScore)
    {
        // Usa o UserTopBarManager para aplicar o bônus (fonte da verdade)
        if (_userHeaderManager != null)
        {
            return _userHeaderManager.ApplyTotalBonus(baseScore);
        }

        // Fallback para lógica local caso UserTopBarManager não exista
        if (isBonusActive && baseScore > 0)
        {
            return baseScore * combinedMultiplier;
        }
        return baseScore;
    }

    private void CheckAnswer(int selectedAnswerIndex)
    {
        QuestionManager questionManager = FindFirstObjectByType<QuestionManager>();
        if (questionManager == null)
        {
            Debug.LogError("QuestionBonusManager: QuestionManager não encontrado.");
            return;
        }

        var currentSessionField = typeof(QuestionManager).GetField("currentSession", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (currentSessionField == null)
        {
            Debug.LogError("QuestionBonusManager: Campo 'currentSession' não encontrado em QuestionManager.");
            return;
        }

        var currentSession = currentSessionField.GetValue(questionManager);
        if (currentSession == null)
        {
            Debug.LogError("QuestionBonusManager: currentSession é null.");
            return;
        }

        var getCurrentQuestionMethod = currentSession.GetType().GetMethod("GetCurrentQuestion");
        if (getCurrentQuestionMethod == null)
        {
            Debug.LogError("QuestionBonusManager: Método 'GetCurrentQuestion' não encontrado.");
            return;
        }

        var currentQuestion = getCurrentQuestionMethod.Invoke(currentSession, null) as Question;
        if (currentQuestion == null)
        {
            Debug.LogError("QuestionBonusManager: currentQuestion é null.");
            return;
        }

        bool isCorrect = selectedAnswerIndex == currentQuestion.correctIndex;

        if (isCorrect)
        {
            consecutiveCorrectAnswers++;
            Debug.Log($"QuestionBonusManager: Respostas consecutivas corretas: {consecutiveCorrectAnswers}/{consecutiveCorrectAnswersNeeded}");

            if (consecutiveCorrectAnswers >= consecutiveCorrectAnswersNeeded && !isBonusActive)
            {
                Debug.Log("QuestionBonusManager: Ativando bônus por respostas consecutivas!");
                ActivateBonus();
            }
        }
        else
        {
            if (consecutiveCorrectAnswers > 0)
            {
                Debug.Log("QuestionBonusManager: Resposta incorreta. Resetando contador de consecutivas.");
            }
            consecutiveCorrectAnswers = 0;
        }
    }
}