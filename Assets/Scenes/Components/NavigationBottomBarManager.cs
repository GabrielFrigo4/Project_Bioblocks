using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NavigationBottomBarManager : BarsManager
{
    protected INavigationService _navigation;
    [System.Serializable]
    public class NavButton
    {
        public string buttonName;
        public string targetScene;
        public Button button;
        public Image normalIcon;
        public Image filledIcon;
    }

    [Header("Botões de Navegação")]
    [SerializeField] private NavButton homeButton;
    [SerializeField] private NavButton rankingButton;
    [SerializeField] private NavButton favoritesButton;
    [SerializeField] private NavButton medalsButton;
    [SerializeField] private NavButton profileButton;

    [Header("Persistência")]
    [SerializeField]
    private List<string> scenesWithoutBottomBar = new List<string>()
    {
        "LoginView",
        "RegisterView",
        "QuestionScene",
        "ResetDatabaseView"
    };

    private List<NavButton> allButtons = new List<NavButton>();
    protected override string BarName => "PersistentBottomBar";
    protected override string BarChildName => "BottomBar";

    protected override void OnAwake()
    {
        Debug.Log("[BottomBar] OnAwake chamado");

        base.scenesWithoutBar.Clear();
        foreach (var scene in scenesWithoutBottomBar)
        {
            base.scenesWithoutBar.Add(scene);
        }

        Debug.Log($"[BottomBar] Cenas sem barra: {string.Join(", ", scenesWithoutBottomBar)}");

        // Registra listener para cenas carregadas automaticamente
        // (quando InitializationManager carrega PathwayScene automaticamente)
        SceneManager.sceneLoaded += OnSceneLoadedDirect;
        Debug.Log("[BottomBar] Listener OnSceneLoadedDirect registrado");

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        Debug.Log("[BottomBar] InitializeButtons chamado");

        allButtons.Clear();
        if (homeButton.button != null) allButtons.Add(homeButton);
        if (rankingButton.button != null) allButtons.Add(rankingButton);
        if (favoritesButton.button != null) allButtons.Add(favoritesButton);
        if (medalsButton.button != null) allButtons.Add(medalsButton);
        if (profileButton.button != null) allButtons.Add(profileButton);

        Debug.Log($"[BottomBar] Total de botões inicializados: {allButtons.Count}");

        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        Debug.Log("[BottomBar] SetupButtonListeners chamado");

        foreach (var buttonInfo in allButtons)
        {
            if (buttonInfo.button != null)
            {
                buttonInfo.button.onClick.RemoveAllListeners();
                string targetButtonName = buttonInfo.buttonName;
                string targetSceneName = buttonInfo.targetScene;

                Debug.Log($"[BottomBar] Configurando listener para botão: {targetButtonName}");

                buttonInfo.button.onClick.AddListener(() =>
                {
                    Debug.Log($"[BottomBar] Botão clicado: {targetButtonName} -> {targetSceneName}");

                    if (_navigation != null)
                    {
                        _navigation.NavigateTo(targetSceneName);
                    }
                    else
                    {
                        Debug.LogWarning("[BottomBar] NavigationManager não encontrado, tentando novamente...");
                        RetryNavigation(targetSceneName, targetButtonName);
                    }
                });
            }
        }
    }

    private void RetryNavigation(string sceneName, string buttonName)
    {
        Debug.Log($"[BottomBar] RetryNavigation iniciado para: {sceneName}");

        if (_navigation != null)
            _navigation.NavigateTo(sceneName);
        else
            Debug.LogError($"[BottomBar] _navigation é null — verifique se AppContext.OnReady foi disparado.");
    }

    protected override void AdjustVisibilityForCurrentScene()
    {
        Debug.Log($"[BottomBar] AdjustVisibilityForCurrentScene chamado para: {currentScene}");

        bool shouldShowBar = !scenesWithoutBar.Contains(currentScene);
        Debug.Log($"[BottomBar] shouldShowBar: {shouldShowBar}");

        Transform barChild = transform.Find(BarChildName);

        if (!shouldShowBar)
        {
            Debug.Log($"[BottomBar] Ocultando barra para cena: {currentScene}");
            if (barChild != null)
            {
                barChild.gameObject.SetActive(false);
            }

            UpdateCanvasElements(false);
        }
        else
        {
            Debug.Log($"[BottomBar] Mostrando barra para cena: {currentScene}");
            if (barChild != null)
            {
                barChild.gameObject.SetActive(true);
            }

            UpdateCanvasElements(true);
        }
    }

    protected override void OnSceneChangedSpecific(string sceneName)
    {
        Debug.Log($"[BottomBar] OnSceneChangedSpecific chamado: {sceneName}");

        if (sceneName == "PathwayScene")
        {
            Debug.Log("[BottomBar] PathwayScene detectada via NavigationManager, iniciando reinicialização...");
            StartCoroutine(ReinitializeForMobile(sceneName));
        }
        else
        {
            Debug.Log($"[BottomBar] Atualizando display normalmente para: {sceneName}");
            UpdateButtonDisplay(sceneName);
        }
    }

    private void OnSceneLoadedDirect(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        Debug.Log($"[BottomBar] OnSceneLoadedDirect acionado: {sceneName} | gameObject.activeSelf: {gameObject.activeSelf}");

        bool shouldShow = !scenesWithoutBar.Contains(sceneName);
        Debug.Log($"[BottomBar] shouldShow: {shouldShow}");

        if (shouldShow)
        {
            Debug.Log($"[BottomBar] Cena {sceneName} deve mostrar a barra. Atualizando...");
            if (sceneName == "PathwayScene")
            {
                Debug.Log("[BottomBar] PathwayScene detectada via SceneManager, iniciando reinicialização...");
                StartCoroutine(ReinitializeForMobile(sceneName));
            }
            else
            {
                Debug.Log($"[BottomBar] Atualizando display para: {sceneName}");
                UpdateButtonDisplay(sceneName);
            }
        }
        else
        {
            Debug.Log($"[BottomBar] Cena {sceneName} não deve mostrar a barra.");
        }
    }

    protected override void UpdateButtonVisibility(string sceneName)
    {
        Debug.Log($"[BottomBar] UpdateButtonVisibility chamado para: {sceneName}");
        UpdateButtonDisplay(sceneName);
    }

    public void UpdateButtonDisplay(string sceneName)
    {
        Debug.Log($"[BottomBar] UpdateButtonDisplay chamado para: {sceneName}");

        foreach (var button in allButtons)
        {
            if (button != null && button.normalIcon != null && button.filledIcon != null)
            {
                bool isActiveButton = (button.targetScene == sceneName);
                button.normalIcon.gameObject.SetActive(!isActiveButton);
                button.filledIcon.gameObject.SetActive(isActiveButton);

                Debug.Log($"[BottomBar] Botão {button.buttonName}: normalIcon={!isActiveButton}, filledIcon={isActiveButton}");
            }
            else
            {
                Debug.LogWarning($"[BottomBar] Botão com referência nula ou ícones nulos!");
            }
        }
    }

    public void AddSceneWithoutBottomBar(string sceneName)
    {
        Debug.Log($"[BottomBar] Adicionando cena sem barra: {sceneName}");
        AddSceneWithoutBar(sceneName);
    }

    public void RemoveSceneWithoutBottomBar(string sceneName)
    {
        Debug.Log($"[BottomBar] Removendo cena sem barra: {sceneName}");
        RemoveSceneWithoutBar(sceneName);
    }

    private System.Collections.IEnumerator ReinitializeForMobile(string sceneName)
    {
        Debug.Log($"[BottomBar] ReinitializeForMobile iniciado para: {sceneName}");

        yield return null;
        Debug.Log("[BottomBar] ReinitializeForMobile: Aguardado 1 frame");

        yield return null;
        Debug.Log("[BottomBar] ReinitializeForMobile: Aguardado 2 frames");

        yield return null;
        Debug.Log("[BottomBar] ReinitializeForMobile: Aguardado 3 frames");

        // _navigation já está garantido pelo AppContext.OnReady no BarsManager
        Debug.Log("[BottomBar] Reinicializando botões...");
        SetupButtonListeners();

        foreach (var btn in allButtons)
        {
            if (btn.button != null)
                btn.button.interactable = true;
        }

        Debug.Log($"[BottomBar] Atualizando display após reinicialização para: {sceneName}");
        UpdateButtonDisplay(sceneName);  
    }

    protected override void OnCleanup()
    {
        Debug.Log("[BottomBar] OnCleanup chamado");
        SceneManager.sceneLoaded -= OnSceneLoadedDirect;
        Debug.Log("[BottomBar] Listener OnSceneLoadedDirect removido");        
        base.OnCleanup();
    }
}