using System.Threading.Tasks;

/// <summary>
/// Implementação fake do IStorageRepository para testes.
/// Não faz nenhuma chamada real ao Firebase Storage.
///
/// Como usar:
///   var fakeStorage = new FakeStorageRepository();
///   AppContext.OverrideForTests(storage: fakeStorage);
///
///   // Verificar se upload foi chamado:
///   Assert.AreEqual(1, fakeStorage.UploadCallCount);
///   Assert.AreEqual("avatar.png", fakeStorage.LastUploadedFileName);
/// </summary>
public class FakeStorageRepository : IStorageRepository
{
    // Contadores para verificar chamadas em testes
    public int UploadCallCount { get; private set; }
    public int DeleteCallCount { get; private set; }
    public string LastUploadedFileName { get; private set; }
    public string LastDeletedUrl { get; private set; }

    // URL retornada pelo fake — configure antes do teste se precisar
    public string FakeDownloadUrl { get; set; } = "https://fake-storage.com/profile_images/test-user/avatar.png";

    // Simula falha no upload — configure para testar tratamento de erro
    public bool ShouldThrowOnUpload { get; set; } = false;
    public bool ShouldThrowOnDelete { get; set; } = false;

    // -------------------------------------------------------
    // IStorageRepository
    // -------------------------------------------------------

    public bool IsInitialized => true;

    public void Initialize() { /* Nada a fazer no fake */ }

    public Task<string> UploadImageAsync(string fileName, byte[] imageBytes)
    {
        if (ShouldThrowOnUpload)
            throw new System.Exception("[FakeStorageRepository] Upload simulado com falha");

        UploadCallCount++;
        LastUploadedFileName = fileName;
        return Task.FromResult(FakeDownloadUrl);
    }

    public Task DeleteProfileImageAsync(string imageUrl)
    {
        if (ShouldThrowOnDelete)
            throw new System.Exception("[FakeStorageRepository] Delete simulado com falha");

        DeleteCallCount++;
        LastDeletedUrl = imageUrl;
        return Task.CompletedTask;
    }
}