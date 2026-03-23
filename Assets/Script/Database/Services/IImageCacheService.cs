using UnityEngine;

/// <summary>
/// Contrato para cache local de imagens de perfil.
/// </summary>
public interface IImageCacheService
{
    bool IsInitialized { get; }

    /// <summary>
    /// Retorna o caminho local da imagem cacheada, ou null se não houver cache válido.
    /// </summary>
    string GetCachedImagePath(string imageUrl);

    /// <summary>
    /// Salva uma textura no cache associada à URL da imagem.
    /// </summary>
    void SaveImageToCache(string imageUrl, Texture2D texture);

    /// <summary>
    /// Carrega uma textura a partir do caminho local no disco.
    /// </summary>
    Texture2D LoadImageFromCache(string localPath);

    void ClearAllCache();

    long GetTotalCacheSize();

    int GetCachedImagesCount();
}