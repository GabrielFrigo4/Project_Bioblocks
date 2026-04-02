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
    private IPlayerLevelService _playerLevel;

    private void Start()
    {
        _auth      = AppContext.Auth;
        _firestore = AppContext.Firestore;
        _userHeaderManager = FindFirstObjectByType<UserHeaderManager>();
        _playerLevel = AppContext.PlayerLevel;
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
        {           if (!_auth.IsUserLoggedIn())
            {
                Debug.LogError("Usuário não autenticado");
                return;
            }

            string userId = _auth.CurrentUserId;
            UserData userData = UserDataStore.CurrentUserData;

            if (userData == null)
            {
                Debug.LogError("CurrentUserData é null");
                return;
            }

            int actualScoreChange = scoreChange;

            if (isCorrect && _userHeaderManager != null && _userHeaderManager.IsAnyBonusActive())
                actualScoreChange = _userHeaderManager.ApplyTotalBonus(scoreChange);
            else if (isCorrect && questionBonusManager != null && questionBonusManager.IsBonusActive())
                actualScoreChange = questionBonusManager.ApplyBonusToScore(scoreChange);

            actualScoreChange = ClampScoreChange(userData.Score, actualScoreChange);

            if (isCorrect)
            {
                string databankName  = answeredQuestion.questionDatabankName;
                int    questionNumber = answeredQuestion.questionNumber;

                try
                {
                    if (database != null && database.IsDatabaseInDevelopment())
                    {
                        await AppContext.AnsweredQuestions.MarkQuestionAsAnswered(databankName, questionNumber);
                    }
                    else
                    {
                        await _firestore.UpdateUserScores(userId, actualScoreChange, questionNumber, databankName, true);

                        if (answeredQuestionsManager != null && answeredQuestionsManager.IsManagerInitialized)
                            await answeredQuestionsManager.ForceUpdate();

                        bool isDatabankReset = UserDataStore.IsDatabankReset(databankName);

                        if (_playerLevel != null)
                        {
                            await _playerLevel.IncrementTotalAnswered();
                            await _playerLevel.CheckAndHandleLevelUp();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar scores: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    if (database == null || !database.IsDatabaseInDevelopment())
                        await _firestore.UpdateUserScores(userId, actualScoreChange, 0, "", false);
                    else
                        Debug.Log("[QuestionScoreManager] Modo DEV - Score negativo NÃO salvo no Firebase");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar score negativo: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar score: {ex.Message}\n{ex.StackTrace}");

            if (UserDataStore.CurrentUserData != null && scoreChange != 0)
            {
                int clientSideScore     = Mathf.Max(0, UserDataStore.CurrentUserData.Score + scoreChange);
                int clientSideWeekScore = Mathf.Max(0, UserDataStore.CurrentUserData.WeekScore + scoreChange);

                UserDataStore.CurrentUserData.Score     = clientSideScore;
                UserDataStore.CurrentUserData.WeekScore = clientSideWeekScore;
                UserDataStore.CurrentUserData = UserDataStore.CurrentUserData;
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