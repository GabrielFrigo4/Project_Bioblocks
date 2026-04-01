// Assets/Script/Core/Player/PlayerLevelService.cs

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlayerLevelService : MonoBehaviour, IPlayerLevelService
{
    public event Action<int, int> OnLevelChanged;
    public event Action<int> OnLevelProgressUpdated;   private IFirestoreRepository _firestore;
    private IStatisticsProvider _statistics;
    private UserData _currentUserData;
    private bool _isInitialized = false;

    // -------------------------------------------------------
    // Ciclo de vida Unity
    // -------------------------------------------------------
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("[PlayerLevelService] Start() chamado");

        _firestore  = AppContext.Firestore;
        _statistics = AppContext.Statistics;

        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        _currentUserData = UserDataStore.CurrentUserData;

        if (_currentUserData == null)
            Debug.LogWarning("[PlayerLevelService] CurrentUserData é null no Start(). Aguardando OnUserDataChanged...");
        else
        {
            Debug.Log($"[PlayerLevelService] CurrentUserData encontrado: {_currentUserData.UserId}, Level: {_currentUserData.PlayerLevel}");
            PerformMigrationIfNeeded();
        }

        _isInitialized = true;
        Debug.Log("[PlayerLevelService] Inicialização completa");
    }

    private void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
    }

    // -------------------------------------------------------
    // IPlayerLevelService — carregamento de dados
    // -------------------------------------------------------

    public void OnUserDataLoaded(UserData userData)
    {
        Debug.Log($"[PlayerLevelService] OnUserDataLoaded. UserId: {userData?.UserId}, Level: {userData?.PlayerLevel}");

        _currentUserData = userData;

        if (_currentUserData != null && _isInitialized)
            PerformMigrationIfNeeded();
    }

    private void OnUserDataChanged(UserData userData)
    {
        Debug.Log($"[PlayerLevelService] OnUserDataChanged. UserId: {userData?.UserId}, Level: {userData?.PlayerLevel}");

        bool wasNull = _currentUserData == null;
        _currentUserData = userData;

        if (wasNull && _currentUserData != null && _isInitialized)
        {
            Debug.Log("[PlayerLevelService] Dados carregados pela primeira vez. Verificando migração...");
            PerformMigrationIfNeeded();
        }
    }

    // -------------------------------------------------------
    // Migração de dados legados
    // -------------------------------------------------------

    private async void PerformMigrationIfNeeded()
    {
        Debug.Log("[PlayerLevelService] PerformMigrationIfNeeded() INICIADO");

        if (_currentUserData == null)
        {
            Debug.LogWarning("[PlayerLevelService] CurrentUserData é null. Abortando migração.");
            return;
        }

        Debug.Log($"[PlayerLevelService] PlayerLevel atual: {_currentUserData.PlayerLevel}");

        if (_currentUserData.PlayerLevel <= 1 && _currentUserData.TotalValidQuestionsAnswered == 0)
        {
            Debug.Log("[PlayerLevelService] PlayerLevel = 0. Iniciando migração...");
            try
            {
                if (_currentUserData.ResetDatabankFlags == null)
                    _currentUserData.ResetDatabankFlags = new Dictionary<string, bool>();

                int totalAnswered = await CalculateValidAnsweredQuestions(_currentUserData.UserId);
                _currentUserData.TotalValidQuestionsAnswered = totalAnswered;

                int totalQuestions  = GetTotalQuestionsCount();
                int calculatedLevel = PlayerLevelConfig.CalculateLevel(totalAnswered, totalQuestions);
                _currentUserData.PlayerLevel = calculatedLevel;

                await _firestore.UpdateUserField(_currentUserData.UserId, "PlayerLevel", calculatedLevel);
                await _firestore.UpdateUserField(_currentUserData.UserId, "TotalValidQuestionsAnswered", totalAnswered);

                UserDataStore.CurrentUserData = _currentUserData;
                Debug.Log($"[PlayerLevelService] Migração concluída. Level: {calculatedLevel}, Questões: {totalAnswered}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerLevelService] Erro na migração: {e.Message}\n{e.StackTrace}");
                _currentUserData.PlayerLevel = 1;
                _currentUserData.TotalValidQuestionsAnswered = 0;
            }
        }
        else
        {
            Debug.Log($"[PlayerLevelService] PlayerLevel já definido ({_currentUserData.PlayerLevel}). Migração não necessária.");
        }
    }

    // -------------------------------------------------------
    // IPlayerLevelService — progressão
    // -------------------------------------------------------

    public async Task IncrementTotalAnswered()
    {
        if (!_isInitialized || _currentUserData == null) return;

        _currentUserData.TotalValidQuestionsAnswered++;

        await _firestore.UpdateUserField(
            _currentUserData.UserId,
            "TotalValidQuestionsAnswered",
            _currentUserData.TotalValidQuestionsAnswered
        );

        UserDataStore.UpdateTotalValidQuestionsAnswered(_currentUserData.TotalValidQuestionsAnswered);
        OnLevelProgressUpdated?.Invoke(_currentUserData.TotalValidQuestionsAnswered);

        Debug.Log($"[PlayerLevelService] Total válido: {_currentUserData.TotalValidQuestionsAnswered}");
    }

    public async Task CheckAndHandleLevelUp()
    {
        if (!_isInitialized || _currentUserData == null) return;

        int totalQuestions = GetTotalQuestionsCount();
        int oldLevel       = _currentUserData.PlayerLevel;
        int newLevel       = PlayerLevelConfig.CalculateLevel(
            _currentUserData.TotalValidQuestionsAnswered,
            totalQuestions
        );

        if (newLevel > oldLevel)
        {
            Debug.Log($"[PlayerLevelService] LEVEL UP! {oldLevel} → {newLevel}");

            _currentUserData.PlayerLevel = newLevel;

            int totalBonus = 0;
            for (int level = oldLevel + 1; level <= newLevel; level++)
            {
                int bonus = PlayerLevelConfig.GetBonusForLevel(level);
                totalBonus += bonus;
                Debug.Log($"[PlayerLevelService] Bônus do nível {level}: {bonus} pontos");
            }

            await GrantLevelUpBonus(totalBonus);
            await _firestore.UpdateUserField(_currentUserData.UserId, "PlayerLevel", newLevel);

            UserDataStore.UpdatePlayerLevel(newLevel);
            OnLevelChanged?.Invoke(oldLevel, newLevel);

            Debug.Log("[PlayerLevelService] Level atualizado no Firebase e UserDataStore");
        }
    }

    public async Task RecalculateTotalAnswered()
    {
        if (!_isInitialized || _currentUserData == null) return;

        int validTotal = await CalculateValidAnsweredQuestions(_currentUserData.UserId);
        int oldTotal   = _currentUserData.TotalValidQuestionsAnswered;
        _currentUserData.TotalValidQuestionsAnswered = validTotal;

        await _firestore.UpdateUserField(
            _currentUserData.UserId,
            "TotalValidQuestionsAnswered",
            validTotal
        );

        UserDataStore.UpdateTotalValidQuestionsAnswered(validTotal);
        Debug.Log($"[PlayerLevelService] Recalculado: {oldTotal} → {validTotal}");
        OnLevelProgressUpdated?.Invoke(validTotal);
    }

    // -------------------------------------------------------
    // IPlayerLevelService — getters
    // -------------------------------------------------------

    public int GetCurrentLevel()                => _currentUserData?.PlayerLevel ?? 1;
    public int GetTotalValidAnswered()           => _currentUserData?.TotalValidQuestionsAnswered ?? 0;
    public int GetTotalQuestionsInAllDatabanks() => _currentUserData?.TotalQuestionsInAllDatabanks ?? 0;

    public float GetProgressInCurrentLevel()
    {
        if (_currentUserData == null) return 0f;

        int totalQuestions  = GetTotalQuestionsCount();
        int currentLevel    = _currentUserData.PlayerLevel;
        var threshold       = PlayerLevelConfig.GetThresholdForLevel(currentLevel);

        float currentPercentage = (float)_currentUserData.TotalValidQuestionsAnswered / totalQuestions;
        float levelRange        = threshold.MaxPercentage - threshold.MinPercentage;
        float progressInLevel   = (currentPercentage - threshold.MinPercentage) / levelRange;

        return Mathf.Clamp01(progressInLevel);
    }

    public int GetQuestionsUntilNextLevel()
    {
        if (_currentUserData == null) return 0;
        if (_currentUserData.PlayerLevel >= 10) return 0;

        int totalQuestions = GetTotalQuestionsCount();
        int nextLevel      = _currentUserData.PlayerLevel + 1;
        var nextThreshold  = PlayerLevelConfig.GetThresholdForLevel(nextLevel);

        int questionsNeeded = nextThreshold.GetRequiredQuestions(totalQuestions);
        int remaining       = questionsNeeded - _currentUserData.TotalValidQuestionsAnswered;

        return Mathf.Max(0, remaining);
    }

    // -------------------------------------------------------
    // Helpers privados
    // -------------------------------------------------------

    private async Task GrantLevelUpBonus(int bonusPoints)
    {
        _currentUserData.Score     += bonusPoints;
        _currentUserData.WeekScore += bonusPoints;

        await _firestore.UpdateUserScores(
            _currentUserData.UserId,
            bonusPoints,
            0, "", false
        );

        UserDataStore.CurrentUserData = _currentUserData;
        Debug.Log($"[PlayerLevelService] Bônus concedido: {bonusPoints} pontos");
    }

    private int GetTotalQuestionsCount()
    {
        int total = _currentUserData?.TotalQuestionsInAllDatabanks ?? 0;

        if (total <= 0 && _statistics != null)
        {
            total = _statistics.GetTotalQuestionsCount();
            Debug.Log($"[PlayerLevelService] Total obtido do IStatisticsProvider: {total}");
        }

        if (total <= 0)
        {
            Debug.LogError("[PlayerLevelService] Não foi possível obter total de questões. Usando fallback 100.");
            total = 100;
        }

        return total;
    }

    private async Task<int> CalculateValidAnsweredQuestions(string userId)
    {
        Debug.Log($"[PlayerLevelService] CalculateValidAnsweredQuestions() para userId: {userId}");

        UserData userData = await _firestore.GetUserData(userId);

        if (userData == null)
        {
            Debug.LogError("[PlayerLevelService] GetUserData retornou NULL!");
            return 0;
        }

        int total = 0;
        foreach (var kvp in userData.AnsweredQuestions)
        {
            string databankName = kvp.Key;
            bool isReset = userData.ResetDatabankFlags != null &&
                           userData.ResetDatabankFlags.ContainsKey(databankName) &&
                           userData.ResetDatabankFlags[databankName];

            if (!isReset)
            {
                int count = new HashSet<int>(kvp.Value).Count;
                total += count;
                Debug.Log($"[PlayerLevelService] Banco '{databankName}': {count} questões válidas");
            }
            else
            {
                Debug.Log($"[PlayerLevelService] Banco '{databankName}': ignorado (resetado)");
            }
        }

        Debug.Log($"[PlayerLevelService] Total calculado: {total} questões");
        return total;
    }
}