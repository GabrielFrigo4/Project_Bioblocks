using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;

public class StorageRepository : MonoBehaviour
{
    private static StorageRepository _instance;
    private bool isInitialized;

    public static StorageRepository Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<StorageRepository>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("StorageRepository");
                    _instance = go.AddComponent<StorageRepository>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void Initialize()
    {
        if (isInitialized) return;

        try
        {
            var storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
            isInitialized = true;
            Debug.Log("Storage initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Storage initialization failed: {e.Message}");
            throw;
        }
    }

    public async Task<string> UploadImageAsync(string fileName, byte[] imageBytes)
    {
        try
        {
            var storage = Firebase.Storage.FirebaseStorage.DefaultInstance;

            string userId = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            string safePath = $"profile_images/{userId}/{Path.GetFileName(fileName)}";

            var storageRef = storage.RootReference;
            var imageRef = storageRef.Child(safePath);

            var metadata = new Firebase.Storage.MetadataChange
            {
                ContentType = "image/png"
            };

            await imageRef.PutBytesAsync(imageBytes, metadata);

            Uri downloadUri = await imageRef.GetDownloadUrlAsync();
            return downloadUri.ToString();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro no upload da imagem: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteProfileImageAsync(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return;

        try
        {
            string fileName = System.IO.Path.GetFileName(new Uri(imageUrl).LocalPath);
            string storagePath = $"profile_images/{UserDataStore.CurrentUserData.UserId}/{fileName}";
            Debug.Log($"Image FileName {fileName}");

            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference imageRef = storage.GetReference(storagePath);

            Debug.Log($"Tentando deletar imagem do caminho: {storagePath}");
            await imageRef.DeleteAsync();
            Debug.Log($"Imagem deletada com sucesso: {storagePath}");
        }
        catch (Firebase.FirebaseException e)
        {
            Debug.LogError($"Erro ao deletar imagem do Storage: {e.Message}. Código do erro: {e.ErrorCode}");
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao deletar imagem do Storage: {e.Message}");
            throw;
        }
    }
}
