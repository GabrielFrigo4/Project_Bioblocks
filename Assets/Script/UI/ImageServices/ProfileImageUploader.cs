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
    private IFirestoreRepository _firestore;
    private IImageUploadService _imageUpload;

    private void Awake()
    {
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
        _firestore = AppContext.Firestore;
        _imageUpload = AppContext.ImageUpload;
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
        NativeGallery.RequestPermissionAsync((permission) =>
        {
            Debug.Log($"[ProfileImageUploader] Permissão: {permission}");

            if (permission == NativeGallery.Permission.Granted)
            {
               OpenGallery();
            }
            else if (permission == NativeGallery.Permission.ShouldAsk)
            {
                OpenGallery();
            }
            else
            {
                ShowAlert("Permissão negada.\nAcesse as configurações do dispositivo para liberar o acesso à galeria.");
                NativeGallery.OpenSettings();
            }
        },
        NativeGallery.PermissionType.Read,
        NativeGallery.MediaType.Image);
    }

    private void OpenGallery()
    {
        Debug.Log("[ProfileImageUploader] Abrindo galeria...");

        NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log($"[ProfileImageUploader] Callback da galeria. Path: {path ?? "NULL"}");

            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ProfileImageUploader] Nenhuma imagem selecionada");
                isProcessing = false;
                return;
            }

            ProcessSelectedImage(path);
        },
        "Selecione uma imagem",
        "image/*");
    }

    private async void ProcessSelectedImage(string imagePath)
{
        isProcessing = true;
        if (uploadButton != null) uploadButton.interactable = false;

        try
        {
            byte[] previewBytes = File.ReadAllBytes(imagePath);
            Texture2D preview = new Texture2D(2, 2);
            preview.LoadImage(previewBytes);
            imageLoader.SetTexture(preview);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[ProfileImageUploader] Erro ao gerar preview: {e.Message}");
        }

        var config = new ImageUploadConfig
        {
            ImagePath        = imagePath,
            DestinationFolder= "profile_images",
            FileNamePrefix   = currentUserData.UserId,
            MaxSizeBytes     = maxImageSizeBytes,
            OldImageUrl      = currentUserData.ProfileImageUrl,
            OnProgress  = msg  => Debug.Log($"[ProfileImageUploader] {msg}"),
            OnCompleted = async url =>
            {
                await _firestore.UpdateUserProfileImageUrl(currentUserData.UserId, url);
                currentUserData.ProfileImageUrl = url;
                UserDataStore.CurrentUserData   = currentUserData;
                UserAvatarSyncHelper.NotifyAvatarChanged(url);
            },
            OnFailed = error => ShowAlert(error)
        };

        try
        {
            await _imageUpload.UploadAsync(config);
        }
        catch
        {
            // Erro já tratado pelo OnFailed do config
        }

        FinishProcessing();
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