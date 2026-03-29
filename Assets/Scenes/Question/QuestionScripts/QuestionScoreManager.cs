using System.Threading.Tasks;
using UnityEngine;
using System;
using QuestionSystem;

public class QuestionScoreManager : MonoBehaviour
{
    private UserData currentUserData;
    private AnsweredQuestionsManager answeredQuestionsManager;
    private QuestionBonusManager questionBonusManager;
    private IAuthRepository _auth;
    private IFirestoreRepository _firestore;
    private UserHeaderManager _userHeaderManager;

    private void Start()
    {
        _auth      = AppContext.Auth;
        _firestore = AppContext.Firestore;
        _userHeaderManager = FindFirstObjectByType<UserHeaderManager>();
        currentUserData = UserDataStore.CurrentUserData;
        questionBonusManager = FindFirstObjectByType<QuestionBonusManager>();

        if (currentUserData == null)
        {
            Debug.LogError("CurrentUserData é null no ScoreManager");
        }

        if (questionBonusManager == null)
        {
            Debug.LogWarning("QuestionBonusManager não encontrado. O sistema de bônus não estará disponível.");
        }
    }

    
    public async Task UpdateScore(int scoreChange, bool isCorrect, Question answeredQuestion, IQuestionDatabase database = null)
    {
        try
        {
            if (!_auth.IsUserLoggedIn())
            {
                Debug.LogError("Usuário não autenticado");
                return;
            }
    
            string userId = _auth.CurrentUserId;
            UserData userData = await _firestore.GetUserData(userId);
    
            if (userData == null)
            {
                Debug.LogError("Dados do usuário não encontrados");
                return;
            }
    
            int actualScoreChange = scoreChange;
    
            if (isCorrect && _userHeaderManager != null && _userHeaderManager.IsAnyBonusActive())
            {
                actualScoreChange = _userHeaderManager.ApplyTotalBonus(scoreChange);
            }
            else if (isCorrect && questionBonusManager != null && questionBonusManager.IsBonusActive())
            {
                actualScoreChange = questionBonusManager.ApplyBonusToScore(scoreChange);
            }

            actualScoreChange = ClampScoreChange(userData.Score, actualScoreChange);
    
            if (isCorrect)
            {
                string databankName = answeredQuestion.questionDatabankName;
                int questionNumber = answeredQuestion.questionNumber;
    
                try
                {
                    if (database != null && database.IsDatabaseInDevelopment())
                    {
                        await AppContext.AnsweredQuestions.MarkQuestionAsAnswered(
                            answeredQuestion.questionDatabankName, 
                            questionNumber
                        );
                    }
                    else
                    {
                        await _firestore.UpdateUserScores(
                            userId,
                            actualScoreChange,
                            questionNumber,
                            databankName,
                            true
                        );
    
                        if (answeredQuestionsManager != null && answeredQuestionsManager.IsManagerInitialized)
                        {
                            await answeredQuestionsManager.ForceUpdate();
                        }
    
                        bool isDatabankReset = UserDataStore.IsDatabankReset(databankName);
    
                        if (!isDatabankReset && PlayerLevelManager.Instance != null)
                        {
                            await PlayerLevelManager.Instance.IncrementTotalAnswered();
                            await PlayerLevelManager.Instance.CheckAndHandleLevelUp();
                        }
                        else if (isDatabankReset)
                        {
                            Debug.Log($"[QuestionScoreManager] Banco {databankName} foi resetado. Questão não conta para level.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar scores e marcar questão como respondida: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    if (database == null || !database.IsDatabaseInDevelopment())
                    {
                        await _firestore.UpdateUserScores(
                            userId,
                            actualScoreChange,
                            0,
                            "",
                            false
                        );
                    }
                    else
                    {
                        Debug.Log($"[QuestionScoreManager] Modo DEV - Score negativo NÃO salvo no Firebase");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar o score no Firestore: {ex.Message}");
                }
            }
    
            if (database == null || !database.IsDatabaseInDevelopment())
            {
                UserData updatedUserData = await _firestore.GetUserData(userId);
    
                if (updatedUserData != null)
                {
                    UserDataStore.CurrentUserData = updatedUserData;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar score: {ex.Message}\n{ex.StackTrace}");
    
            if (currentUserData != null && scoreChange != 0)
            {
                int clientSideScore = Mathf.Max(0, currentUserData.Score + scoreChange);
                int clientSideWeekScore = Mathf.Max(0, currentUserData.WeekScore + scoreChange);
    
                currentUserData.Score = clientSideScore;
                currentUserData.WeekScore = clientSideWeekScore;
                UserDataStore.CurrentUserData = currentUserData;
            }
        }
    }
    
    private void OnEnable()
    {
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
    }

    private void OnDisable()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
    }

    private void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
    }

    public bool HasBonusActive()
    {
        if (_userHeaderManager != null && _userHeaderManager.IsAnyBonusActive())
        {
            return true;
        }

        return questionBonusManager != null && questionBonusManager.IsBonusActive();
    }

    public int CalculateBonusScore(int baseScore)
    {
       if (_userHeaderManager != null && _userHeaderManager.IsAnyBonusActive())
        {
            return _userHeaderManager.ApplyTotalBonus(baseScore);
        }

        if (questionBonusManager != null && questionBonusManager.IsBonusActive())
        {
            return questionBonusManager.ApplyBonusToScore(baseScore);
        }

        return baseScore;
    }
   
    private int ClampScoreChange(int currentScore, int scoreChange)
    {
        int finalScore = currentScore + scoreChange;
        if (finalScore < 0)
            return -currentScore;

        return scoreChange;
    }
   
}   