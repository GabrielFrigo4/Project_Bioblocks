using System.Threading.Tasks;

/// <summary>
/// Contrato para operações de armazenamento de arquivos.
/// Não expõe nenhum tipo do Firebase SDK.
/// </summary>
public interface IStorageRepository
{
    bool IsInitialized { get; }

    void Initialize();

    /// <summary>
    /// Faz upload de uma imagem e retorna a URL de download.
    /// </summary>
    Task<string> UploadImageAsync(string fileName, byte[] imageBytes);

    /// <summary>
    /// Deleta a imagem de perfil a partir da URL de download.
    /// </summary>
    Task DeleteProfileImageAsync(string imageUrl);
}