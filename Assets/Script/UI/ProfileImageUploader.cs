using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;

[RequireComponent(typeof(ProfileImageLoader))]
public class ProfileImageUploader : MonoBehaviour
{
    [Header("Upload Configuration")]
    [SerializeField] private Button uploadButton;
    [SerializeField] private bool enableUploadOnStart = true;

    [Header("Validation")]
    [SerializeField] private int maxImageSizeBytes = 1024 * 1024;

    private ProfileImageLoader imageLoader;
    private UserData currentUserData;
    private bool isProcessing = false;
    private IStorageRepository _storage;
    private IFirestoreRepository _firestore;

    private void Awake()
    {
        _storage  = AppContext.Storage;
        _firestore = AppContext.Firestore;
        imageLoader = GetComponent<ProfileImageLoader>();

        if (imageLoader == null)
        {
            Debug.LogError("[ProfileImageUploader] ProfileImageLoader não encontrado!");
            return;
        }

        imageLoader.Initialize();
    }

    private void Start()
    {
        _storage  = AppContext.Storage;
        _firestore = AppContext.Firestore;
        currentUserData = UserDataStore.CurrentUserData;

        if (enableUploadOnStart)
        {
            EnableUpload(true);
        }

        LoadCurrentProfileImage();
    }

    public void EnableUpload(bool enable)
    {
        if (uploadButton == null)
        {
            Debug.LogWarning("[ProfileImageUploader] Upload button não configurado");
            return;
        }

        if (enable)
        {
            uploadButton.onClick.RemoveAllListeners();
            uploadButton.onClick.AddListener(OnUploadButtonClick);
            uploadButton.interactable = true;
        }
        else
        {
            uploadButton.onClick.RemoveAllListeners();
            uploadButton.interactable = false;
        }
    }

    private void LoadCurrentProfileImage()
    {
        if (currentUserData != null && !string.IsNullOrEmpty(currentUserData.ProfileImageUrl))
        {
            imageLoader.LoadProfileImage(currentUserData.ProfileImageUrl);
        }
        else
        {
            imageLoader.LoadStandardProfileImage();
        }
    }

    private void OnUploadButtonClick()
    {
        if (isProcessing || NativeGallery.IsMediaPickerBusy())
        {
            Debug.Log("[ProfileImageUploader] Upload já em andamento ou galeria ocupada");
            return;
        }

        RequestGalleryPermission();
    }

    private void RequestGalleryPermission()
    {
        bool granted = true;

        try
        {
            granted = NativeGallery.CheckPermission(
                NativeGallery.PermissionType.Read,
                NativeGallery.MediaType.Image
            );
        }
        catch
        {
            granted = true;
        }

        if (!granted)
        {
            Debug.LogWarning("[ProfileImageUploader] Permissão para acessar a galeria negada");

            if (AlertManager.Instance != null)
            {
                AlertManager.Instance.ShowAlert(
                    "Permissão para acessar a galeria negada.\nPor favor, verifique as configurações do seu dispositivo."
                );
            }
            return;
        }

        OpenGallery();
    }

