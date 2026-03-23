using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private FeedbackManager feedbackManager;
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    // -------------------------------------------------------
    // Dependências — obtidas do AppContext no Start()
    // -------------------------------------------------------
    private IAuthRepository _auth;

    private Dictionary<string, LoginAttempt> loginAttempts = new Dictionary<string, LoginAttempt>();
    private const int MAX_ATTEMPTS = 5;
    private const int LOCKOUT_MINUTES = 15;
    private bool isProcessing = false;

    private void Start()
    {
        _auth = AppContext.Auth;

        loginButton.onClick.AddListener(HandleLogin);
        registerButton.onClick.AddListener(HandleRegisterNavigation);
        InvokeRepeating(nameof(CleanupOldAttempts), 0f, 86400f);
    }

    // -------------------------------------------------------
    // Login
    // -------------------------------------------------------

    public async void HandleLogin()
    {
        if (isProcessing) return;

        if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            feedbackManager.ShowFeedback("Email e senha são obrigatórios.", true);
            return;
        }

        string email = emailInput.text.Trim().ToLower();

        if (!CheckRateLimit(email)) return;

        isProcessing = true;
        SetButtonsInteractable(false);
        feedbackManager.ShowFeedback("", false);
        loadingSpinner?.ShowSpinner();

        try
        {
            var userData = await _auth.SignInWithEmailAsync(email, passwordInput.text);

            if (userData == null)
                throw new Exception("Login falhou: dados do usuário nulos");

            if (loginAttempts.ContainsKey(email))
                loginAttempts[email].Reset();

            UserDataStore.CurrentUserData = userData;

            await AnsweredQuestionsManager.Instance.ForceUpdate();

            loadingSpinner?.ShowSpinnerUntilSceneLoaded("PathwayScene");
            SceneManager.LoadScene("PathwayScene");
        }
        catch (FirebaseException e)
        {
            if (!loginAttempts.ContainsKey(email))
                loginAttempts[email] = new LoginAttempt();

            loginAttempts[email].IncrementAttempt();
            feedbackManager.ShowFeedback(GetFirebaseAuthErrorMessage(e), true);
            Debug.LogError($"{e.ErrorCode}, Message: {e.Message}");
            loadingSpinner?.HideSpinner();
            SetButtonsInteractable(true);
            isProcessing = false;
        }
        catch (Exception e)
        {
            feedbackManager.ShowFeedback(e.Message, true);
            Debug.LogError(e.Message);
            loadingSpinner?.HideSpinner();
            SetButtonsInteractable(true);
            isProcessing = false;
        }
    }

    // -------------------------------------------------------
    // Navegação
    // -------------------------------------------------------

    public void HandleRegisterNavigation()
    {
        loadingSpinner?.ShowSpinnerUntilSceneLoaded("RegisterView");
        SceneManager.LoadScene("RegisterView");
    }

    // -------------------------------------------------------
    // UI helpers
    // -------------------------------------------------------

    private void SetButtonsInteractable(bool interactable)
    {
        loginButton.interactable = interactable;
        registerButton.interactable = interactable;
        emailInput.interactable = interactable;
        passwordInput.interactable = interactable;
    }

    // -------------------------------------------------------
    // Rate limiting
    // -------------------------------------------------------

    private bool CheckRateLimit(string email)
    {
        if (!loginAttempts.ContainsKey(email))
            loginAttempts[email] = new LoginAttempt();

        var attempt = loginAttempts[email];

        if (attempt.IsLockedOut)
        {
            var remainingMinutes = Math.Ceiling((attempt.LockoutUntil.Value - DateTime.Now).TotalMinutes);
            feedbackManager.ShowFeedback($"Tente novamente em {remainingMinutes} minutos.", true);
            return false;
        }

        return true;
    }

    private void CleanupOldAttempts()
    {
        var now = DateTime.Now;
        var keysToRemove = new List<string>();

        foreach (var kvp in loginAttempts)
        {
            if ((now - kvp.Value.LastAttempt).TotalHours > 24)
                keysToRemove.Add(kvp.Key);
        }

        foreach (var key in keysToRemove)
            loginAttempts.Remove(key);
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
            (int)AuthError.UserNotFound       => "Usuário não encontrado.",
            (int)AuthError.WrongPassword      => "Email e/ou Senha incorretos.",
            (int)AuthError.InvalidEmail       => "Email inválido.",
            (int)AuthError.NetworkRequestFailed => "Falha na conexão com a internet.",
            _                                 => e.Message
        };
    }

    // -------------------------------------------------------
    // LoginAttempt — controle de tentativas por email
    // -------------------------------------------------------

    private class LoginAttempt
    {
        public int Attempts { get; set; }
        public DateTime LastAttempt { get; set; }
        public DateTime? LockoutUntil { get; set; }

        public bool IsLockedOut => LockoutUntil.HasValue && DateTime.Now < LockoutUntil.Value;

        public void IncrementAttempt()
        {
            Attempts++;
            LastAttempt = DateTime.Now;

            if (Attempts >= MAX_ATTEMPTS)
                LockoutUntil = DateTime.Now.AddMinutes(LOCKOUT_MINUTES);
        }

        public void Reset()
        {
            Attempts = 0;
            LockoutUntil = null;
        }
    }
}