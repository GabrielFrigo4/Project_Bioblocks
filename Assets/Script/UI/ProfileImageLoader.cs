using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ProfileImageLoader : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RawImage imageContent;
    [SerializeField] private Sprite standardProfileImage;

    [Header("Mask Configuration")]
    [SerializeField] private bool autoConfigureMask = true;
    [SerializeField] private int maskResolution = 256;

    private bool isInitialized = false;
    private string pendingImageUrl = null;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (isInitialized) return;

        if (imageContent == null)
        {
            imageContent = GetComponentInChildren<RawImage>(true);
            
            if (imageContent == null)
            {
                Debug.LogError($"[ProfileImageLoader] ImageContent não encontrado em '{gameObject.name}'. Certifique-se de atribuir o RawImage no Inspector.");
                isInitialized = true;
                return;
            }
        }

        if (autoConfigureMask)
        {
            ConfigureImageMask();
        }

        isInitialized = true;
    }

    public void SetImageContent(RawImage rawImage)
    {
        imageContent = rawImage;

        if (autoConfigureMask)
        {
            ConfigureImageMask();
        }

        isInitialized = true;
    }

    private void ConfigureImageMask()
    {
        if (imageContent == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] ImageContent é null, não é possível configurar máscara em '{gameObject.name}'");
            return;
        }

        var maskObject = imageContent.transform.parent?.gameObject;
        if (maskObject == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] MaskObject (pai do RawImage) não encontrado em '{gameObject.name}'");
            return;
        }

        var mask = maskObject.GetComponent<Mask>();
        if (mask == null)
        {
            mask = maskObject.AddComponent<Mask>();
        }
        mask.showMaskGraphic = true;

        var maskImage = maskObject.GetComponent<Image>();
        if (maskImage == null)
        {
            maskImage = maskObject.AddComponent<Image>();
        }

        maskImage.sprite = CreateCircleSprite(maskResolution);
        maskImage.type = Image.Type.Simple;
        maskImage.color = Color.white;

        var imageRect = imageContent.GetComponent<RectTransform>();
        if (imageRect != null)
        {
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.sizeDelta = Vector2.zero;
            imageRect.anchoredPosition = Vector2.zero;
        }
    }

    private Sprite CreateCircleSprite(int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        float radius = resolution / 2f;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                Color color = distance < radius ? Color.white : Color.clear;
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return Sprite.Create(
            texture,
            new Rect(0, 0, resolution, resolution),
            Vector2.one * 0.5f,
            100f,
            0,
            SpriteMeshType.Tight
        );
    }

    public void LoadProfileImage(string imageUrl)
    {
        if (!isInitialized)
        {
            Initialize();
        }

        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em '{gameObject.name}'");
            return;
        }

        if (!gameObject.activeInHierarchy)
        {
            pendingImageUrl = imageUrl;
            return;
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            LoadStandardProfileImage();
            return;
        }

        StartCoroutine(LoadImageFromUrl(imageUrl));
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(pendingImageUrl))
        {
            string urlToLoad = pendingImageUrl;
            pendingImageUrl = null;
            LoadProfileImage(urlToLoad);
        }
    }

    public void LoadFromCurrentUser()
    {
        UserData currentUserData = UserDataStore.CurrentUserData;
        if (currentUserData != null)
        {
            LoadProfileImage(currentUserData.ProfileImageUrl);
        }
        else
        {
            LoadStandardProfileImage();
        }
    }

    private IEnumerator LoadImageFromUrl(string url)
    {
        string cachedPath = ImageCacheService.Instance.GetCachedImagePath(url);
        
        if (!string.IsNullOrEmpty(cachedPath))
        {
            Texture2D cachedTexture = ImageCacheService.Instance.LoadImageFromCache(cachedPath);
            
            if (cachedTexture != null)
            {
                SetTexture(cachedTexture);
                yield break;
            }
        }

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                SetTexture(texture);
                ImageCacheService.Instance.SaveImageToCache(url, texture);
            }
            else
            {
                Debug.LogWarning($"[ProfileImageLoader] Erro ao carregar imagem: {www.error}. Usando imagem padrão.");
                LoadStandardProfileImage();
            }
        }
    }

    public void LoadStandardProfileImage()
    {
        if (!isInitialized)
        {
            Initialize();
        }

        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em '{gameObject.name}'");
            return;
        }

        if (standardProfileImage == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] StandardProfileImage não atribuída em '{gameObject.name}'. Criando placeholder...");
            CreateAndSetPlaceholderTexture();
            return;
        }

        if (standardProfileImage.texture == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] StandardProfileImage não tem textura em '{gameObject.name}'");
            CreateAndSetPlaceholderTexture();
            return;
        }

        imageContent.texture = standardProfileImage.texture;
        imageContent.color = Color.white;
    }

    private void CreateAndSetPlaceholderTexture()
    {
        Texture2D texture = new Texture2D(128, 128);
        Color[] colors = new Color[128 * 128];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.gray;
        }
        texture.SetPixels(colors);
        texture.Apply();
        SetTexture(texture);
    }

    public void SetTexture(Texture2D texture)
    {
        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em '{gameObject.name}'");
            return;
        }

        if (imageContent.texture != null &&
            imageContent.texture != standardProfileImage?.texture &&
            imageContent.texture != texture)
        {
            Destroy(imageContent.texture);
        }

        imageContent.texture = texture;
        imageContent.color = Color.white;
        AdjustImageAspectRatio(texture);
    }

    private void AdjustImageAspectRatio(Texture2D texture)
    {
        if (texture == null || imageContent == null) return;

        float textureAspect = (float)texture.width / texture.height;
        
        if (Mathf.Approximately(textureAspect, 1f))
        {
            imageContent.uvRect = new Rect(0, 0, 1, 1);
            return;
        }

        Rect uvRect;
        
        if (textureAspect > 1f) 
        {
            float scale = 1f / textureAspect;
            float offset = (1f - scale) / 2f;
            uvRect = new Rect(offset, 0, scale, 1);
        }
        else 
        {
            float scale = textureAspect;
            float offset = (1f - scale) / 2f;
            uvRect = new Rect(0, offset, 1, scale);
        }
        
        imageContent.uvRect = uvRect;
    }

    private void OnDestroy()
    {
        if (imageContent != null &&
            imageContent.texture != null &&
            imageContent.texture != standardProfileImage?.texture)
        {
            Destroy(imageContent.texture);
        }
    }

    public RawImage ImageContent => imageContent;
    public Sprite StandardProfileImage
    {
        get => standardProfileImage;
        set => standardProfileImage = value;
    }
    public bool IsInitialized => isInitialized;
    public string PendingImageUrl => pendingImageUrl;
}