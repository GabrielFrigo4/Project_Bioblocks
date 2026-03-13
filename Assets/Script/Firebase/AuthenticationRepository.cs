using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Firestore;

public class AuthenticationRepository : MonoBehaviour
{
    private static AuthenticationRepository _instance;
    private FirebaseAuth auth;
    private bool isInitialized;
    public bool IsInitialized => isInitialized;
    private FirebaseFirestore db;

    public FirebaseAuth Auth => auth;

    public static AuthenticationRepository Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<AuthenticationRepository>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AuthenticationManager");
                    _instance = go.AddComponent<AuthenticationRepository>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public async Task InitializeAsync()
    {
        if (isInitialized) return;

        try
        {
            var app = Firebase.FirebaseApp.DefaultInstance;
            if (app == null)
            {
                Debug.Log("Creating Firebase App");
                app = Firebase.FirebaseApp.Create();
            }

            auth = FirebaseAuth.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;

            isInitialized = true;
            Debug.Log("Firebase Authentication initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Firebase initialization failed: {e.Message}");
            throw;
        }
    }

    public async Task<UserData> SignInWithEmailAsync(string email, string password)
    {
        if (!isInitialized) throw new System.Exception("Firebase não inicializado");
        if (auth == null) throw new System.Exception("FirebaseAuth não inicializado");

        try
        {
            var result = await auth.SignInWithEmailAndPasswordAsync(email, password);
            if (result != null && result.User != null)
            {
                string uid = result.User.UserId;
                UserData userData = await FirestoreRepository.Instance.GetUserData(uid);
                if (userData != null)
                {
                    UserDataStore.CurrentUserData = userData;
                    return userData;
                }
                throw new System.Exception("Dados do usuário não encontrados");
            }
            throw new System.Exception("Login falhou: resultado ou usuário nulo");
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception during login: {e.Message}");
            throw;
        }
    }

    public async Task<UserData> RegisterUserAsync(string name, string nickName, string email, string password)
    {
        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

            Dictionary<string, object> userData = new Dictionary<string, object>
            {
                { "UserId", result.User.UserId },
                { "NickName", nickName },
                { "Name", name },
                { "Email", email },
                { "ProfileImageUrl", "" },
                { "Score", 0 },
                { "Progress", 0 },
                { "CreatedTime", Timestamp.FromDateTime(DateTime.UtcNow) },
                { "IsUserRegistered", true },
                { "AnsweredQuestions", new Dictionary<string, List<int>>() }
            };

            await db.Collection("Users").Document(result.User.UserId).SetAsync(userData);

            var user = new UserData
            {
                UserId = result.User.UserId,
                NickName = nickName,
                Name = name,
                Email = email,
                ProfileImageUrl = "",
                Score = 0,
                QuestionTypeProgress = 0,
                CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow),
                IsUserRegistered = true,
                AnsweredQuestions = new Dictionary<string, List<int>>()
            };

            UserDataStore.CurrentUserData = user;
            return user;
        }
        catch (Exception e)
        {
            Debug.LogError($"Registration failed: {e.Message}");
            throw;
        }
    }

    public bool IsUserLoggedIn()
    {
        var user = auth?.CurrentUser;
        return user != null && !user.IsAnonymous;
    }

    public async Task LogoutAsync()
    {
        try
        {
            if (!isInitialized) throw new System.Exception("Firebase não inicializado");
            auth.SignOut();
            await Task.CompletedTask;
            Debug.Log("Usuário deslogado com sucesso");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao fazer logout: {e.Message}");
            throw;
        }
    }

    public async Task CheckAuthenticationStatus()
    {
        try
        {
            if (!isInitialized) throw new System.Exception("Firebase não inicializado");

            var user = auth.CurrentUser;
            if (user == null) throw new System.Exception("Usuário não está autenticado");

            await user.ReloadAsync();
            string token = await user.TokenAsync(true);

            if (string.IsNullOrEmpty(token))
            {
                throw new ReauthenticationRequiredException("Token inválido");
            }

            Debug.Log("Token atualizado com sucesso");
        }
        catch (Firebase.FirebaseException e) when (e.Message.Contains("recent authentication"))
        {
            Debug.LogError("É necessário reautenticar");
            throw new ReauthenticationRequiredException("É necessário reautenticar para prosseguir");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao verificar status de autenticação: {e.Message}");
            throw;
        }
    }

    public async Task DeleteUser(string userId)
    {
        try
        {
            if (!isInitialized) throw new System.Exception("Firebase não inicializado");

            var user = auth.CurrentUser;
            if (user == null) throw new System.Exception("Usuário não está autenticado");

            await user.ReloadAsync();
            string token = await user.TokenAsync(true);
            if (string.IsNullOrEmpty(token))
            {
                throw new ReauthenticationRequiredException("Token inválido");
            }

            Debug.Log("Token atualizado, tentando deletar usuário...");
            await user.DeleteAsync();
            Debug.Log("Usuário deletado com sucesso do Authentication");
        }
        catch (Firebase.FirebaseException e) when (e.Message.Contains("requires recent authentication"))
        {
            Debug.LogError("É necessário reautenticar para deletar o usuário");
            throw new ReauthenticationRequiredException("É necessário reautenticar para deletar a conta");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao deletar usuário: {e.Message}");
            throw;
        }
    }

    public async Task ReauthenticateUser(string email, string password)
    {
        try
        {
            if (!isInitialized) throw new System.Exception("Firebase não inicializado");
            if (auth == null) throw new System.Exception("FirebaseAuth não inicializado");

            var user = auth.CurrentUser;
            if (user == null) throw new Exception("Usuário não está autenticado");

            Credential credential = EmailAuthProvider.GetCredential(email, password);
            await user.ReauthenticateAsync(credential);
            Debug.Log("Usuário reautenticado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro na reautenticação: {e.Message}");
            throw;
        }
    }
}
