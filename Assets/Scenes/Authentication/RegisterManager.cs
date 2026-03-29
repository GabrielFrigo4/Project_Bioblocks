using Firebase;
using Firebase.Auth;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class RegisterManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button backButton;
    [SerializeField] private FeedbackManager feedbackManager;
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    // -------------------------------------------------------
    // Dependências — obtidas do AppContext no Start()
    // -------------------------------------------------------
    private IAuthRepository _auth;
    private IFirestoreRepository _firestore;

    private bool isProcessing = false;

    private void Start()
    {
        _auth      = AppContext.Auth;
        _firestore = AppContext.Firestore;

        nickNameInput.contentType = TMP_InputField.ContentType.Standard;
        nickNameInput.characterLimit = 15;
        nickNameInput.onValueChanged.AddListener(ValidateNickname);
        registerButton.onClick.AddListener(HandleRegistration);
    }

    // -------------------------------------------------------
    // Registro
    // -------------------------------------------------------

    public async void HandleRegistration()
    {
        if (isProcessing) return;

        isProcessing = true;
        SetAllButtonsInteractable(false);
        loadingSpinner?.ShowSpinner();

        try
        {
            if (string.IsNullOrEmpty(nickNameInput.text) ||
                string.IsNullOrEmpty(nameInput.text)     ||
                string.IsNullOrEmpty(emailInput.text)    ||
                string.IsNullOrEmpty(passwordInput.text))
            {
                throw new Exception("Todos os campos são obrigatórios.");
            }

            // Verifica nickname usando o repositório — sem acesso direto ao Firestore
            bool nicknameExists = await _firestore.AreNicknameTaken(nickNameInput.text);
            if (nicknameExists)
                throw new Exception("Este nickname já está em uso. Por favor, escolha outro.");

            Debug.Log("=== LIMPEZA ANTES DE REGISTRAR NOVO USUÁRIO ===");

            UserDataStore.CurrentUserData = null;
            Debug.Log("✓ UserDataStore limpo");
            AppContext.AnsweredQuestions?.ResetManager();
            AnsweredQuestionsListStore.ClearAll();
            Debug.Log("=== LIMPEZA CONCLUÍDA, INICIANDO REGISTRO ===");

            await _auth.RegisterUserAsync(
                nameInput.text,
                nickNameInput.text,
                emailInput.text,
                passwordInput.text
            );

            await Task.Delay(300);

            // Busca os dados do usuário recém-criado usando o userId do Auth
            string userId = _auth.CurrentUserId;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Erro: usuário criado mas ID não encontrado.");

            var userData = await _firestore.GetUserData(userId);
            if (userData == null)
                throw new Exception("Erro ao carregar dados do usuário recém-criado.");

            UserDataStore.CurrentUserData = userData;
            await Task.Delay(300);
            await AppContext.AnsweredQuestions?.ForceUpdate();
            loadingSpinner?.ShowSpinnerUntilSceneLoaded("PathwayScene");
            SceneManager.LoadScene("PathwayScene");
        }
        catch (FirebaseException e)
        {
            string errorMessage = GetFirebaseAuthErrorMessage(e);
            feedbackManager.ShowFeedback(errorMessage, true);
            Debug.LogError(errorMessage);
            loadingSpinner?.HideSpinner();
            SetAllButtonsInteractable(true);
            isProcessing = false;
        }
        catch (Exception e)
        {
            feedbackManager.ShowFeedback(e.Message, true);
            Debug.LogError(e.Message);
            loadingSpinner?.HideSpinner();
            SetAllButtonsInteractable(true);
            isProcessing = false;
        }
    }

    // -------------------------------------------------------
    // Navegação
    // -------------------------------------------------------

    public void SceneLoader()
    {
        if (isProcessing) return;

        isProcessing = true;
        SetAllButtonsInteractable(false);
        loadingSpinner?.ShowSpinnerUntilSceneLoaded("LoginView");
        SceneManager.LoadScene("LoginView");
    }

    // -------------------------------------------------------
    // Validação
    // -------------------------------------------------------

    private void ValidateNickname(string value)
    {
        if (value.Length < 3)
            feedbackManager.ShowFeedback("Nickname deve possuir mais de 3 caracteres.", true);
        else
            feedbackManager.HideFeedback();
    }

    // -------------------------------------------------------
    // UI helpers
    // -------------------------------------------------------

    private void SetAllButtonsInteractable(bool interactable)
    {
        registerButton.interactable = interactable;
        if (backButton != null) backButton.interactable = interactable;
        nameInput.interactable     = interactable;
        nickNameInput.interactable = interactable;
        emailInput.interactable    = interactable;
        passwordInput.interactable = interactable;
    }

    // -------------------------------------------------------
    // Tradução de erros Firebase
    // Isolado aqui — se o SDK mudar, só este método é afetado
    // -------------------------------------------------------

    private string GetFirebaseAuthErrorMessage(FirebaseException e)
    {
        var errorCode = (int)e.ErrorCode;
        return errorCode switch
        {
            (int)AuthError.EmailAlreadyInUse => "Email já registrado.",
            (int)AuthError.WeakPassword      => "Senha muito fraca.",
            _                                => e.Message
        };
    }
}