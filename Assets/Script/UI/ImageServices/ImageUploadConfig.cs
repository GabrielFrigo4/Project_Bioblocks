using System;

public class ImageUploadConfig
{
    public string ImagePath          { get; set; }  // caminho local da imagem
    public string DestinationFolder  { get; set; }  // ex: "profile_images", "post_images"
    public string FileNamePrefix     { get; set; }  // ex: userId, postId
    public int    MaxSizeBytes       { get; set; } = 1024 * 1024; // 1MB default
    public string OldImageUrl        { get; set; }  // URL antiga para deletar (opcional)
    public Action<string> OnProgress { get; set; }  // mensagem de progresso (opcional)
    public Action<string> OnCompleted{ get; set; }  // URL final
    public Action<string> OnFailed   { get; set; }  // mensagem de erro
}