    private void OpenGallery()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ProfileImageUploader] Nenhuma imagem selecionada");
                return;
            }

            StartCoroutine(ProcessSelectedImage(path));
        },
        "Selecione uma imagem",
        "image/*");
    }

    private IEnumerator ProcessSelectedImage(string imagePath)
    {
        isProcessing = true;

        if (uploadButton != null)
        {
            uploadButton.interactable = false;
        }

        FileInfo fileInfo = null;
        try
        {
            fileInfo = new FileInfo(imagePath);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ProfileImageUploader] Erro ao acessar arquivo: {e.Message}");
            ShowAlert($"Erro ao acessar o arquivo.\nDetalhes: {e.Message}");
            FinishProcessing();
            yield break;
        }

        if (fileInfo.Length > maxImageSizeBytes)
        {
            Debug.LogWarning($"[ProfileImageUploader] Imagem muito grande: {fileInfo.Length} bytes (máx: {maxImageSizeBytes})");
            ShowAlert($"A imagem selecionada excede o tamanho máximo permitido ({maxImageSizeBytes / (1024 * 1024)}MB).");
            FinishProcessing();
            yield break;
        }

        byte[] imageBytes = null;
        Texture2D texture = null;

        try
        {
            imageBytes = File.ReadAllBytes(imagePath);
            texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            imageLoader.SetTexture(texture);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ProfileImageUploader] Erro ao carregar imagem: {e.Message}");
            ShowAlert($"Erro ao carregar a imagem.\nDetalhes: {e.Message}");
            FinishProcessing();
            yield break;
        }

        if (!string.IsNullOrEmpty(currentUserData.ProfileImageUrl))
        {
            Debug.Log($"[ProfileImageUploader] ===== PASSO 3: Deletando imagem antiga =====");
            Debug.Log($"[ProfileImageUploader] Tentando deletar imagem antiga: {currentUserData.ProfileImageUrl}");

            bool deleteSuccess = false;
            yield return StartCoroutine(DeleteOldProfileImage(currentUserData.ProfileImageUrl, (result) =>
            {
                deleteSuccess = result;
                Debug.Log($"[ProfileImageUploader] Callback recebido de DeleteOldProfileImage: {result}");
            }));

            Debug.Log($"[ProfileImageUploader] Após coroutine DeleteOldProfileImage. Success={deleteSuccess}");

            if (!deleteSuccess)
            {
                Debug.LogWarning("[ProfileImageUploader] Não foi possível deletar imagem antiga (pode não existir mais). Continuando com upload da nova imagem...");
            }
            else
            {
                Debug.Log("[ProfileImageUploader] Imagem antiga deletada com sucesso");
            }
        }
        else
        {
            Debug.Log("[ProfileImageUploader] ===== PASSO 3: PULADO (primeira foto) =====");
            Debug.Log("[ProfileImageUploader] Primeira foto de perfil do usuário. Não há imagem antiga para deletar.");
        }

        Debug.Log($"[ProfileImageUploader] ===== PASSO 4: Iniciando upload da nova imagem =====");

        string userId = currentUserData.UserId;
        string fileName = $"profile_images/{userId}_{DateTime.UtcNow.Ticks}.jpg";

        Debug.Log($"[ProfileImageUploader] Iniciando upload para: {fileName}");

        bool uploadSuccess = false;
        string imageUrl = null;

        yield return StartCoroutine(UploadNewProfileImage(fileName, imageBytes, (success, url) =>
        {
            uploadSuccess = success;
            imageUrl = url;
        }));

        if (!uploadSuccess)
        {
            Debug.LogError("[ProfileImageUploader] Falha no upload da nova imagem");
            ShowAlert("Falha no upload da imagem.\nPor favor, tente novamente mais tarde.");
            FinishProcessing();
            yield break;
        }

        Debug.Log($"[ProfileImageUploader] Upload bem-sucedido! URL: {imageUrl}");

        Debug.Log($"[ProfileImageUploader] Atualizando URL no Firestore: {imageUrl}");

        bool updateSuccess = false;
        yield return StartCoroutine(UpdateProfileUrl(imageUrl, (success) =>
        {
            updateSuccess = success;
        }));

        if (!updateSuccess)
        {
            Debug.LogError("[ProfileImageUploader] Falha ao atualizar URL do perfil no Firestore");
            ShowAlert("Falha ao atualizar o perfil.\nPor favor, tente novamente mais tarde.");
            FinishProcessing();
            yield break;
        }

        Debug.Log("[ProfileImageUploader] URL atualizada no Firestore com sucesso!");

        UserAvatarSyncHelper.NotifyAvatarChanged(imageUrl);

        Debug.Log("[ProfileImageUploader] ✅ Upload concluído com sucesso!");
        FinishProcessing();
    }

    private IEnumerator DeleteOldProfileImage(string imageUrl, Action<bool> callback)
    {
        Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - INÍCIO");

        Task task = null;
        bool taskStarted = false;

        try
        {
            task = _storage.DeleteProfileImageAsync(imageUrl);
            taskStarted = true;
            Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - Task criada, aguardando...");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[ProfileImageUploader] DeleteOldProfileImage - Erro ao criar task: {ex.Message}");
            callback(false);
            yield break;
        }

        if (taskStarted && task != null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - Task completada. IsFaulted={task.IsFaulted}, IsCompleted={task.IsCompleted}");
        }

        if (task != null && task.IsFaulted)
        {
            string errorMessage = task.Exception?.GetBaseException().Message ?? "";
            if (errorMessage.Contains("404") || errorMessage.Contains("Not Found"))
            {
                Debug.Log("[ProfileImageUploader] Imagem antiga não encontrada no Storage (404). Isso é normal se for a primeira foto ou se já foi deletada.");
            }
            else
            {
                Debug.LogWarning($"[ProfileImageUploader] Erro ao deletar imagem antiga: {errorMessage}");
            }

            Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - Chamando callback(false)");
            callback(false);
        }
        else
        {
            Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - Chamando callback(true)");
            callback(true);
        }

        Debug.Log($"[ProfileImageUploader] DeleteOldProfileImage - FIM");
    }

    private IEnumerator UploadNewProfileImage(string fileName, byte[] imageBytes, Action<bool, string> callback)
    {
        Debug.Log($"[ProfileImageUploader] UploadNewProfileImage - INÍCIO. Tamanho: {imageBytes.Length} bytes, Nome: {fileName}");

        Task<string> task = null;
        bool taskStarted = false;

        try
        {
            task = UploadImageAsync(fileName, imageBytes);
            taskStarted = true;
            Debug.Log($"[ProfileImageUploader] UploadNewProfileImage - Task criada, aguardando...");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[ProfileImageUploader] UploadNewProfileImage - Erro ao criar task: {ex.Message}");
            callback(false, null);
            yield break;
        }

        if (taskStarted && task != null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            Debug.Log($"[ProfileImageUploader] UploadNewProfileImage - Task completada. IsFaulted={task.IsFaulted}");
        }

        if (task != null && task.IsFaulted)
        {
            Debug.LogError($"[ProfileImageUploader] Erro no upload: {task.Exception?.GetBaseException().Message}");
            callback(false, null);
        }
        else if (task != null)
        {
            Debug.Log($"[ProfileImageUploader] Upload concluído. URL recebida: {task.Result}");
            callback(true, task.Result);
        }
        else
        {
            Debug.LogError($"[ProfileImageUploader] Task é null!");
            callback(false, null);
        }

        Debug.Log($"[ProfileImageUploader] UploadNewProfileImage - FIM");
    }

    private IEnumerator UpdateProfileUrl(string imageUrl, Action<bool> callback)
    {
        Debug.Log($"[ProfileImageUploader] UpdateProfileUrl - INÍCIO. URL: {imageUrl}, UserID: {currentUserData.UserId}");

        Task task = null;
        bool taskStarted = false;

        try
        {
            task = UpdateProfileImageUrlAsync(imageUrl);
            taskStarted = true;
            Debug.Log($"[ProfileImageUploader] UpdateProfileUrl - Task criada, aguardando...");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[ProfileImageUploader] UpdateProfileUrl - Erro ao criar task: {ex.Message}");
            callback(false);
            yield break;
        }

        if (taskStarted && task != null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            Debug.Log($"[ProfileImageUploader] UpdateProfileUrl - Task completada. IsFaulted={task.IsFaulted}");
        }

        if (task != null && task.IsFaulted)
        {
            Debug.LogError($"[ProfileImageUploader] Erro ao atualizar URL no Firestore: {task.Exception?.GetBaseException().Message}");
            callback(false);
        }
        else
        {
            Debug.Log("[ProfileImageUploader] URL atualizada no Firestore e UserDataStore!");
            callback(true);
        }

        Debug.Log($"[ProfileImageUploader] UpdateProfileUrl - FIM");
    }

    private async Task<string> UploadImageAsync(string fileName, byte[] imageBytes)
    {
        try
        {
            return await _storage.UploadImageAsync(fileName, imageBytes);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ProfileImageUploader] Erro no upload: {e.Message}");
            throw;
        }
    }

    private async Task UpdateProfileImageUrlAsync(string imageUrl)
    {
        try
        {
            await _firestore.UpdateUserProfileImageUrl(currentUserData.UserId, imageUrl);
            currentUserData.ProfileImageUrl = imageUrl;
            UserDataStore.CurrentUserData = currentUserData;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ProfileImageUploader] Erro ao atualizar URL: {e.Message}");
            throw;
        }
    }

    private void ShowAlert(string message)
    {
        if (AlertManager.Instance != null)
        {
            AlertManager.Instance.ShowAlert(message);
        }
        else
        {
            Debug.LogWarning($"[ProfileImageUploader] AlertManager não disponível. Mensagem: {message}");
        }
    }

    private void FinishProcessing()
    {
        isProcessing = false;

        if (uploadButton != null)
        {
            uploadButton.interactable = true;
        }
    }

    private void OnDestroy()
    {
        if (uploadButton != null)
        {
            uploadButton.onClick.RemoveAllListeners();
        }
    }

    public ProfileImageLoader ImageLoader => imageLoader;
    public bool IsProcessing => isProcessing;
}