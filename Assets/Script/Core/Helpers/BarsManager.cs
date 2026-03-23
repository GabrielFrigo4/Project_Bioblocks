using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BarsManager : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] protected string currentScene = "";

    protected List<string> scenesWithoutBar = new List<string>();
    protected bool isSceneBeingLoaded = false;
    protected abstract string BarName { get; }
    protected abstract string BarChildName { get; }
    protected INavigationService _navigation;

    protected virtual void Awake()
    {
        if (AppContext.IsReady)
        {
            InitializeBar();
        }
        else
        {
            AppContext.OnReady += InitializeBar;
        }
    }

    private void InitializeBar()
    {
        AppContext.OnReady -= InitializeBar;
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void Start()
    {
        if (AppContext.IsReady)
        {
            InitializeNavigation();
        }
        else
        {
            AppContext.OnReady += InitializeNavigation;
        }
    }

    private void InitializeNavigation()
    {
        AppContext.OnReady -= InitializeNavigation;
        _navigation = AppContext.Navigation;
        RegisterWithNavigationManager();

        string activeScene = SceneManager.GetActiveScene().name;
        if (!string.IsNullOrEmpty(activeScene))
            currentScene = activeScene;

        UpdateBarState(currentScene);
        OnStart();
    }

    protected virtual void OnDestroy()
    {
        AppContext.OnReady -= InitializeBar;
        AppContext.OnReady -= InitializeNavigation;
        OnCleanup();
    }

    protected virtual void OnEnable()
    {
        currentScene = SceneManager.GetActiveScene().name;
        AdjustVisibilityForCurrentScene();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnCleanup() { }

    protected virtual void RegisterWithNavigationManager()
    {
         if (_navigation != null)
         {
             _navigation.OnSceneChanged -= OnSceneChanged;
         }
        else
        {
            Debug.LogWarning($"{BarName}: NavigationManager não encontrado no AppContext!");
        }
    }

    protected virtual void OnSceneChanged(string sceneName)
    {
        Debug.Log($"[{BarName}] OnSceneChanged chamado: {sceneName} | isSceneBeingLoaded: {isSceneBeingLoaded}");

        if (isSceneBeingLoaded)
        {
            Debug.LogWarning($"[{BarName}] Cena está sendo carregada, ignorando...");
            return;
        }

        currentScene = sceneName;
        bool shouldShowBar = !scenesWithoutBar.Contains(sceneName);
        Debug.Log($"[{BarName}] shouldShowBar: {shouldShowBar}");

        SetBarVisibility(shouldShowBar);

        if (shouldShowBar)
        {
            Debug.Log($"[{BarName}] Chamando OnSceneChangedSpecific para {sceneName}");
            OnSceneChangedSpecific(sceneName);
            UpdateBarState(currentScene);
        }
    }

    protected virtual void OnSceneChangedSpecific(string sceneName) { }
    protected virtual void UpdateBarState(string sceneName)
    {
        currentScene = sceneName;
        AdjustVisibilityForCurrentScene();

        if (!scenesWithoutBar.Contains(currentScene))
        {
            UpdateButtonVisibility(sceneName);
            EnsureBarIntegrity();
        }
    }

    protected virtual void UpdateButtonVisibility(string sceneName) { }
    protected virtual void AdjustVisibilityForCurrentScene()
    {
        bool shouldShowBar = !scenesWithoutBar.Contains(currentScene);

        SetBarVisibility(shouldShowBar);

        if (shouldShowBar)
        {
            Transform barChild = transform.Find(BarChildName);

            if (barChild != null)
            {
                barChild.gameObject.SetActive(true);
            }

            UpdateCanvasElements(true);
        }
    }

    protected virtual void UpdateCanvasElements(bool shouldShow)
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = shouldShow;
        }

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = shouldShow ? 1f : 0f;
            canvasGroup.interactable = shouldShow;
            canvasGroup.blocksRaycasts = shouldShow;
        }
    }

    protected virtual void EnsureBarIntegrity()
    {
        if (scenesWithoutBar.Contains(currentScene))
        {
            return;
        }

        if (!gameObject.activeSelf) return;

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null && !canvas.enabled)
        {
            canvas.enabled = true;
        }

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {

            if (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha = 1f;
            }

            if (!canvasGroup.interactable)
            {
                canvasGroup.interactable = true;
            }

            if (!canvasGroup.blocksRaycasts)
            {
                canvasGroup.blocksRaycasts = true;
            }
        }

        Transform barChild = transform.Find(BarChildName);
        if (barChild != null && !barChild.gameObject.activeSelf)
        {
            barChild.gameObject.SetActive(true);
        }
    }

    public virtual void AddSceneWithoutBar(string sceneName)
    {
        if (!scenesWithoutBar.Contains(sceneName))
        {
            scenesWithoutBar.Add(sceneName);

            if (currentScene == sceneName)
            {
                AdjustVisibilityForCurrentScene();
            }
        }
    }

    public virtual void RemoveSceneWithoutBar(string sceneName)
    {
        if (scenesWithoutBar.Contains(sceneName))
        {
            scenesWithoutBar.Remove(sceneName);

            if (currentScene == sceneName)
            {
                AdjustVisibilityForCurrentScene();
            }
        }
    }

    public virtual void ForceVisibilityCheck()
    {
        currentScene = SceneManager.GetActiveScene().name;
        AdjustVisibilityForCurrentScene();
    }

    public virtual void ForceRefreshState()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        currentScene = activeScene;
        bool shouldShowBar = !scenesWithoutBar.Contains(currentScene);
        SetBarVisibility(shouldShowBar);

        if (shouldShowBar)
        {
            Transform barChild = transform.Find(BarChildName);
            if (barChild != null)
            {
                barChild.gameObject.SetActive(true);
            }

            UpdateCanvasElements(true);
            UpdateButtonVisibility(currentScene);
        }
    }

    protected virtual void SetBarVisibility(bool visible)
    {
        if (gameObject.activeSelf != visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
