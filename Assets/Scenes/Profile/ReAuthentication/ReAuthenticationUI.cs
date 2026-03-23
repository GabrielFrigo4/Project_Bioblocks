using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReAuthenticationUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private CanvasGroup reAuthCanvasGroup;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button authenticateButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI authenticateButtonText;

    [Header("References")]
    [SerializeField] private CanvasGroup deleteAccountCanvasGroup;

    private System.Action onReauthenticationSuccess;
    private IAuthRepository _auth;

    private void Awake()
    {
        HideReAuthPanel();

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.Log("Canvas não encontrado no parent, adicionando ao gameObject atual");
            canvas = gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;
        canvas.sortingOrder = 1000;

        GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.Log("GraphicRaycaster não encontrado, adicionando um");
            raycaster = gameObject.AddComponent<GraphicRaycaster>();
        }

        Debug.Log($"ReAuthenticationUI inicializado com Canvas.sortingOrder={canvas.sortingOrder}");
    }

    private void Start()
    {
        _auth = AppContext.Auth;
        Debug.Log("ReAuthenticationUI inicializado");

        if (reAuthCanvasGroup == null)
        {
            Debug.LogError("reAuthCanvasGroup não está configurado!");
            return;
        }

        if (authenticateButton != null)
        {
            authenticateButton.onClick.AddListener(OnAuthenticateClick);
            Debug.Log("Botão de autenticação configurado");
        }
        else
        {
            Debug.LogError("Botão de autenticação não encontrado!");
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(OnCancelClick);
            Debug.Log("Botão de cancelamento configurado");
        }
        else
        {
            Debug.LogError("Botão de cancelamento não encontrado!");
        }
    }

    public void ShowReAuthPanel(string userEmail, System.Action onSuccess)
    {
        Debug.Log($"ShowReAuthPanel chamado para email: {userEmail}");
        onReauthenticationSuccess = onSuccess;

        Canvas reAuthCanvas = GetComponent<Canvas>();
        if (reAuthCanvas != null)
        {
            reAuthCanvas.overrideSorting = true;
            if (reAuthCanvas.sortingOrder < 200)
            {
                reAuthCanvas.sortingOrder = 200;
            }
            Debug.Log($"ReAuthUI usando Canvas com sortingOrder {reAuthCanvas.sortingOrder}");
        }

        // Se há um CanvasGroup, configure-o para ser interativo e visível
        if (reAuthCanvasGroup != null)
        {
            reAuthCanvasGroup.alpha = 1;
            reAuthCanvasGroup.interactable = true;
            reAuthCanvasGroup.blocksRaycasts = true;
        }

        // Configurar UI
        if (emailInput != null)
        {
            emailInput.text = userEmail;
            emailInput.interactable = false;
        }

        if (passwordInput != null)
        {
            passwordInput.text = "";
            passwordInput.interactable = true;
        }

        if (errorText != null)
        {
            errorText.text = "";
        }

        if (authenticateButtonText != null)
        {
            authenticateButtonText.text = "Confirmar";
        }

        // Verificar se os botões estão interativos
        if (authenticateButton != null)
        {
            authenticateButton.interactable = true;
        }

        if (cancelButton != null)
        {
            cancelButton.interactable = true;
        }

        // Focar no campo de senha
        if (passwordInput != null)
        {
            passwordInput.Select();
            passwordInput.ActivateInputField();
        }

        Debug.Log("Painel de reautenticação mostrado");
    }

    private void HideReAuthPanel()
    {
        if (reAuthCanvasGroup != null)
        {
            reAuthCanvasGroup.alpha = 0;
            reAuthCanvasGroup.interactable = false;
            reAuthCanvasGroup.blocksRaycasts = false;
        }
    }

    public async void OnAuthenticateClick()
    {
        Debug.Log("OnAuthenticateClick chamado");
        LoadingSpinnerComponent.Instance.ShowSpinner();

        if (passwordInput == null || string.IsNullOrEmpty(passwordInput.text))
        {
            if (errorText != null)
            {
                errorText.text = "Por favor, insira sua senha";
            }
            LoadingSpinnerComponent.Instance.HideSpinner();
            return;
        }

        try
        {
            if (authenticateButton != null) authenticateButton.interactable = false;
            if (authenticateButtonText != null) authenticateButtonText.text = "Autenticando...";
            if (errorText != null) errorText.text = "";

            await _auth.ReauthenticateUser(emailInput.text, passwordInput.text);
            Debug.Log("Reautenticação bem-sucedida");

            HideReAuthPanel();

            if (onReauthenticationSuccess != null)
            {
                onReauthenticationSuccess.Invoke();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Erro na reautenticação: {ex.Message}");
            if (errorText != null) errorText.text = "Senha incorreta. Por favor, tente novamente.";
            if (authenticateButton != null) authenticateButton.interactable = true;
            if (authenticateButtonText != null) authenticateButtonText.text = "Confirmar";
            LoadingSpinnerComponent.Instance.HideSpinner();
        }
    }

    public void OnCancelClick()
    {
        Debug.Log("OnCancelClick chamado");
        HideReAuthPanel();
        LoadingSpinnerComponent.Instance.HideSpinner(); 
        
        GameObject deleteAccountDarkOverlay = GameObject.Find("DeleteAccountDarkOverlay");
        if (deleteAccountDarkOverlay != null)
        {
            Canvas overlayCanvas = deleteAccountDarkOverlay.GetComponent<Canvas>();
            if (overlayCanvas != null)
            {
                overlayCanvas.sortingOrder = 109; 
            }
        }
    }

    private void OnDestroy()
    {
        if (authenticateButton != null)
            authenticateButton.onClick.RemoveListener(OnAuthenticateClick);

        if (cancelButton != null)
            cancelButton.onClick.RemoveListener(OnCancelClick);
    }
}