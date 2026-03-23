using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase;

/// <summary>
/// Implementação real do IStorageRepository usando Firebase Storage.
/// </summary>
public class StorageRepository : MonoBehaviour, IStorageRepository
{
    private IAuthRepository _auth;
    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    // -------------------------------------------------------
    // Injeção de dependência
    // -------------------------------------------------------

    public void InjectDependencies(IAuthRepository auth)
    {
        _auth = auth;
    }

    // -------------------------------------------------------
    // IStorageRepository
    // -------------------------------------------------------

    public void Initialize()
    {
        if (isInitialized) return;

        try
        {
            // Valida que o Firebase Storage está acessível
            var _ = FirebaseStorage.DefaultInstance;
            isInitialized = true;
            Debug.Log("[StorageRepository] Storage initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"[StorageRepository] Storage initialization failed: {e.Message}");
            throw;
        }
    }

    public async Task<string> UploadImageAsync(string fileName, byte[] imageBytes)
    {
        if (_auth == null) throw new Exception("[StorageRepository] IAuthRepository não injetado");
        if (!_auth.IsUserLoggedIn()) throw new Exception("[StorageRepository] Nenhum usuário logado");

        try
        {
            string userId = _auth.CurrentUserId;
            string safePath = $"profile_images/{userId}/{Path.GetFileName(fileName)}";

            var storage = FirebaseStorage.DefaultInstance;
            var imageRef = storage.RootReference.Child(safePath);

            var metadata = new MetadataChange { ContentType = "image/png" };

            await imageRef.PutBytesAsync(imageBytes, metadata);

            Uri downloadUri = await imageRef.GetDownloadUrlAsync();
            return downloadUri.ToString();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[StorageRepository] Erro no upload da imagem: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteProfileImageAsync(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;
        if (_auth == null) throw new Exception("[StorageRepository] IAuthRepository não injetado");
        if (!_auth.IsUserLoggedIn()) throw new Exception("[StorageRepository] Nenhum usuário logado");

        try
        {
            string userId = _auth.CurrentUserId;
            string fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
            string storagePath = $"profile_images/{userId}/{fileName}";

            Debug.Log($"[StorageRepository] Tentando deletar imagem: {storagePath}");

            var imageRef = FirebaseStorage.DefaultInstance.GetReference(storagePath);
            await imageRef.DeleteAsync();

            Debug.Log($"[StorageRepository] Imagem deletada com sucesso: {storagePath}");
        }
        catch (FirebaseException e)
        {
            Debug.LogError($"[StorageRepository] Erro Firebase ao deletar imagem: {e.Message}. Código: {e.ErrorCode}");
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[StorageRepository] Erro ao deletar imagem: {e.Message}");
            throw;
        }
    }
}