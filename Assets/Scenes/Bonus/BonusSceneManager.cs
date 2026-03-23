using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class BonusSceneManager : MonoBehaviour
{
    [Header("Bonus UI Mappings")]
    [SerializeField] private List<BonusUIElements> bonusUIMappings = new List<BonusUIElements>();

    [Header("Bonus Especial")]
    [SerializeField] private TextMeshProUGUI bonusCountBE;
    [SerializeField] private TextMeshProUGUI isBonusActiveBE;
    [SerializeField] private Button bonusButtonBE;

    [Header("Bonus das Listas")]
    [SerializeField] private TextMeshProUGUI bonusCountBL;
    [SerializeField] private TextMeshProUGUI isBonusActiveBL;
    [SerializeField] private Button bonusButtonBL;

    [Header("Bonus Incansável")]
    [SerializeField] private TextMeshProUGUI bonusCountBI;
    [SerializeField] private TextMeshProUGUI isBonusActiveBI;
    [SerializeField] private Button bonusButtonBI;

    [Header("Bonus Especial Pro")]
    [SerializeField] private TextMeshProUGUI bonusCountBEPro;
    [SerializeField] private TextMeshProUGUI isBonusActiveBEPro;
    [SerializeField] private Button bonusButtonBEPro;

    [Header("Bonus das Listas Pro")]
    [SerializeField] private TextMeshProUGUI bonusCountBLPro;
    [SerializeField] private TextMeshProUGUI isBonusActiveBLPro;
    [SerializeField] private Button bonusButtonBLPro;

    [Header("Bonus Incansável Pro")]
    [SerializeField] private TextMeshProUGUI bonusCountBIPro;
    [SerializeField] private TextMeshProUGUI isBonusActiveBIPro;
    [SerializeField] private Button bonusButtonBIPro;

    [Header("Visual Settings")]
    [SerializeField] private float inactiveAlpha = 0.6f;
    [SerializeField] private bool useGrayscaleWhenInactive = false;
    private UserHeaderManager _userHeaderManager;
    

    private readonly Dictionary<string, BonusConfig> bonusConfigs = new Dictionary<string, BonusConfig>()
    {
        { "specialBonus", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 5 } },
        { "listCompletionBonus", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 1 } },
        { "persistenceBonus", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 1 } },
        { "specialBonusPro", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 5 } },
        { "listCompletionBonusPro", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 1 } },
        { "persistenceBonusPro", new BonusConfig { duration = 600f, multiplier = 3, thresholdCount = 1 } }
    };

    private UserBonusManager userBonusManager;
    private string userId;
    private bool isInitialized = false;

    private void Awake()
    {
        userBonusManager = new UserBonusManager();

        if (bonusUIMappings.Count == 0)
        {
            SetupDefaultBonusMappings();
        }
    }

    private void SetupDefaultBonusMappings()
    {
        bonusUIMappings = new List<BonusUIElements>
        {
            new BonusUIElements
            {
                bonusFirestoreName = "specialBonus",
                bonusCountText = bonusCountBE,
                isBonusActiveText = isBonusActiveBE,
                bonusButton = bonusButtonBE,
                bonusContainer = bonusButtonBE?.gameObject,
                bonusTitle = "Ativar Special Bonus",
                bonusMessage = "Você terá xp triplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            },
            new BonusUIElements
            {
                bonusFirestoreName = "listCompletionBonus",
                bonusCountText = bonusCountBL,
                isBonusActiveText = isBonusActiveBL,
                bonusButton = bonusButtonBL,
                bonusContainer = bonusButtonBL?.gameObject,
                bonusTitle = "Ativar Bonus das Listas",
                bonusMessage = "Você terá xp duplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            },
            new BonusUIElements
            {
                bonusFirestoreName = "persistenceBonus",
                bonusCountText = bonusCountBI,
                isBonusActiveText = isBonusActiveBI,
                bonusButton = bonusButtonBI,
                bonusContainer = bonusButtonBI?.gameObject,
                bonusTitle = "Ativar Bonus Incansável",
                bonusMessage = "Você terá xp duplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            },
            new BonusUIElements
            {
                bonusFirestoreName = "specialBonusPro",
                bonusCountText = bonusCountBEPro,
                isBonusActiveText = isBonusActiveBEPro,
                bonusButton = bonusButtonBEPro,
                bonusContainer = bonusButtonBEPro?.gameObject,
                bonusTitle = "Ativar Special Bonus Pro",
                bonusMessage = "Você terá xp triplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            },
            new BonusUIElements
            {
                bonusFirestoreName = "listCompletionBonusPro",
                bonusCountText = bonusCountBLPro,
                isBonusActiveText = isBonusActiveBLPro,
                bonusButton = bonusButtonBLPro,
                bonusContainer = bonusButtonBLPro?.gameObject,
                bonusTitle = "Ativar Bonus das Listas Pro",
                bonusMessage = "Você terá xp duplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            },
            new BonusUIElements
            {
                bonusFirestoreName = "persistenceBonusPro",
                bonusCountText = bonusCountBIPro,
                isBonusActiveText = isBonusActiveBIPro,
                bonusButton = bonusButtonBIPro,
                bonusContainer = bonusButtonBIPro?.gameObject,
                bonusTitle = "Ativar Bonus Incansável Pro",
                bonusMessage = "Você terá xp duplicada por 10 min.\nPoderá ser cumulativo se já existir um bonus em uso.\nDeseja ativar o bonus agora?"
            }
        };
    }

    private void OnEnable()
    {
        InitializeAndFetchBonus();
        HalfViewRegistry.OnAnyHalfViewHidden += OnAnyHalfViewHidden;
    }

    private void OnDisable()
    {
        if (isInitialized)
        {
            StopListeningForBonusUpdates();
        }

        HalfViewRegistry.OnAnyHalfViewHidden -= OnAnyHalfViewHidden;
    }

    private void OnAnyHalfViewHidden()
    {
        ForceUpdateUIAndReactivateButton();
    }

    private async void InitializeAndFetchBonus()
    {
        if (UserDataStore.CurrentUserData != null && !string.IsNullOrEmpty(UserDataStore.CurrentUserData.UserId))
        {
            userId = UserDataStore.CurrentUserData.UserId;
            await FetchBonuses();
            StartListeningForBonusUpdates();
            isInitialized = true;
        }
        else
        {
            Debug.LogWarning("BonusSceneManager: Usuário não está logado");
        }
    }

    public async Task FetchBonuses()
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("BonusSceneManager: UserId não definido");
            return;
        }

        try
        {
            List<BonusType> userBonuses = await userBonusManager.GetUserBonuses(userId);
            UpdateBonusUI(userBonuses);
        }
        catch (Exception e)
        {
            Debug.LogError($"BonusSceneManager: Erro ao buscar bônus: {e.Message}");
        }
    }

    private void UpdateBonusUI(List<BonusType> bonuses)
    {
        foreach (var bonusUIMapping in bonusUIMappings)
        {
            BonusType matchingBonus = bonuses.FirstOrDefault(b =>
                b.BonusName == bonusUIMapping.bonusFirestoreName);

            bool isButtonInteractable = false;
            int count = 0;

            if (matchingBonus != null)
            {
                count = matchingBonus.BonusCount;
                isButtonInteractable = count > 0;

                if (bonusUIMapping.bonusCountText != null)
                {
                    bonusUIMapping.bonusCountText.text = count.ToString();
                }

                if (bonusUIMapping.isBonusActiveText != null)
                {
                    bonusUIMapping.isBonusActiveText.text = matchingBonus.IsBonusActive ? "Ativo" : "Inativo";
                }
            }
            else
            {
                if (bonusUIMapping.bonusCountText != null)
                {
                    bonusUIMapping.bonusCountText.text = "0";
                }

                if (bonusUIMapping.isBonusActiveText != null)
                {
                    bonusUIMapping.isBonusActiveText.text = "Inativo";
                }
            }

            if (bonusUIMapping.bonusButton != null)
            {
                bonusUIMapping.bonusButton.interactable = isButtonInteractable;
            }

            UpdateBonusVisualState(bonusUIMapping.bonusContainer, isButtonInteractable);
        }
    }

    private void UpdateBonusVisualState(GameObject container, bool isActive)
    {
        if (container == null) return;

        CanvasGroup canvasGroup = container.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = container.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = isActive ? 1f : inactiveAlpha;

        if (useGrayscaleWhenInactive)
        {
            Image[] images = container.GetComponentsInChildren<Image>();
            foreach (var img in images)
            {
                if (img != null)
                {
                    img.color = isActive ? Color.white : Color.gray;
                }
            }
        }
    }

    private void Start()
    {
        _userHeaderManager = FindFirstObjectByType<UserHeaderManager>();
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        foreach (var bonusUI in bonusUIMappings)
        {
            if (bonusUI.bonusButton == null) continue;

            bonusUI.bonusButton.onClick.RemoveAllListeners();
            bonusUI.bonusButton.onClick.AddListener(() =>
            {
                ShowBonusConfirmation(bonusUI.bonusFirestoreName);
            });
        }
    }

    private void ShowBonusConfirmation(string bonusName)
    {
        BonusUIElements bonusUI = bonusUIMappings.FirstOrDefault(b => b.bonusFirestoreName == bonusName);
        if (bonusUI != null && bonusUI.bonusButton != null)
        {
            bonusUI.bonusButton.interactable = false;
        }

        string currentScene = SceneManager.GetActiveScene().name;
        HalfViewComponent halfView = HalfViewRegistry.GetHalfViewForScene(currentScene);

        if (halfView == null)
        {
            halfView = HalfViewRegistry.EnsureHalfViewInCurrentScene();
        }

        if (halfView != null)
        {
            halfView.HideMenu();
            StartCoroutine(ConfigureBonusHalfViewAfterFrame(halfView, bonusName));
        }
        else
        {
            Debug.LogError($"Não foi possível criar o HalfViewComponent. Ativando {bonusName} diretamente.");
            _ = ActivateBonus(bonusName);
        }
    }

    private IEnumerator ConfigureBonusHalfViewAfterFrame(HalfViewComponent halfView, string bonusName)
    {
        yield return null;
        var preventReconfigField = typeof(HalfViewComponent).GetField("preventButtonReconfiguration",
                                                                    System.Reflection.BindingFlags.NonPublic |
                                                                    System.Reflection.BindingFlags.Instance);
        if (preventReconfigField != null)
        {
            preventReconfigField.SetValue(halfView, true);
        }

        BonusUIElements bonusUI = bonusUIMappings.FirstOrDefault(b => b.bonusFirestoreName == bonusName);
        if (bonusUI == null)
        {
            Debug.LogError($"Mapeamento não encontrado para {bonusName}");
            yield break;
        }

        halfView.OnCancelled -= OnHalfViewCancelled;
        halfView.OnCancelled += OnHalfViewCancelled;
        halfView.SetTitle(bonusUI.bonusTitle);
        halfView.SetMessage(bonusUI.bonusMessage);

        halfView.SetPrimaryButton("Cancelar", () =>
        {
            CancelBonusActivation(bonusName);
        });

        halfView.SetSecondaryButton("Ativar Bonus", () =>
        {
            ActivateBonusFromButton(bonusName);
        });

        halfView.ShowMenu();
    }

    public void CancelBonusActivation(string bonusName)
    {
        ForceUpdateUIAndReactivateButton();
    }

    public async void ActivateBonusFromButton(string bonusName)
    {
        try
        {
            if (!bonusConfigs.TryGetValue(bonusName, out BonusConfig config))
            {
                Debug.LogError($"Configuração não encontrada para {bonusName}");
                return;
            }

            await userBonusManager.ConsumeBonusAndActivate(userId, bonusName, config.duration, config.multiplier);
            await FetchBonuses();
            
            if (_userHeaderManager != null)
            {
                _userHeaderManager.RefreshActiveBonuses();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao ativar bônus {bonusName}: {e.Message}");
            ForceUpdateUIAndReactivateButton();
        }
    }

    private async Task ActivateBonus(string bonusName)
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("BonusSceneManager: UserId não definido");
            return;
        }

        try
        {
            if (!bonusConfigs.TryGetValue(bonusName, out BonusConfig config))
            {
                Debug.LogError($"Configuração não encontrada para {bonusName}");
                return;
            }

            await userBonusManager.ConsumeBonusAndActivate(userId, bonusName, config.duration, config.multiplier);
            
            if (_userHeaderManager != null)
            {
                _userHeaderManager.RefreshActiveBonuses();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"BonusSceneManager: Erro ao ativar {bonusName}: {e.Message}");
            throw;
        }
    }

    private void OnHalfViewCancelled()
    {
        ForceUpdateUIAndReactivateButton();
    }

    private void ForceUpdateUIAndReactivateButton()
    {
        StartCoroutine(FetchAndUpdateUI());
    }

    private IEnumerator FetchAndUpdateUI()
    {
        var fetchTask = userBonusManager.GetUserBonuses(userId);
        while (!fetchTask.IsCompleted)
        {
            yield return null;
        }

        if (fetchTask.IsFaulted || fetchTask.IsCanceled)
        {
            Debug.LogError($"Erro ao buscar dados do usuário: {fetchTask.Exception?.Message}");
            FallbackReactivateButton();
        }
        else
        {
            try
            {
                List<BonusType> bonuses = fetchTask.Result;
                UpdateBonusUI(bonuses);
                ReactivateButtonsIfNeeded(bonuses);
            }
            catch (Exception e)
            {
                Debug.LogError($"Erro ao processar resultados: {e.Message}");
                FallbackReactivateButton();
            }
        }
    }

    private void FallbackReactivateButton()
    {
        foreach (var mapping in bonusUIMappings)
        {
            if (mapping.bonusButton != null)
            {
                mapping.bonusButton.interactable = true;
            }
        }
    }

    private void ReactivateButtonsIfNeeded(List<BonusType> bonuses)
    {
        foreach (var bonusConfig in bonusConfigs)
        {
            string bonusName = bonusConfig.Key;
            BonusConfig config = bonusConfig.Value;

            BonusType bonus = bonuses.FirstOrDefault(b => b.BonusName == bonusName);
            if (bonus != null && bonus.BonusCount >= config.thresholdCount)
            {
                BonusUIElements bonusUI = bonusUIMappings.FirstOrDefault(b => b.bonusFirestoreName == bonusName);
                if (bonusUI != null && bonusUI.bonusButton != null)
                {
                    bonusUI.bonusButton.interactable = true;
                }
            }
        }
    }

    private void StartListeningForBonusUpdates()
    {
        StartCoroutine(BonusUpdatePollingCoroutine());
    }

    private IEnumerator BonusUpdatePollingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (!isInitialized || string.IsNullOrEmpty(userId))
            {
                yield break;
            }

            _ = FetchBonuses();
        }
    }

    private void StopListeningForBonusUpdates()
    {
        StopAllCoroutines();
    }

    public async void RefreshBonusUI()
    {
        await FetchBonuses();
    }
}