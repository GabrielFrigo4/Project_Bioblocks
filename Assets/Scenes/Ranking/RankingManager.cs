using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject    rankingRowPrefab;
    [SerializeField] protected RectTransform rankingTableContent;
    [SerializeField] protected ScrollRect    scrollRect;

    [Header("Week Reset Information")]
    [SerializeField] private TMP_Text weekResetCountdownText;
    private WeekResetCountdown _resetCountdown;

    [Header("Loading Status")]
    [SerializeField] private GameObject loadingIndicator;
    [SerializeField] private TMP_Text   lastUpdateText;

    // ─── Estado interno ───────────────────────────────────────
    protected IRankingRepository _rankingRepository;
    protected Ranking            _currentUserRanking;  // entrada do usuário logado
    protected List<Ranking>      _rankings;

    private INavigationService _navigation;
    private DateTime           _lastFetchTime = DateTime.MinValue;
    private bool               _isFetching    = false;

    // ─────────────────────────────────────────────────────────
    // Unity lifecycle
    // ─────────────────────────────────────────────────────────
    protected virtual void Start()
    {
        _navigation = AppContext.Navigation;

        if (rankingRowPrefab == null || rankingTableContent == null || scrollRect == null)
        {
            Debug.LogError("[RankingManager] Referências obrigatórias não configuradas!");
            return;
        }

        InitializeRepository();
        InitializeWeekResetCountdown();
    }

    protected virtual void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
    }

    // ─────────────────────────────────────────────────────────
    // Inicialização
    // ─────────────────────────────────────────────────────────
    private void InitializeWeekResetCountdown()
    {
        if (weekResetCountdownText != null)
        {
            _resetCountdown = gameObject.AddComponent<WeekResetCountdown>();
            _resetCountdown.Initialize(weekResetCountdownText);
        }
    }

    protected virtual void InitializeRepository()
    {
        bool debugMode = BioBlocksSettings.Instance != null
                      && BioBlocksSettings.Instance.IsDebugMode();

        _rankingRepository = debugMode
            ? (IRankingRepository) new FakeRankingRepository()
            : new RankingRepository();

        _ = InitializeAsync();
    }

    protected virtual async Task InitializeAsync()
    {
        try
        {
            _currentUserRanking = await _rankingRepository.GetCurrentUserRankingAsync();

            if (_currentUserRanking == null)
            {
                Debug.LogError("[RankingManager] Entrada de ranking do usuário não encontrada.");
                return;
            }

            UserDataStore.OnUserDataChanged += OnUserDataChanged;
            await FetchRankings();
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingManager] Falha na inicialização: {e.Message}");
        }
    }

    // ─────────────────────────────────────────────────────────
    // Eventos
    // ─────────────────────────────────────────────────────────
    protected virtual void OnUserDataChanged(UserData userData)
    {
        if (userData == null) return;

        // Atualiza a entrada local para refletir mudanças de score
        if (_currentUserRanking != null)
        {
            _currentUserRanking.userScore     = userData.Score;
            _currentUserRanking.userWeekScore = userData.WeekScore;
        }

        // Atualiza apenas a linha do usuário já visível — sem novo fetch Firebase
        UpdateCurrentUserRowIfVisible();
    }

    // ─────────────────────────────────────────────────────────
    // Fetch
    // ─────────────────────────────────────────────────────────
    public virtual async Task FetchRankings()
    {
        if (_isFetching)
        {
            Debug.LogWarning("[RankingManager] Busca de rankings já em andamento.");
            return;
        }

        try
        {
            _isFetching    = true;
            ShowLoadingIndicator(true);

            _rankings      = await _rankingRepository.GetRankingsAsync(limit: 50);
            _lastFetchTime = DateTime.UtcNow;

            if (_rankings != null && _rankings.Count > 0)
                UpdateRankingTable();
            else
                Debug.LogWarning("[RankingManager] Ranking retornou vazio.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingManager] Erro ao buscar rankings: {e.Message}\n{e.StackTrace}");
            _rankings = new List<Ranking>();
        }
        finally
        {
            _isFetching = false;
            ShowLoadingIndicator(false);
            UpdateLastFetchLabel();
        }
    }

    // ─────────────────────────────────────────────────────────
    // UI
    // ─────────────────────────────────────────────────────────
    protected virtual void UpdateRankingTable()
    {
        if (rankingTableContent == null) return;

        foreach (Transform child in rankingTableContent)
            Destroy(child.gameObject);

        string currentUserId = AppContext.Auth.CurrentUserId;
        var    top20         = _rankings.Take(20).ToList();

        for (int i = 0; i < top20.Count; i++)
        {
            var  ranking       = top20[i];
            bool isCurrentUser = ranking.UserId == currentUserId;
            bool applyStyle    = isCurrentUser && (i + 1) > 3;
            CreateRankingRow(i + 1, ranking, applyStyle);
        }

        // Usuário fora do top-20: adiciona linha extra no final
        bool userInTop20 = top20.Any(r => r.UserId == currentUserId);
        if (!userInTop20)
        {
            int     userRank    = _rankings.FindIndex(r => r.UserId == currentUserId) + 1;
            Ranking userRanking = _rankings.Find(r => r.UserId == currentUserId);

            if (userRanking != null && userRank > 20)
            {
                GameObject go    = Instantiate(rankingRowPrefab, rankingTableContent);
                var        rowUI = go.GetComponent<RankingRowUI>();
                rowUI?.SetupAsExtraRow(userRank, userRanking.userName,
                    userRanking.userScore, userRanking.userWeekScore,
                    userRanking.profileImageUrl);
            }
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rankingTableContent);

        if (scrollRect != null)
            StartCoroutine(ScrollToCurrentUser(currentUserId));
    }

    private void UpdateCurrentUserRowIfVisible()
    {
        if (_currentUserRanking == null) return;

        string currentUserId = AppContext.Auth.CurrentUserId;

        foreach (Transform child in rankingTableContent)
        {
            var row = child.GetComponent<RankingRowUI>();
            if (row != null && row.UserId == currentUserId)
            {
                row.UpdateScores(_currentUserRanking.userScore, _currentUserRanking.userWeekScore);
                return;
            }
        }
    }

    protected virtual void CreateRankingRow(int rank, Ranking ranking, bool isCurrentUser)
    {
        GameObject rowGO = Instantiate(rankingRowPrefab, rankingTableContent);
        var        rowUI = rowGO.GetComponent<RankingRowUI>();

        if (rowUI != null)
            rowUI.Setup(rank, ranking.UserId, ranking.userName,
                        ranking.userScore, ranking.userWeekScore,
                        ranking.profileImageUrl, isCurrentUser);
        else
            Debug.LogError("[RankingManager] RankingRowUI não encontrado no prefab!");
    }

    protected virtual IEnumerator ScrollToCurrentUser(string currentUserId)
    {
        yield return new WaitForEndOfFrame();

        int userRank = _rankings.FindIndex(r => r.UserId == currentUserId) + 1;
        if (userRank > 15)
            scrollRect.verticalNormalizedPosition = 0f;
    }

    private void ShowLoadingIndicator(bool show)
    {
        if (loadingIndicator != null)
            loadingIndicator.SetActive(show);
    }

    private void UpdateLastFetchLabel()
    {
        if (lastUpdateText == null) return;

        lastUpdateText.text = _lastFetchTime == DateTime.MinValue
            ? "Nunca atualizado"
            : FormatElapsedTime(DateTime.UtcNow - _lastFetchTime);
    }

    private string FormatElapsedTime(TimeSpan elapsed)
    {
        if (elapsed.TotalMinutes < 1)  return "Atualizado agora";
        if (elapsed.TotalMinutes < 60) return $"Atualizado há {(int)elapsed.TotalMinutes} min";
        if (elapsed.TotalHours   < 24) return $"Atualizado há {(int)elapsed.TotalHours}h";
        return                                $"Atualizado há {(int)elapsed.TotalDays}d";
    }

    // ─────────────────────────────────────────────────────────
    // Botões / navegação
    // ─────────────────────────────────────────────────────────
    public async void OnRefreshButtonClicked()
    {
        try   { await FetchRankings(); }
        catch (Exception e) { Debug.LogError($"[RankingManager] Refresh falhou: {e.Message}"); }
    }

    public virtual void Navigate(string sceneName)
        => _navigation.NavigateTo(sceneName);
}