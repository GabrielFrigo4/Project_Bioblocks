using System;
using System.Linq;
using UnityEngine;

public class LocalSyncMetadataRepository
{
    private SQLite4Unity3d.SQLiteConnection _db;

    public LocalSyncMetadataRepository(IDatabaseManager databaseManager)
    {
        _db = databaseManager.GetConnection();
    }

    public void UpdateSyncMetadata(string entityType, bool success, string error = null)
    {
        try
        {
            var metadata = GetSyncMetadata(entityType);

            if (metadata == null)
            {
                metadata = new SyncMetadataEntity
                {
                    EntityType = entityType
                };
            }

            metadata.LastSyncTimestamp = DateTime.UtcNow;
            metadata.SyncStatus = success ? "Success" : "Failed";
            metadata.LastError = error;
            metadata.SyncAttempts = success ? 0 : metadata.SyncAttempts + 1;

            if (metadata.SyncAttempts == 0 && string.IsNullOrEmpty(metadata.EntityType))
            {
                _db.Insert(metadata);
            }
            else
            {
                _db.InsertOrReplace(metadata);
            }

            Debug.Log($"[LocalSyncMetadataRepository] Updated sync metadata for {entityType}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalSyncMetadataRepository] Error updating sync metadata: {e.Message}");
        }
    }

    public SyncMetadataEntity GetSyncMetadata(string entityType)
    {
        try
        {
            return _db.Table<SyncMetadataEntity>()
                     .Where(m => m.EntityType == entityType)
                     .FirstOrDefault();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalSyncMetadataRepository] Error getting sync metadata: {e.Message}");
            return null;
        }
    }

    public DateTime GetLastSyncTime(string entityType)
    {
        var metadata = GetSyncMetadata(entityType);
        return metadata?.LastSyncTimestamp ?? DateTime.MinValue;
    }

    public bool ShouldSync(string entityType, TimeSpan maxAge)
    {
        var lastSync = GetLastSyncTime(entityType);
        var age = DateTime.UtcNow - lastSync;
        return age > maxAge;
    }
}
