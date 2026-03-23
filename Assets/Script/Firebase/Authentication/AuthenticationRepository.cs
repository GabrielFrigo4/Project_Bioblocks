using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Firestore;

/// <summary>
/// Implementação real do IAuthRepository usando Firebase Authentication.
/// </summary>
public class AuthenticationRepository : MonoBehaviour, IAuthRepository
{
    private FirebaseAuth _auth;
    private IFirestoreRepository _firestore;
    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    /// <summary>
    /// UserId do usuário logado, ou null se não houver sessão ativa.
    /// Não expõe FirebaseUser — apenas a string que o resto do app precisa.
    /// </summary>
    public string CurrentUserId => _auth?.CurrentUser?.UserId;

    // -------------------------------------------------------
    // Inicialização
    // -------------------------------------------------------

    /// <summary>
    /// Chamado pelo AppContext depois que o Firebase já está disponível.
    /// O IFirestoreRepository é passado aqui para evitar dependência circular
    /// (Auth precisa do Firestore para buscar UserData após login).
    /// </summary>
    public void InjectDependencies(IFirestoreRepository firestore)
    {
        _firestore = firestore;
    }

    public async Task InitializeAsync()
    {
        if (isInitialized) return;

        try
        {
            var app = Firebase.FirebaseApp.DefaultInstance;
            if (app == null)
            {
                Debug.Log("[AuthRepository] Creating Firebase App");
                app = Firebase.FirebaseApp.Create();
            }

            _auth = FirebaseAuth.DefaultInstance;
            isInitialized = true;
            Debug.Log("[AuthRepository] Firebase Authentication initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Firebase initialization failed: {e.Message}");
            throw;
        }
    }

    // -------------------------------------------------------
    // IAuthRepository
    // -------------------------------------------------------

    public bool IsUserLoggedIn()
    {
        var user = _auth?.CurrentUser;
        return user != null && !user.IsAnonymous;
    }

    public async Task<UserData> SignInWithEmailAsync(string email, string password)
    {
        if (!isInitialized) throw new Exception("Firebase não inicializado");
        if (_auth == null) throw new Exception("FirebaseAuth não inicializado");
        if (_firestore == null) throw new Exception("FirestoreRepository não injetado");

        try
        {
            var result = await _auth.SignInWithEmailAndPasswordAsync(email, password);
            if (result?.User == null)
                throw new Exception("Login falhou: resultado ou usuário nulo");

            string uid = result.User.UserId;
            UserData userData = await _firestore.GetUserData(uid);

            if (userData == null)
                throw new Exception("Dados do usuário não encontrados");

            UserDataStore.CurrentUserData = userData;
            return userData;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Exception during login: {e.Message}");
            throw;
        }
    }

    public async Task<UserData> RegisterUserAsync(string name, string nickName, string email, string password)
    {
        if (_firestore == null) throw new Exception("FirestoreRepository não injetado");

        try
        {
            var result = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

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

            // Usa o IFirestoreRepository em vez de acessar o Firestore diretamente
            await _firestore.CreateUserDocument(user);

            UserDataStore.CurrentUserData = user;
            return user;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Registration failed: {e.Message}");
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            if (!isInitialized) throw new Exception("Firebase não inicializado");
            _auth.SignOut();
            UserDataStore.CurrentUserData = null;
            await Task.CompletedTask;
            Debug.Log("[AuthRepository] Usuário deslogado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao fazer logout: {e.Message}");
            throw;
        }
    }

    public async Task ReloadCurrentUserAsync()
    {
        var user = _auth?.CurrentUser;
        if (user == null) throw new Exception("Nenhum usuário logado");
        await user.ReloadAsync();
    }

    public async Task CheckAuthenticationStatus()
    {
        try
        {
            if (!isInitialized) throw new Exception("Firebase não inicializado");

            var user = _auth.CurrentUser;
            if (user == null) throw new Exception("Usuário não está autenticado");

            await user.ReloadAsync();
            string token = await user.TokenAsync(true);

            if (string.IsNullOrEmpty(token))
                throw new ReauthenticationRequiredException("Token inválido");

            Debug.Log("[AuthRepository] Token atualizado com sucesso");
        }
        catch (Firebase.FirebaseException e) when (e.Message.Contains("recent authentication"))
        {
            Debug.LogError("[AuthRepository] É necessário reautenticar");
            throw new ReauthenticationRequiredException("É necessário reautenticar para prosseguir");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao verificar autenticação: {e.Message}");
            throw;
        }
    }

    public async Task DeleteUser(string userId)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firebase não inicializado");

            var user = _auth.CurrentUser;
            if (user == null) throw new Exception("Usuário não está autenticado");

            await user.ReloadAsync();
            string token = await user.TokenAsync(true);

            if (string.IsNullOrEmpty(token))
                throw new ReauthenticationRequiredException("Token inválido");

            Debug.Log("[AuthRepository] Token atualizado, tentando deletar usuário...");
            await user.DeleteAsync();
            Debug.Log("[AuthRepository] Usuário deletado com sucesso do Authentication");
        }
        catch (Firebase.FirebaseException e) when (e.Message.Contains("requires recent authentication"))
        {
            Debug.LogError("[AuthRepository] É necessário reautenticar para deletar o usuário");
            throw new ReauthenticationRequiredException("É necessário reautenticar para deletar a conta");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao deletar usuário: {e.Message}");
            throw;
        }
    }

    public async Task ReauthenticateUser(string email, string password)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firebase não inicializado");
            if (_auth == null) throw new Exception("FirebaseAuth não inicializado");

            var user = _auth.CurrentUser;
            if (user == null) throw new Exception("Usuário não está autenticado");

            Credential credential = EmailAuthProvider.GetCredential(email, password);
            await user.ReauthenticateAsync(credential);
            Debug.Log("[AuthRepository] Usuário reautenticado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro na reautenticação: {e.Message}");
            throw;
        }
    }
}  