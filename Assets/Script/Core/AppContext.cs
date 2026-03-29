using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using System;

/// <summary>
/// AppContext é o ponto central de composição do app.
///
/// RESPONSABILIDADES:
///   - Inicializar todos os serviços Firebase na ordem correta
///   - Expor os serviços como interfaces (IFirestoreRepository, etc.)
///   - Sobreviver entre cenas via DontDestroyOnLoad
///
/// SETUP NO UNITY:
///   1. Crie um GameObject vazio chamado "App" na InitializationScene
///   2. Adicione este script ao GameObject "App"
///   3. Adicione também FirestoreRepository, AuthenticationRepository
///      e StorageRepository como componentes do mesmo GameObject "App"
///   4. O AppContext encontra todos eles via GetComponent no Awake
///
/// USO NAS CLASSES CONSUMIDORAS:
///   private IFirestoreRepository _firestore;
///
///   private void Start()
///   {
///       _firestore = AppContext.Firestore;
///   }
///
/// USO EM TESTES:
///   AppContext.OverrideForTests(
///       firestore: new FakeFirestoreRepository(),
///       auth:      new FakeAuthRepository()
///   );
/// </summary>
public class AppContext : MonoBehaviour
{
    // -------------------------------------------------------
    // Singleton do AppContext em si
    // (este é o ÚNICO singleton que permanece — ele é o
    //  container, não um serviço de negócio)
    // -------------------------------------------------------
    private static AppContext _instance;
    public static event Action OnReady;

    // -------------------------------------------------------
    // Serviços expostos como interfaces
    // -------------------------------------------------------
    public static IFirestoreRepository Firestore { get; private set; }
    public static IAuthRepository      Auth      { get; private set; }
    public static IStorageRepository   Storage     { get; private set; }
    public static IStatisticsProvider  Statistics  { get; private set; }
    public static INavigationService   Navigation  { get; private set; }
    public static ISceneDataService    SceneData      { get; private set; }
    public static IDatabaseManager     LocalDatabase  { get; private set; }
    public static IImageCacheService   ImageCache     { get; private set; }
    public static IImageUploadService  ImageUpload { get; private set; }
    public static IAnsweredQuestionsManager AnsweredQuestions { get; private set; }

    // -------------------------------------------------------
    // Flag de prontidão — consulte antes de usar os serviços
    // -------------------------------------------------------
    public static bool IsReady { get; private set; }

    // -------------------------------------------------------
    // Awake: monta tudo a partir dos componentes no mesmo GameObject
    // -------------------------------------------------------
    private async void Awake()
    {
        Debug.Log("[AppContext] Awake() chamado");
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        await InitializeServices();
    }

