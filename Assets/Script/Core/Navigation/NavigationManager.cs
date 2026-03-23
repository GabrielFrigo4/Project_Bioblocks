using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour, INavigationService
{
    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;

    private ISceneDataService _sceneData;

    private Dictionary<string, string> buttonSceneMapping = new Dictionary<string, string>()
    {
        { "HomeButton",      "PathwayScene" },
        { "RankingButton",   "RankingScene" },
        { "FavoritesButton", "RankingScene" },
        { "MedalsButton",    "ProfileScene" },
        { "ProfileButton",   "ProfileScene" }
    };

    // -------------------------------------------------------
    // INavigationService — eventos
    // -------------------------------------------------------

    public event Action<string> OnSceneChanged;
    public event Action<string> OnNavigationComplete;

    // -------------------------------------------------------
    // Injeção de dependência
    // -------------------------------------------------------

    public void InjectDependencies(ISceneDataService sceneDataService)
    {
        _sceneData = sceneDataService;
    }

    // -------------------------------------------------------
    // Ciclo de vida
    // -------------------------------------------------------

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (debugLogs)
            Debug.Log("[NavigationManager] Inicializado");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (debugLogs)
            Debug.Log($"[NavigationManager] Cena carregada: {scene.name}");

        OnSceneChanged?.Invoke(scene.name);
        OnNavigationComplete?.Invoke(scene.name);
    }

    // -------------------------------------------------------
    // INavigationService — navegação
    // -------------------------------------------------------

    public void NavigateTo(string sceneName, Dictionary<string, object> sceneData = null)
    {
        if (debugLogs)
            Debug.Log($"[NavigationManager] Navegando para: {sceneName}");

        try
        {
            if (buttonSceneMapping.ContainsKey(sceneName))
            {
                if (debugLogs)
                    Debug.Log($"[NavigationManager] Convertendo {sceneName} → {buttonSceneMapping[sceneName]}");
                sceneName = buttonSceneMapping[sceneName];
            }

            if (sceneData != null && _sceneData != null)
                _sceneData.SetData(sceneData);

            SceneManager.LoadScene(sceneName);

            if (debugLogs)
                Debug.Log($"[NavigationManager] Cena carregada: {sceneName}");

            OnSceneChanged?.Invoke(sceneName);
        }
        catch (Exception e)
        {
            Debug.LogError($"[NavigationManager] Erro ao carregar cena {sceneName}: {e.Message}");
        }
    }

    public void OnNavigationButtonClicked(string buttonName)
    {
        if (debugLogs)
            Debug.Log($"[NavigationManager] Botão clicado: {buttonName}");

        NavigateTo(buttonName);
    }

    public void AddButtonSceneMapping(string buttonName, string sceneName)
    {
        buttonSceneMapping[buttonName] = sceneName;
    }
}