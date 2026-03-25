using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageUploadService : MonoBehaviour, IImageUploadService
{
    public bool IsUploading { get; private set; } = false;

    private IStorageRepository _storage;

    public void InjectDependencies(IStorageRepository storage)
    {
        _storage = storage;
    }

    public async Task<string> UploadAsync(ImageUploadConfig config)
    {
        if (IsUploading)
        {
            Debug.LogWarning("[ImageUploadService] Upload já em andamento.");
            return null;
        }

        IsUploading = true;

        try
        {
            config.OnProgress?.Invoke("Validando imagem...");
            byte[] imageBytes = ReadAndValidate(config);

            if (!string.IsNullOrEmpty(config.OldImageUrl))
            {
                config.OnProgress?.Invoke("Removendo imagem anterior...");
                await DeleteOldImage(config.OldImageUrl);
            }

            config.OnProgress?.Invoke("Enviando imagem...");
            string fileName = BuildFileName(config);
            string imageUrl = await _storage.UploadImageAsync(fileName, imageBytes);

            Debug.Log($"[ImageUploadService] ✅ Upload concluído: {imageUrl}");
            config.OnCompleted?.Invoke(imageUrl);
            return imageUrl;
        }
        catch (ImageTooLargeException)
        {
            string msg = $"Imagem muito grande. Máximo: {config.MaxSizeBytes / (1024 * 1024)}MB.";
            Fail(config, msg);
            throw;
        }
        catch (Exception ex)
        {
            Fail(config, $"Falha no upload: {ex.Message}");
            throw;
        }
        finally
        {
            IsUploading = false;
        }
    }

    private byte[] ReadAndValidate(ImageUploadConfig config)
    {
        var fileInfo = new FileInfo(config.ImagePath);
        if (fileInfo.Length > config.MaxSizeBytes)
            throw new ImageTooLargeException(
                $"Tamanho: {fileInfo.Length / (1024 * 1024)}MB"
            );
        return File.ReadAllBytes(config.ImagePath);
    }

    private async Task DeleteOldImage(string oldImageUrl)
    {
        try
        {
            await _storage.DeleteProfileImageAsync(oldImageUrl);
            Debug.Log("[ImageUploadService] Imagem antiga deletada.");
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[ImageUploadService] Não foi possível deletar imagem antiga: {ex.Message}");
        }
    }

    private string BuildFileName(ImageUploadConfig config) =>
        $"{config.DestinationFolder}/{config.FileNamePrefix}_{DateTime.UtcNow.Ticks}.jpg";

    private void Fail(ImageUploadConfig config, string message)
    {
        Debug.LogError($"[ImageUploadService] {message}");
        config.OnFailed?.Invoke(message);
    }
}
