using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCacheServiceStub : IImageCacheService
{
    public bool IsInitialized => true;

    public string GetCachedImagePath(string imageUrl) => null;

    public void SaveImageToCache(string imageUrl, Texture2D texture) { }

    public Texture2D LoadImageFromCache(string localPath) => null;

    public void ClearAllCache() { }

    public long GetTotalCacheSize() => 0;

    public int GetCachedImagesCount() => 0;
}