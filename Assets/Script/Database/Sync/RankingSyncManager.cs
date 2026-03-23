using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class RankingSyncManager : MonoBehaviour
{                                                                      
    private LocalRankingRepository _localRepo;
    private LocalSyncMetadataRepository _syncMetadataRepo;
    private IRankingRepository _remoteRepo;
    
    private bool _isSyncing = false;
    private bool _isInitialized = false;
    private const string RANKINGS_ENTITY_TYPE = "Rankings";
    private TimeSpan _cacheValidityDuration = TimeSpan.FromMinutes(5);

    public event Action OnSyncStarted;
    public event Action<bool> OnSyncCompleted;

    private IFirestoreRepository _firestore;
    private ConnectivityMonitor _connectivity;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _firestore        = AppContext.Firestore;
        _connectivity     = FindFirstObjectByType<ConnectivityMonitor>();
        _localRepo        = new LocalRankingRepository(AppContext.LocalDatabase);
        _syncMetadataRepo = new LocalSyncMetadataRepository(AppContext.LocalDatabase);
        Initialize();                 
     }

    private bool Initialize()
    {
        if (_isInitialized)
            return true;

        try
        {
            if (BioBlocksSettings.Instance != null && BioBlocksSettings.Instance.IsDebugMode())
            {
                _remoteRepo = new MockRankingRepository();
            }
            else
            {
                if (_firestore == null || !_firestore.IsInitialized)
                {
                    Debug.LogError("[RankingSyncManager] FirestoreRepository not initialized");
                    return false;
                }
                
                _remoteRepo = new RankingRepository();
            }

            _isInitialized = true;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Initialization failed: {e.Message}\n{e.StackTrace}");
            return false;
        }
    }

    public async Task<List<Ranking>> GetRankingsWithCache()
    {
        if (!_isInitialized && !Initialize())
        {
            Debug.LogError("[RankingSyncManager] Not initialized, cannot get rankings");
            return new List<Ranking>();
        }

        try
        {
            var cachedRankings = _localRepo.GetAllRankings();
            
            if (cachedRankings != null && cachedRankings.Count > 0)
            {
                TrySyncInBackgroundSafe();
                return cachedRankings.Select(e => RankingDTO.ToRanking(e)).ToList();
            }
            else
            {
                bool success = await SyncRankings();
                
                if (success)
                {
                    cachedRankings = _localRepo.GetAllRankings();
                    return cachedRankings.Select(e => RankingDTO.ToRanking(e)).ToList();
                }
                else
                {
                    return await FallbackDirectFetch();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] GetRankingsWithCache failed: {e.Message}\n{e.StackTrace}");
            return new List<Ranking>();
        }
    }

    private async void TrySyncInBackgroundSafe()
    {
        try
        {
            await TrySyncInBackground();
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Background sync failed: {e.Message}");
        }
    }

    private async Task TrySyncInBackground()
    {
        if (_connectivity == null || !_connectivity.IsOnline)
        {
            return;
        }

        if (_syncMetadataRepo.ShouldSync(RANKINGS_ENTITY_TYPE, _cacheValidityDuration))
        {
            await SyncRankings();
        }
    }

    private async Task<List<Ranking>> FallbackDirectFetch()
    {
        try
        {
            var firebaseRankings = await _remoteRepo.GetRankingsAsync();
            
            if (firebaseRankings != null && firebaseRankings.Count > 0)
            {
                return firebaseRankings;
            }
            else
            {
                Debug.LogWarning("[RankingSyncManager] Direct fetch returned no data");
                return new List<Ranking>();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Direct fetch failed: {e.Message}");
            return new List<Ranking>();
        }
    }

    public async Task<bool> SyncRankings()
    {
        if (!_isInitialized && !Initialize())
        {
            Debug.LogError("[RankingSyncManager] Not initialized, cannot sync");
            return false;
        }

        if (_isSyncing)
        {
            return false;
        }

        if (_connectivity == null || !_connectivity.IsOnline)
        {
            Debug.LogWarning("[RankingSyncManager] Cannot sync - device is offline");
            return false;
        }

        _isSyncing = true;
        OnSyncStarted?.Invoke();

        try
        {
            var remoteRankings = await _remoteRepo.GetRankingsAsync();
            
            if (remoteRankings == null || remoteRankings.Count == 0)
            {
                Debug.LogWarning("[RankingSyncManager] No rankings received from remote");
                _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, false, "No data received");
                OnSyncCompleted?.Invoke(false);
                return false;
            }

            var currentUser = await _remoteRepo.GetCurrentUserDataAsync();
            
            var entities = new List<RankingEntity>();
            for (int i = 0; i < remoteRankings.Count; i++)
            {
                var r = remoteRankings[i];
                var userId = i.ToString();
                
                if (currentUser != null && r.userName == currentUser.NickName)
                {
                    userId = currentUser.UserId;
                }
                
                entities.Add(RankingDTO.ToEntity(r, userId));
            }

            _localRepo.SaveRankings(entities);
            _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, true);
            
            OnSyncCompleted?.Invoke(true);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Sync failed: {e.Message}\n{e.StackTrace}");
            _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, false, e.Message);
            
            OnSyncCompleted?.Invoke(false);
            return false;
        }
        finally
        {
            _isSyncing = false;
        }
    }

    public async Task<bool> ForceRefresh()
    {
        return await SyncRankings();
    }

    public bool IsCacheValid()
    {
        if (!_isInitialized)
            return false;
            
        return !_syncMetadataRepo.ShouldSync(RANKINGS_ENTITY_TYPE, _cacheValidityDuration);
    }

    public DateTime GetLastSyncTime()
    {
        if (!_isInitialized)
            return DateTime.MinValue;
            
        return _syncMetadataRepo.GetLastSyncTime(RANKINGS_ENTITY_TYPE);
    }

    public int GetCachedRankingsCount()
    {
        if (!_isInitialized)
            return 0;
            
        return _localRepo.GetRankingsCount();
    }
}