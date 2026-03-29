using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Gerencia as questões respondidas corretamente pelo usuário.
/// </summary>
public class AnsweredQuestionsManager : MonoBehaviour, IAnsweredQuestionsManager
{
  public delegate void AnsweredQuestionsUpdatedHandler(Dictionary<string, int> answeredCounts);
    public static event AnsweredQuestionsUpdatedHandler OnAnsweredQuestionsUpdated;

    // -------------------------------------------------------
    // Dependências — obtidas do AppContext no Start()
    // -------------------------------------------------------
    private IFirestoreRepository _firestore;
    private IAuthRepository _auth;

    private string userId;
    private bool isInitialized = false;

    // -------------------------------------------------------
    // Ciclo de vida
    // -------------------------------------------------------

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        _firestore = AppContext.Firestore;
        _auth      = AppContext.Auth;

        await Initialize();
    }


    // -------------------------------------------------------
    // Inicialização
    // -------------------------------------------------------

    private async Task Initialize()
    {
        if (isInitialized) return;
        try
        {
            await Task.Yield();

            if (!_firestore.IsInitialized)
            {
                Debug.LogError("[AnsweredQuestionsManager] FirestoreRepository não está inicializado");
                return;
            }

            if (!_auth.IsUserLoggedIn())
            {
                Debug.LogError("[AnsweredQuestionsManager] Nenhum usuário está autenticado");
                return;
            }

            userId = _auth.CurrentUserId;
            Debug.Log($"[AnsweredQuestionsManager] Inicializando para usuário: {userId}");

            _firestore.ListenToUserData(
                userId,
                onScoreChanged: null,
                onWeekScoreChanged: null,
                onAnsweredQuestionsChanged: HandleAnsweredQuestionsUpdate
            );

            await FetchUserAnsweredQuestions();
            isInitialized = true;
            Debug.Log("[AnsweredQuestionsManager] Inicializado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro na inicialização: {e.Message}");
            isInitialized = false;
        }
    }

    // -------------------------------------------------------
    // Listener de atualizações em tempo real
    // -------------------------------------------------------

    private void HandleAnsweredQuestionsUpdate(Dictionary<string, List<int>> answeredQuestions)
    {
        try
        {
            if (answeredQuestions == null) return;

            Dictionary<string, int> answeredCounts = new Dictionary<string, int>();

            foreach (var kvp in answeredQuestions)
            {
                string databankName = kvp.Key;
                List<int> questionsList = kvp.Value;

                if (questionsList == null) continue;

                var distinctQuestions = questionsList.Distinct().ToList();
                int totalQuestions = QuestionBankStatistics.GetTotalQuestions(databankName);
                if (totalQuestions <= 0) totalQuestions = 50;

                int count = Mathf.Min(distinctQuestions.Count, totalQuestions);
                answeredCounts[databankName] = count;
                AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(userId, databankName, count);

                Debug.Log($"[AnsweredQuestionsManager] Listener atualizou {databankName}: {count} questões");
            }

            if (answeredCounts.Count > 0)
                OnAnsweredQuestionsUpdated?.Invoke(answeredCounts);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro ao processar atualização: {ex.Message}");
        }
    }

    // -------------------------------------------------------
    // Busca de dados
    // -------------------------------------------------------

    private async Task FetchUserAnsweredQuestions()
    {
        try
        {
            UserData userData = await _firestore.GetUserData(userId);

            if (userData?.AnsweredQuestions == null) return;

            Dictionary<string, int> answeredCounts = new Dictionary<string, int>();

            foreach (var kvp in userData.AnsweredQuestions)
            {
                string databankName = kvp.Key;
                List<int> questionsList = kvp.Value;

                if (questionsList == null) continue;

                var distinctQuestions = questionsList.Distinct().ToList();

                if (distinctQuestions.Count != questionsList.Count)
                    Debug.LogWarning($"[AnsweredQuestionsManager] {questionsList.Count - distinctQuestions.Count} questões duplicadas em {databankName}");

                int totalQuestions = QuestionBankStatistics.GetTotalQuestions(databankName);
                int count = Mathf.Min(distinctQuestions.Count, totalQuestions);

                answeredCounts[databankName] = count;
                AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(userId, databankName, count);
                Debug.Log($"[AnsweredQuestionsManager] {databankName}: {count} questões respondidas");
            }

            Debug.Log($"[AnsweredQuestionsManager] Disparando OnAnsweredQuestionsUpdated com {answeredCounts.Count} bancos");
            OnAnsweredQuestionsUpdated?.Invoke(answeredCounts);
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro ao buscar dados: {e.Message}");
            throw;
        }
    }

    public async Task<List<string>> FetchUserAnsweredQuestionsInTargetDatabase(string target)
    {
        List<string> answeredQuestions = new List<string>();

        try
        {
            if (!isInitialized)
            {
                await Initialize();
                if (!isInitialized)
                {
                    Debug.LogError("[AnsweredQuestionsManager] Falha ao inicializar");
                    return answeredQuestions;
                }
            }

            UserData userData = await _firestore.GetUserData(userId);

            if (userData?.AnsweredQuestions != null &&
                userData.AnsweredQuestions.ContainsKey(target))
            {
                answeredQuestions = userData.AnsweredQuestions[target]
                    .Select(q => q.ToString())
                    .ToList();

                Debug.Log($"[AnsweredQuestionsManager] {answeredQuestions.Count} questões respondidas em {target}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro ao buscar questões de {target}: {e.Message}");
        }

        return answeredQuestions;
    }

    // -------------------------------------------------------
    // Marcar questão como respondida corretamente
    // -------------------------------------------------------

    public async Task MarkQuestionAsAnswered(string databankName, int questionNumber)
    {
        try
        {
            if (!isInitialized)
            {
                await Initialize();
                if (!isInitialized)
                {
                    Debug.LogError("[AnsweredQuestionsManager] Falha ao inicializar");
                    return;
                }
            }

            if (string.IsNullOrEmpty(userId))
            {
                Debug.LogError("[AnsweredQuestionsManager] Usuário não autenticado");
                return;
            }

            UserData userData = await _firestore.GetUserData(userId);
            if (userData == null)
            {
                Debug.LogError("[AnsweredQuestionsManager] Dados do usuário não encontrados");
                return;
            }

            bool alreadyAnswered = userData.AnsweredQuestions != null &&
                                   userData.AnsweredQuestions.ContainsKey(databankName) &&
                                   userData.AnsweredQuestions[databankName].Contains(questionNumber);

            if (!alreadyAnswered)
            {
                await _firestore.UpdateUserScore(userId, userData.Score, questionNumber, databankName, true);
                await ForceUpdate();
                Debug.Log($"[AnsweredQuestionsManager] Questão {questionNumber} marcada em {databankName}");
            }
            else
            {
                Debug.Log($"[AnsweredQuestionsManager] Questão {questionNumber} já estava marcada em {databankName}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro ao marcar questão: {ex.Message}");
        }
    }

    // -------------------------------------------------------
    // Força atualização
    // -------------------------------------------------------

    public async Task ForceUpdate()
    {
        try
        {
            Debug.Log("[AnsweredQuestionsManager] ForceUpdate iniciado");

            if (!isInitialized)
            {
                await Initialize();
                if (!isInitialized)
                {
                    Debug.LogError("[AnsweredQuestionsManager] Falha ao inicializar durante ForceUpdate");
                    return;
                }
            }

            await FetchUserAnsweredQuestions();

            if (userId != null)
            {
                var userCounts = AnsweredQuestionsListStore.GetAnsweredQuestionsCountForUser(userId);
                OnAnsweredQuestionsUpdated?.Invoke(userCounts);
            }

            Debug.Log("[AnsweredQuestionsManager] ForceUpdate concluído");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro em ForceUpdate: {e.Message}");
        }
    }

    // -------------------------------------------------------
    // Questões restantes
    // -------------------------------------------------------

    public async Task<bool> HasRemainingQuestions(string currentDatabase, List<string> currentQuestionList)
    {
        try
        {
            if (!isInitialized)
            {
                await Initialize();
                if (!isInitialized)
                {
                    Debug.LogError("[AnsweredQuestionsManager] Falha ao inicializar");
                    return false;
                }
            }

            List<string> answeredQuestions = await FetchUserAnsweredQuestionsInTargetDatabase(currentDatabase);
            bool hasRemaining = currentQuestionList.Except(answeredQuestions).Any();

            Debug.Log($"[AnsweredQuestionsManager] {currentDatabase}: " +
                      $"Total={currentQuestionList.Count}, " +
                      $"Respondidas={answeredQuestions.Count}, " +
                      $"Restantes={hasRemaining}");

            return hasRemaining;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnsweredQuestionsManager] Erro ao verificar questões restantes: {e.Message}");
            return false;
        }
    }

    // -------------------------------------------------------
    // Reset
    // -------------------------------------------------------

    public void ResetManager()
    {
        isInitialized = false;
        userId = null;
    }

    public bool IsManagerInitialized => isInitialized;
}