    private async Task InitializeServices()
    {
        IsReady = false;

        try
        {
            // 1. Verifica e corrige dependências do Firebase
            Debug.Log("[AppContext] Verificando dependências do Firebase...");
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
            {
                throw new System.Exception($"[AppContext] Firebase dependencies unavailable: {dependencyStatus}");
            }
            Debug.Log("[AppContext] Firebase disponível.");

            // 2. Busca os componentes no mesmo GameObject — sem arrastar no Inspector
            var authRepo      = GetComponent<AuthenticationRepository>();
            var firestoreRepo = GetComponent<FirestoreRepository>();
            var storageRepo   = GetComponent<StorageRepository>();
            var statsManager    = GetComponent<DatabaseStatisticsManager>();
            var navigationMgr   = GetComponent<NavigationManager>();
            var sceneDataMgr    = GetComponent<SceneDataManager>();
            var databaseMgr     = GetComponent<DatabaseManager>();
            var imageCacheSvc   = GetComponent<ImageCacheService>();
            var imageUploadSvc = GetComponent<ImageUploadService>();
            var answeredQuestionsMgr = GetComponent<AnsweredQuestionsManager>();

            if (authRepo == null)
                throw new System.Exception("[AppContext] AuthenticationRepository não encontrado no GameObject. Adicione o componente.");
            if (firestoreRepo == null)
                throw new System.Exception("[AppContext] FirestoreRepository não encontrado no GameObject. Adicione o componente.");
            if (databaseMgr == null)
                throw new System.Exception("[AppContext] DatabaseManager não encontrado no GameObject. Adicione o componente.");
            if (imageCacheSvc == null)
                throw new System.Exception("[AppContext] ImageCacheService não encontrado no GameObject. Adicione o componente.");
            if (navigationMgr == null)
                throw new System.Exception("[AppContext] NavigationManager não encontrado no GameObject. Adicione o componente.");
            if (sceneDataMgr == null)
                throw new System.Exception("[AppContext] SceneDataManager não encontrado no GameObject. Adicione o componente.");
            if (statsManager == null)
                throw new System.Exception("[AppContext] DatabaseStatisticsManager não encontrado no GameObject. Adicione o componente.");
            if (storageRepo == null)
                throw new System.Exception("[AppContext] StorageRepository não encontrado no GameObject. Adicione o componente.");
            if (imageUploadSvc == null)
                throw new Exception("[AppContext] ImageUploadService não encontrado.");
            if (answeredQuestionsMgr == null)
                throw new Exception("[AppContext] AnsweredQuestionsManager não encontrado.");

            // 3. Inicializa na ordem correta
            await authRepo.InitializeAsync();
            firestoreRepo.Initialize();
            storageRepo.Initialize();

            // 4. Injeta dependências cruzadas
            //    Auth precisa do Firestore para buscar UserData após login
            authRepo.InjectDependencies(firestoreRepo);

            //    Storage precisa do Auth para obter o userId sem chamar FirebaseAuth diretamente
            storageRepo.InjectDependencies(authRepo);
            imageUploadSvc.InjectDependencies(storageRepo);

            // 5. Injeta DatabaseManager no ImageCacheService
            imageCacheSvc.InjectDependencies(databaseMgr);

            // 6. Injeta SceneDataService no NavigationManager
            navigationMgr.InjectDependencies(sceneDataMgr);

            // 7. Inicializa estatísticas dos bancos de questões
            await statsManager.Initialize();

            // 8. Expõe como interfaces — a partir daqui ninguém conhece a classe concreta
            Auth      = authRepo;
            Firestore = firestoreRepo;
            Storage     = storageRepo;
            Statistics  = statsManager;
            Navigation     = navigationMgr;
            LocalDatabase  = databaseMgr;
            ImageCache     = imageCacheSvc;
            SceneData   = sceneDataMgr;
            ImageUpload = imageUploadSvc;
            AnsweredQuestions = answeredQuestionsMgr;

            IsReady = true;
            OnReady?.Invoke();
            Debug.Log("[AppContext] Todos os serviços prontos.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[AppContext] Falha na inicialização: {e.Message}\n{e.StackTrace}");
            if (e.InnerException != null)
                Debug.LogError($"[AppContext] InnerException: {e.InnerException.Message}\n{e.InnerException.StackTrace}");
            IsReady = false;
            throw;
        }
    }

    // -------------------------------------------------------
    // Suporte a testes: substitui as implementações reais por fakes
    // Chame isso no [SetUp] dos seus testes antes de instanciar
    // qualquer Manager que dependa do AppContext.
    //
    // Exemplo:
    //   AppContext.OverrideForTests(
    //       firestore: new FakeFirestoreRepository(),
    //       auth:      new FakeAuthRepository()
    //   );
    // -------------------------------------------------------
    public static void OverrideForTests(
        IFirestoreRepository firestore = null,
        IAuthRepository      auth      = null,
        IStorageRepository   storage    = null,
        IStatisticsProvider  statistics  = null,
        INavigationService   navigation  = null,
        ISceneDataService    sceneData      = null,
        IDatabaseManager     localDatabase  = null,
        IImageCacheService   imageCache     = null)
    {
        if (firestore != null) Firestore = firestore;
        if (auth      != null) Auth      = auth;
        if (storage    != null) Storage    = storage;
        if (statistics != null) Statistics = statistics;
        if (navigation != null) Navigation = navigation;
        if (sceneData     != null) SceneData     = sceneData;
        if (localDatabase != null) LocalDatabase = localDatabase;
        if (imageCache    != null) ImageCache    = imageCache;
        IsReady = true;
    }
}