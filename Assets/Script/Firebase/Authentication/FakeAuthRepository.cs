using System.Threading.Tasks;

/// <summary>
/// Implementação fake do IAuthRepository para testes.
/// Não faz nenhuma chamada real ao Firebase Authentication.
///
/// Como usar:
///   var fakeAuth = new FakeAuthRepository();
///   fakeAuth.SetLoggedInUser("user-123");
///   AppContext.OverrideForTests(auth: fakeAuth);
/// </summary>
public class FakeAuthRepository : IAuthRepository
{
    private string _currentUserId;
    private bool _isLoggedIn;

    // Contadores para verificar chamadas em testes
    public int LogoutCallCount { get; private set; }
    public int ReloadCallCount { get; private set; }
    public string LastSignInEmail { get; private set; }

    // -------------------------------------------------------
    // Configuração do fake
    // -------------------------------------------------------

    public void SetLoggedInUser(string userId)
    {
        _currentUserId = userId;
        _isLoggedIn = true;
    }

    public void SetLoggedOut()
    {
        _currentUserId = null;
        _isLoggedIn = false;
    }

    // -------------------------------------------------------
    // IAuthRepository
    // -------------------------------------------------------

    public bool IsInitialized => true;

    public string CurrentUserId => _currentUserId;

    public bool IsUserLoggedIn() => _isLoggedIn;

    public Task InitializeAsync() => Task.CompletedTask;

    public Task<UserData> SignInWithEmailAsync(string email, string password)
    {
        LastSignInEmail = email;
        _isLoggedIn = true;

        var fakeUser = new UserData
        {
            UserId = _currentUserId ?? "fake-user-id",
            Email = email,
            NickName = "FakeUser",
            Name = "Fake User"
        };

        return Task.FromResult(fakeUser);
    }

    public Task<UserData> RegisterUserAsync(string name, string nickName, string email, string password)
    {
        _currentUserId = "new-fake-user-id";
        _isLoggedIn = true;

        var fakeUser = new UserData
        {
            UserId = _currentUserId,
            Email = email,
            NickName = nickName,
            Name = name
        };

        return Task.FromResult(fakeUser);
    }

    public Task LogoutAsync()
    {
        LogoutCallCount++;
        _currentUserId = null;
        _isLoggedIn = false;
        return Task.CompletedTask;
    }

    public Task ReloadCurrentUserAsync()
    {
        ReloadCallCount++;
        return Task.CompletedTask;
    }

    public Task CheckAuthenticationStatus() => Task.CompletedTask;

    public Task DeleteUser(string userId)
    {
        _currentUserId = null;
        _isLoggedIn = false;
        return Task.CompletedTask;
    }

    public Task ReauthenticateUser(string email, string password) => Task.CompletedTask;
}