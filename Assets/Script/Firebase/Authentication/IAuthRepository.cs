using System.Threading.Tasks;

/// <summary>
/// Contrato para autenticação de usuários.
///
/// Importante: esta interface não expõe nenhum tipo do Firebase SDK
/// (FirebaseAuth, FirebaseUser, etc.). Quem usa a interface trabalha
/// apenas com tipos do domínio do app (string, UserData, Task).
/// </summary>
public interface IAuthRepository
{
    bool IsInitialized { get; }

    string CurrentUserId { get; }

    bool IsUserLoggedIn();

    Task InitializeAsync();

    Task<UserData> SignInWithEmailAsync(string email, string password);

    Task<UserData> RegisterUserAsync(string name, string nickName, string email, string password);

    Task LogoutAsync();

    Task ReloadCurrentUserAsync();

    Task CheckAuthenticationStatus();

    Task DeleteUser(string userId);

    Task ReauthenticateUser(string email, string password);
}
