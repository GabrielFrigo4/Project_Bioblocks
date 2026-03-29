using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    [Header("UserData UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text emailText;
    [SerializeField] private TMP_Text createdTimeText;
    [SerializeField] private CanvasGroup userDataTable;

    [Header("Delete Account")]
    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private TextMeshProUGUI deleteAccountButtonText;
    [SerializeField] private DeleteAccountPanel deleteAccountPanel;

    [Header("Configurações de Overlay")]
    [SerializeField] private GameObject deleteAccountDarkOverlay;
    [SerializeField] private float overlayAlpha = 0.6f;

    [Header("ReAuthentication")]
    [SerializeField] private ReAuthenticationUI reAuthUI;

    [Header("Navegação e UI Global")]
    private INavigationService _navigation;
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    // -------------------------------------------------------
    // Dependências — obtidas do AppContext no Start()
    // -------------------------------------------------------
    private IFirestoreRepository _firestore;
    private IStorageRepository _storage;
    private IAuthRepository _auth;
    private IStatisticsProvider _statistics;

    private UserData currentUserData;
    private bool firestoreDeleted = false;
    private bool storageDeleted = false;

    // -------------------------------------------------------
    // Ciclo de vida
    // -------------------------------------------------------

    private void Start()
    {
        _firestore  = AppContext.Firestore;
        _storage    = AppContext.Storage;
        _auth       = AppContext.Auth;
        _statistics = AppContext.Statistics;
        _navigation = AppContext.Navigation;

        if (deleteAccountPanel == null)
            Debug.LogError("DeleteAccountPanel não está atribuído no ProfileManager!");

        if (deleteAccountDarkOverlay != null)
            deleteAccountDarkOverlay.SetActive(false);

        HalfViewComponent halfView = HalfViewRegistry.GetHalfViewForScene(SceneManager.GetActiveScene().name);
        if (halfView != null)
        {
            halfView.Configure(
                "Opções da Conta",
                "Escolha uma das opções abaixo:",
                "Sair da Conta",
                () => LogoutButton(),
                "Deletar Conta",
                () =>
                {
                    halfView.HideMenu();
                    StartCoroutine(DelayedStartDeleteAccount());
                }
            );
        }

        InitializeAccountManager();
    }

    private void OnEnable()
    {
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        AnsweredQuestionsManager.OnAnsweredQuestionsUpdated += HandleAnsweredQuestionsUpdated;
    }

    private void OnDisable()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
        AnsweredQuestionsManager.OnAnsweredQuestionsUpdated -= HandleAnsweredQuestionsUpdated;
        DatabaseStatisticsManager.OnStatisticsReady -= OnDatabaseStatisticsReady;

        if (this != null && gameObject != null && gameObject.scene.isLoaded)
        {
            GameObject darkOverlay = GameObject.Find("DarkOverlay");
            if (darkOverlay != null && darkOverlay.activeInHierarchy)
            {
                darkOverlay.SetActive(false);
                Debug.Log("DarkOverlay desativado via OnDisable");
            }
        }
    }

    // -------------------------------------------------------
    // Inicialização
    // -------------------------------------------------------

    private void InitializeAccountManager()
    {
        currentUserData = UserDataStore.CurrentUserData;
        Debug.Log($"CurrentUserData: {(currentUserData != null ? "Loaded" : "Null")}");

        if (currentUserData == null)
        {
            Debug.LogError("User data not loaded. Redirecting to Login.");
            return;
        }

        UpdateUI();

        _firestore.ListenToUserData(
            currentUserData.UserId,
            onScoreChanged: null,
            onWeekScoreChanged: null,
            onAnsweredQuestionsChanged: answeredQuestions =>
            {
                Debug.Log("ProfileManager: Recebeu atualização de questões respondidas via listener");
                if (_statistics.IsInitialized)
                    DisplayAnsweredQuestionsCount();
            }
        );

        if (_statistics.IsInitialized)
        {
            DisplayAnsweredQuestionsCount();
        }
        else
        {
            DatabaseStatisticsManager.OnStatisticsReady += OnDatabaseStatisticsReady;
            StartCoroutine(InitializeDatabaseStatistics());
        }
    }

    private IEnumerator InitializeDatabaseStatistics()
    {
        yield return null;
        var task = (_statistics as DatabaseStatisticsManager)?.Initialize();
        if (task == null) yield break;
        while (!task.IsCompleted) yield return null;
        if (task.IsFaulted)
            Debug.LogError($"Erro ao inicializar estatísticas: {task.Exception}");
    }

    private void OnDatabaseStatisticsReady()
    {
        Debug.Log("ProfileManager: Estatísticas prontas");
        DisplayAnsweredQuestionsCount();
        DatabaseStatisticsManager.OnStatisticsReady -= OnDatabaseStatisticsReady;
    }

    // -------------------------------------------------------
    // UI
    // -------------------------------------------------------

    private void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentUserData == null)
        {
            Debug.LogError("Tentando atualizar UI com userData null");
            return;
        }

        nameText.text = currentUserData.Name;
        emailText.text = currentUserData.Email;
        createdTimeText.text = $"Conta criada em {currentUserData.GetFormattedCreatedTime()}";
    }

    private void HandleAnsweredQuestionsUpdated(Dictionary<string, int> answeredCounts)
    {
        if (this == null) return;
        Debug.Log("ProfileManager: Recebeu atualização do AnsweredQuestionsManager");
        DisplayAnsweredQuestionsCount();
    }

    private void DisplayAnsweredQuestionsCount()
    {
        if (currentUserData == null || string.IsNullOrEmpty(currentUserData.UserId))
        {
            Debug.LogError("Usuário não encontrado ou ID inválido");
            return;
        }

        var userAnsweredQuestions = AnsweredQuestionsListStore.GetAnsweredQuestionsCountForUser(currentUserData.UserId);

        string[] allDatabases =
        {
            "AcidBaseBufferQuestionDatabase",
            "AminoacidQuestionDatabase",
            "BiochemistryIntroductionQuestionDatabase",
            "CarbohydratesQuestionDatabase",
            "EnzymeQuestionDatabase",
            "LipidsQuestionDatabase",
            "MembranesQuestionDatabase",
            "NucleicAcidsQuestionDatabase",
            "ProteinQuestionDatabase",
            "WaterQuestionDatabase"
        };

        foreach (string databankName in allDatabases)
        {
            int answeredCount = userAnsweredQuestions.ContainsKey(databankName) ? userAnsweredQuestions[databankName] : 0;
            int totalQuestions = QuestionBankStatistics.GetTotalQuestions(databankName);
            if (totalQuestions <= 0) totalQuestions = 50;

            string objectName = $"{databankName}CountText";
            GameObject textObject = GameObject.Find(objectName);
            if (textObject == null)
            {
                Debug.LogWarning($"Não foi possível encontrar o GameObject: {objectName}");
                continue;
            }

            TextMeshProUGUI tmpText = textObject.GetComponent<TextMeshProUGUI>();
            if (tmpText == null)
            {
                Debug.LogWarning($"TextMeshProUGUI não encontrado em: {objectName}");
                continue;
            }

            tmpText.text = $"{answeredCount}/{totalQuestions}";
        }
    }

    // -------------------------------------------------------
    // Navegação
    // -------------------------------------------------------
    
    public void Navigate(string sceneName)
    {
        
        if (_navigation != null)
            _navigation.NavigateTo(sceneName);
        else
        {
            Debug.LogWarning("[ProfileManager] NavigationService não disponível, usando SceneManager diretamente.");
            SceneManager.LoadScene(sceneName);
        }
    }

    // -------------------------------------------------------
    // Logout
    // -------------------------------------------------------

    public void LogoutButton()
    {
        StartCoroutine(LogoutAsync().AsCoroutine());
    }

    public async Task LogoutAsync()
    {
        try
        {
            // Desinscreve os eventos ANTES de limpar os dados
            UserDataStore.OnUserDataChanged -= OnUserDataChanged;
            AnsweredQuestionsManager.OnAnsweredQuestionsUpdated -= HandleAnsweredQuestionsUpdated;


            string currentUserId = UserDataStore.CurrentUserData?.UserId;
            UserDataStore.CurrentUserData = null;
            AppContext.AnsweredQuestions?.ResetManager();

            if (!string.IsNullOrEmpty(currentUserId))
                AnsweredQuestionsListStore.ClearUserAnsweredQuestions(currentUserId);

            await _auth.LogoutAsync();
            Debug.Log("Logout realizado com sucesso");

            Navigate("LoginView");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao realizar logout: {ex.Message}");
            throw;
        }
    }

    // -------------------------------------------------------
    // Delete Account
    // -------------------------------------------------------

    private IEnumerator DelayedStartDeleteAccount()
    {
        yield return new WaitForSeconds(0.1f);
        StartDeleteAccount();
    }

    public void StartDeleteAccount()
    {
        Debug.Log("StartDeleteAccount chamado");

        if (deleteAccountPanel == null)
        {
            Debug.LogError("DeleteAccountPanel é null!");
            return;
        }

        if (deleteAccountDarkOverlay != null)
        {
            if (!deleteAccountDarkOverlay.activeSelf)
                deleteAccountDarkOverlay.SetActive(true);

            Canvas overlayCanvas = deleteAccountDarkOverlay.GetComponent<Canvas>();
            if (overlayCanvas == null)
            {
                overlayCanvas = deleteAccountDarkOverlay.AddComponent<Canvas>();
                deleteAccountDarkOverlay.AddComponent<GraphicRaycaster>();
            }
            overlayCanvas.overrideSorting = true;
            overlayCanvas.sortingOrder = 109;

            Image overlayImage = deleteAccountDarkOverlay.GetComponent<Image>();
            if (overlayImage != null)
            {
                Color color = overlayImage.color;
                color.a = overlayAlpha;
                overlayImage.color = color;
            }
        }
        else
        {
            Debug.LogError("DeleteAccountDarkOverlay é null!");
        }

        userDataTable.alpha = 0;
        userDataTable.interactable = false;
        userDataTable.blocksRaycasts = false;

        HalfViewRegistry.HideHalfViewForCurrentScene();
        deleteAccountPanel.ShowPanel();
        Debug.Log("DeleteAccountPanel exibido com sucesso");
    }

    public void CancelDeleteAccount()
    {
        if (deleteAccountPanel == null) return;

        if (deleteAccountDarkOverlay != null)
            deleteAccountDarkOverlay.SetActive(false);

        GameObject halfViewOverlay = GameObject.Find("HalfViewDarkOverlay");
        if (halfViewOverlay != null)
            halfViewOverlay.SetActive(false);

        ReenableSceneInteractions();
        userDataTable.alpha = 1;
        userDataTable.interactable = true;
        userDataTable.blocksRaycasts = true;
        deleteAccountPanel.HidePanel();
    }

    public void DeleteAccountButton()
    {
        StartCoroutine(DeleteAccountAsync().AsCoroutine());
    }           

    public async Task DeleteAccountAsync(bool isRetry = false)
    {
        Debug.Log($"Starting DeleteAccountAsync... (isRetry: {isRetry})");

        if (currentUserData.UserId == null)
        {
            Debug.LogError("The user is not connected.");
            return;
        }

        string userId = currentUserData.UserId;

        try
        {
            if (deleteAccountButton != null) deleteAccountButton.interactable = false;
            if (deleteAccountButtonText != null) deleteAccountButtonText.text = "Verificando...";

            if (!isRetry)
            {
                // 1. Deletar documento do Firestore
                if (!firestoreDeleted)
                {
                    try
                    {
                        await _firestore.DeleteDocument("Users", userId);
                        Debug.Log("Documento do Firestore deletado com sucesso");
                        firestoreDeleted = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Erro ao deletar Firestore: {ex.Message}");
                    }
                }

                // 2. Deletar imagem do Storage
                if (!storageDeleted && !string.IsNullOrEmpty(currentUserData.ProfileImageUrl))
                {
                    try
                    {
                        await _storage.DeleteProfileImageAsync(currentUserData.ProfileImageUrl);
                        Debug.Log("Imagem do Storage deletada com sucesso");
                        storageDeleted = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Erro ao deletar Storage: {ex.Message}");
                    }
                }
            }

            // 3. Deletar usuário do Authentication
            try
            {
                await _auth.DeleteUser(userId);
                Debug.Log("Usuário deletado do Authentication com sucesso");

                if (deleteAccountButtonText != null) deleteAccountButtonText.text = "Até a próxima!";
                deleteAccountPanel.HidePanel();

                CleanupOverlays();
                ReenableSceneInteractions();

                UserDataStore.CurrentUserData = null;
            

                AnsweredQuestionsListStore.ClearAll();
                Debug.Log("=== LIMPEZA COMPLETA FINALIZADA ===");

                await Task.Delay(300);
                loadingSpinner?.ShowSpinnerUntilSceneLoaded("LoginView");
                Navigate("LoginView");
            }
            catch (ReauthenticationRequiredException)
            {
                Debug.Log("Reautenticação necessária para deletar usuário");

                if (deleteAccountButton != null)
                {
                    deleteAccountButton.interactable = true;
                    deleteAccountButtonText.text = "Deletar Conta";
                }

                deleteAccountPanel.HidePanel();

                if (reAuthUI != null)
                {
                    Canvas reAuthCanvas = reAuthUI.GetComponent<Canvas>() ?? reAuthUI.gameObject.AddComponent<Canvas>();
                    reAuthCanvas.overrideSorting = true;
                    reAuthCanvas.sortingOrder = 200;

                    if (reAuthUI.GetComponent<GraphicRaycaster>() == null)
                        reAuthUI.gameObject.AddComponent<GraphicRaycaster>();
                }

                reAuthUI.ShowReAuthPanel(currentUserData.Email, async () =>
                {
                    Debug.Log("Reautenticação bem-sucedida, tentando deletar apenas Authentication...");
                    await DeleteAccountAsync(true);
                });
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao deletar conta: {ex.Message}");
            if (deleteAccountButton != null)
            {
                deleteAccountButton.interactable = true;
                deleteAccountButtonText.text = "Novamente";
            }
            loadingSpinner?.HideSpinner();
        }
    }

    // -------------------------------------------------------
    // Helpers privados
    // -------------------------------------------------------

    private void CleanupOverlays()
    {
        if (deleteAccountDarkOverlay != null)
        {
            Canvas overlayCanvas = deleteAccountDarkOverlay.GetComponent<Canvas>();
            if (overlayCanvas != null) Destroy(overlayCanvas);
            deleteAccountDarkOverlay.SetActive(false);
        }

        GameObject halfViewOverlay = GameObject.Find("HalfViewDarkOverlay");
        if (halfViewOverlay != null) halfViewOverlay.SetActive(false);

        string[] overlayNames = { "DarkOverlay", "DeleteAccountDarkOverlay", "HalfViewDarkOverlay", "Overlay", "BlockerPanel" };
        foreach (string overlayName in overlayNames)
        {
            GameObject overlay = GameObject.Find(overlayName);
            if (overlay != null && overlay.activeSelf)
            {
                Canvas canvas = overlay.GetComponent<Canvas>();
                if (canvas != null) canvas.enabled = false;
                overlay.SetActive(false);
            }
        }

        Canvas[] allCanvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.sortingOrder >= 100 && canvas.gameObject.name.Contains("Dark"))
                canvas.gameObject.SetActive(false);
        }
    }

    private void ReenableSceneInteractions()
    {
        Selectable[] selectables = FindObjectsByType<Selectable>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        foreach (Selectable selectable in selectables)
        {
            if (!ShouldStayDisabled(selectable.gameObject))
                selectable.interactable = true;
        }
        Debug.Log("Todas as interações da cena reabilitadas");
    }

    private bool ShouldStayDisabled(GameObject obj) => false;
}