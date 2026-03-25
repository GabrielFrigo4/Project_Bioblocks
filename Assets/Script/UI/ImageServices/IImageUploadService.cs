using System.Threading.Tasks;

public interface IImageUploadService
{
    Task<string> UploadAsync(ImageUploadConfig config);
    bool IsUploading { get; }
}

