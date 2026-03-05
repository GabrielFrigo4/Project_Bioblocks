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
    [SerializeField] protected GameObject rankingRowPrefab;
    [SerializeField] protected RectTransform rankingTableContent;
    [SerializeField] protected ScrollRect scrollRect;

    [Header("Week Reset Information")]
    [SerializeField] private TMP_Text weekResetCountdownText;
    private WeekResetCountdown resetCountdown;

    [Header("Loading Status")]
    [SerializeField] private GameObject loadingIndicator;
    [SerializeField] private TMP_Text lastUpdateText;

    protected UserData currentUserData;
    protected List<Ranking> rankings;
    protected IRankingRepository rankingRepository;

    private DateTime lastFetchTime = DateTime.MinValue;
    private bool isFetching = false;

    protected virtual void Start()
    {
        if (rankingRowPrefab == null || rankingTableContent == null || scrollRect == null)
        {
            Debug.LogError("RankingManager: Referências obrigatórias não configuradas!");
            return;
        }

        InitializeRepository();
        InitializeWeekResetCountdown();
    }

    private void InitializeWeekResetCountdown()
    {
        if (weekResetCountdownText != null)
        {
            resetCountdown = gameObject.AddComponent<WeekResetCountdown>();
            resetCountdown.Initialize(weekResetCountdownText);
        }
    }

    protected virtual void InitializeRepository()
    {
        if (BioBlocksSettings.Instance.IsDebugMode())
        {
            rankingRepository = new MockRankingRepository();
            _ = InitializeRankingManager();
            return;
        }

        if (!FirestoreRepository.Instance.IsInitialized)
        {
            Debug.LogError("FirestoreRepository não está inicializado");
            return;
        }

        rankingRepository = new RankingRepository();
        _ = InitializeRankingManager();
    }

    protected virtual async Task InitializeRankingManager()
    {
        currentUserData = await rankingRepository.GetCurrentUserDataAsync();
        if (currentUserData != null)
        {
            UserDataStore.OnUserDataChanged += OnUserDataChanged;
            await FetchRankings();
        }
        else
        {
            Debug.LogError("User data not loaded.");
        }
    }

    protected virtual void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
    }

    protected virtual void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
        _ = FetchRankings();
    }

    public virtual async Task FetchRankings()
    {
        if (isFetching)
        {
            Debug.LogWarning("Já existe uma busca de rankings em andamento.");
            return;
        }

        try
        {
            isFetching = true;
            ShowLoadingIndicator(true);
            
            rankings = await rankingRepository.GetRankingsAsync();
            lastFetchTime = DateTime.UtcNow;

            if (rankings != null && rankings.Count > 0)
            {
                rankings = rankings
                    .OrderByDescending(r => r.userScore)
                    .ThenByDescending(r => r.userWeekScore)
                    .ToList();

                UpdateRankingTable();
                UpdateLastFetchTime();
            }
            else
            {
                Debug.LogWarning("Nenhum ranking foi obtido!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar rankings: {e.Message}\n{e.StackTrace}");
            rankings = new List<Ranking>();
        }
        finally
        {
            isFetching = false;
            ShowLoadingIndicator(false);
        }
    }

    private void ShowLoadingIndicator(bool show)
    {
        if (loadingIndicator != null)
        {
            loadingIndicator.SetActive(show);
        }
    }

    private void UpdateLastFetchTime()
    {
        if (lastUpdateText != null)
        {
            if (lastFetchTime == DateTime.MinValue)
            {
                lastUpdateText.text = "Nunca atualizado";
            }
            else
            {
                var timeSince = DateTime.UtcNow - lastFetchTime;
                
                if (timeSince.TotalMinutes < 1)
                {
                    lastUpdateText.text = "Atualizado agora";
                }
                else if (timeSince.TotalMinutes < 60)
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalMinutes} min";
                }
                else if (timeSince.TotalHours < 24)
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalHours}h";
                }
                else
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalDays}d";
                }
            }
        }
    }

    protected virtual void UpdateRankingTable()
    {
        if (rankingTableContent == null)
        {
            Debug.LogError("rankingTableContent é null!");
            return;
        }

        foreach (Transform child in rankingTableContent)
        {
            Destroy(child.gameObject);
        }

        var top20Rankings = rankings.Take(20).ToList();

        for (int i = 0; i < top20Rankings.Count; i++)
        {
            var ranking = top20Rankings[i];
            bool isCurrentUser = ranking.userName == currentUserData.NickName;
            bool applyCurrentUserStyle = isCurrentUser && (i + 1) > 3;
            CreateRankingRow(i + 1, ranking, applyCurrentUserStyle);
        }

        if (!top20Rankings.Any(r => r.userName == currentUserData.NickName))
        {
            int currentUserRank = rankings.FindIndex(r => r.userName == currentUserData.NickName) + 1;
            var currentUserRanking = rankings.Find(r => r.userName == currentUserData.NickName);

            if (currentUserRanking != null && currentUserRank > 20)
            {
                GameObject separatorGO = Instantiate(rankingRowPrefab, rankingTableContent);
                var separatorUI = separatorGO.GetComponent<RankingRowUI>();
                if (separatorUI != null)
                {
                    separatorUI.SetupAsExtraRow(currentUserRank, currentUserRanking.userName,
                        currentUserRanking.userScore, currentUserRanking.userWeekScore,
                        currentUserRanking.profileImageUrl);
                }
            }
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rankingTableContent);

        if (scrollRect != null)
        {
            StartCoroutine(ScrollToCurrentUser());
        }
    }

    protected virtual IEnumerator ScrollToCurrentUser()
    {
        yield return new WaitForEndOfFrame();

        int currentUserRank = rankings.FindIndex(r => r.userName == currentUserData.NickName) + 1;
        if (currentUserRank > 15)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    protected virtual void CreateRankingRow(int rank, Ranking ranking, bool isCurrentUser)
    {
        GameObject rowGO = Instantiate(rankingRowPrefab, rankingTableContent);
        var rowUI = rowGO.GetComponent<RankingRowUI>();
        if (rowUI != null)
        {
            rowUI.Setup(rank, ranking.userName, ranking.userScore,
                        ranking.userWeekScore, ranking.profileImageUrl, isCurrentUser);
        }
        else
        {
            Debug.LogError("RankingRowUI component not found on prefab!");
        }
    }

    protected virtual void OnRankingRowClicked(Ranking ranking)
    {
        Debug.Log($"Clicked on ranking for user: {ranking.userName}");
    }

    public virtual void Navigate(string sceneName)
    {
        NavigationManager.Instance.NavigateTo(sceneName);
    }

    public async void OnRefreshButtonClicked()
    {
        await FetchRankings();
    }
}