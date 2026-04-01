using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia o fluxo de inicialização do app.
///
public class InitializationManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject retryPanel;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text errorText;

    [Header("Configuration")]
    [SerializeField] private float minimumLoadingTime = 2.0f;

    [Header("Global Loading Spinner")]
    [SerializeField] private GameObject globalSpinnerPrefab;

    // -------------------------------------------------------
    // Dependências — obtidas do AppContext, nunca via .Instance
    // -------------------------------------------------------
    private IFirestoreRepository _firestore;
    private IAuthRepository _auth;

    private LoadingSpinnerComponent globalSpinner;

    private void Awake()
    {
        InitializeGlobalSpinner();
    }

    private void Start()
    {
        // Obtém os serviços do AppContext uma única vez
        // O AppContext já garantiu que estão inicializados no seu próprio Awake              
        SetupUI();
        StartInitialization();
    }

    // -------------------------------------------------------
    // Spinner
    // -------------------------------------------------------
    private void InitializeGlobalSpinner()
    {
        try
        {
            globalSpinner = LoadingSpinnerComponent.Instance;

            if (globalSpinner == null && globalSpinnerPrefab != null)
            {
                Canvas mainCanvas = FindObjectOfType<Canvas>();
                GameObject spinnerObject = mainCanvas != null
                    ? Instantiate(globalSpinnerPrefab, mainCanvas.transform)
                    : Instantiate(globalSpinnerPrefab);

                spinnerObject.name = "GlobalLoadingSpinner";
                DontDestroyOnLoad(spinnerObject);
                globalSpinner = spinnerObject.GetComponent<LoadingSpinnerComponent>();
            }
            globalSpinner?.ShowSpinner();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error initializing spinner: {e.Message}");
        }
    }

    // -------------------------------------------------------
    // Fluxo principal
    // -------------------------------------------------------
    private void SetupUI()
    {
        if (retryPanel != null) retryPanel.SetActive(false);
        if (progressBar != null) progressBar.fillAmount = 0f;
    }

    private async void StartInitialization()
    {
        float startTime = Time.time;
        try
        {
            if (!AppContext.IsReady)
            {
            UpdateStatus("Inicializando Firebase...");
            await WaitForAppContext();
            }

            // Só busca as dependências DEPOIS que o AppContext está pronto
            _firestore = AppContext.Firestore;
            _auth      = AppContext.Auth;

            UpdateProgress(0.3f);
            UpdateStatus("Verificando autenticação...");
            bool isAuthenticated = await CheckAuthentication();
            UpdateProgress(0.5f);
            bool userDataLoaded = false;

            if (isAuthenticated)
            {
                UpdateStatus("Carregando dados do usuário...");
                userDataLoaded = await LoadUserData();
                UpdateProgress(0.7f);

                if (userDataLoaded)
                {
                    UpdateStatus("Carregando bancos de questões...");
                    await DatabaseStatisticsManager.Instance.Initialize();
                    UpdateProgress(0.85f);

                    UpdateStatus("Configurando sistema de níveis...");
                    InitializePlayerLevelService();
                    UpdateProgress(0.9f);
                }
            }

            // Garante tempo mínimo de loading
            float elapsed = Time.time - startTime;
            if (elapsed < minimumLoadingTime)
                await Task.Delay(Mathf.RoundToInt((minimumLoadingTime - elapsed) * 1000));

            NavigateAfterInit(isAuthenticated && userDataLoaded);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InitializationManager] INITIALIZATION FAILED: {ex.GetType().Name}: {ex.Message}");
            Debug.LogError($"[InitializationManager] StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                Debug.LogError($"[InitializationManager] InnerException: {ex.InnerException.Message}");

            try { globalSpinner?.HideSpinner(); } catch { }

            ShowError($"Falha na inicialização: {ex.Message}");
        }
    }

    /// <summary>
    /// Aguarda o AppContext terminar a inicialização assíncrona.
    /// Em condições normais isso já estará pronto quando o Start() rodar,
    /// mas esta guarda evita race conditions caso o Firebase demore mais que o esperado.
    /// </summary>
    private async Task WaitForAppContext()
    {
        float timeout = 15f;
        float elapsed = 0f;

        while (!AppContext.IsReady && elapsed < timeout)
        {
            await Task.Delay(100);
            elapsed += 0.1f;

            // Loga a cada 3 segundos para acompanhar o progresso
            if (Mathf.RoundToInt(elapsed * 10) % 30 == 0)
            Debug.Log($"[InitializationManager] Aguardando AppContext... {elapsed:F1}s");
        }

        if (!AppContext.IsReady)
            throw new Exception("[InitializationManager] AppContext não ficou pronto dentro do timeout.");

        Debug.Log("[InitializationManager] AppContext pronto.");
    }

    // -------------------------------------------------------
    // Autenticação e dados
    // -------------------------------------------------------
    private async Task<bool> CheckAuthentication()
    {
        if (!_auth.IsUserLoggedIn()) return false;

        try
        {
            await _auth.ReloadCurrentUserAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> LoadUserData()
    {
        try
        {
            if (!_auth.IsUserLoggedIn()) return false;
            string userId = _auth.CurrentUserId;
            var userData  = await _firestore.GetUserData(userId);
            if (userData == null) return false;
            UserDataStore.CurrentUserData = userData;
            Debug.Log($"[InitializationManager] UserData carregado. UserId: {userData.UserId}, Level: {userData.PlayerLevel}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao carregar dados: {e.Message}");
            throw;
        }
    }

    // -------------------------------------------------------
    // Navegação
    // -------------------------------------------------------
    private void NavigateAfterInit(bool authenticated)
    {
        try
        {
            string targetScene = authenticated ? "PathwayScene" : "LoginView";

            globalSpinner?.ShowSpinnerUntilSceneLoaded(targetScene);
            SceneManager.LoadScene(targetScene);
        }
        catch (Exception)
        {
            string targetScene = authenticated ? "PathwayScene" : "LoginView";
            SceneManager.LoadScene(targetScene);
        }
    }

    // -------------------------------------------------------
    // PlayerLevelManager (ainda singleton — será refatorado)
    // -------------------------------------------------------
    private void InitializePlayerLevelService()
    {
        if (AppContext.PlayerLevel == null)
        {
            Debug.LogError("[InitializationManager] PlayerLevelService não encontrado no AppContext.");
            return;
        }

        Debug.Log("[InitializationManager] PlayerLevelService pronto.");    
    }

    // -------------------------------------------------------
    // UI helpers
    // -------------------------------------------------------
    private void UpdateStatus(string message)
    {
        if (statusText != null) statusText.text = message;
    }

    private void UpdateProgress(float progress)
    {
        if (progressBar != null) progressBar.fillAmount = progress;
    }

    private void ShowError(string message)
    {
        if (retryPanel != null)
        {
            retryPanel.SetActive(true);
            if (errorText != null) errorText.text = message;
        }
    }
}