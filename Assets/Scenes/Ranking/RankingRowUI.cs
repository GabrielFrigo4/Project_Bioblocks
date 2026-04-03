using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingRowUI : MonoBehaviour
{
    [Header("Text Components")]
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] public  TMP_Text totalScoreText;
    [SerializeField] public  TMP_Text weekScoreText;

    [Header("Background Images")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image rankBadgeImage;

    [Header("Profile Image")]
    [SerializeField] private ProfileImageLoader imageLoader;

    public string UserId { get; private set; } = "";
    private bool _isExtraRow = false;

    // ─────────────────────────────────────────────────────────
    // Setup
    // ─────────────────────────────────────────────────────────
    private void Awake() => ValidateReferences();

    private void ValidateReferences()
    {
        if (imageLoader == null)
            Debug.LogError($"[RankingRowUI] ProfileImageLoader não atribuído em '{gameObject.name}'!");

        if (rankText == null || nickNameText == null || weekScoreText == null)
            Debug.LogError($"[RankingRowUI] Componentes de texto obrigatórios ausentes em '{gameObject.name}'!");
    }

    public void Setup(int rank, string userId, string userName,
                      int totalScore, int weekScore,
                      string profileImageUrl, bool isCurrentUser)
    {
        if (imageLoader == null)
        {
            Debug.LogError($"[RankingRowUI] Setup abortado — ProfileImageLoader ausente para '{userName}'");
            return;
        }

        UserId = userId;

        rankText.text     = _isExtraRow ? "..." : $"{rank}";
        nickNameText.text = userName;

        if (totalScoreText != null)
        {
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = $"{totalScore}";
        }

        if (weekScoreText != null)
        {
            weekScoreText.gameObject.SetActive(true);
            weekScoreText.text = $"{weekScore}";
        }

        SetupRankBadge(rank);
        imageLoader.LoadProfileImage(profileImageUrl);
    }

    public void UpdateScores(int totalScore, int weekScore)
    {
        if (totalScoreText != null) totalScoreText.text = $"{totalScore}";
        if (weekScoreText  != null) weekScoreText.text  = $"{weekScore}";
    }

    public void SetupAsExtraRow(int actualRank, string userName,
                                int totalScore, int weekScore, string profileImageUrl)
    {
        _isExtraRow = true;
        Setup(actualRank, "", userName, totalScore, weekScore, profileImageUrl, true);
    }

    // ─────────────────────────────────────────────────────────
    // Badge de posição
    // ─────────────────────────────────────────────────────────
    private void SetupRankBadge(int rank)
    {
        if (rankBadgeImage == null) return;

        if (rank > 3)
        {
            rankBadgeImage.gameObject.SetActive(false);
            return;
        }

        rankBadgeImage.gameObject.SetActive(true);

        Color badgeColor;
        string hex = rank switch
        {
            1 => "#ece057ff",
            2 => "#98a1a2ff",
            3 => "#252325ff",
            _ => "#ffffffff",
        };
        ColorUtility.TryParseHtmlString(hex, out badgeColor);
        rankBadgeImage.color = badgeColor;
    }
}