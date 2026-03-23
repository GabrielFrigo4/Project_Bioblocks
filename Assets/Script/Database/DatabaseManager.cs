using UnityEngine;
using SQLite4Unity3d;
using System;
using System.IO;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private SQLiteConnection _connection;
    private readonly object _lock = new object();
    private bool _isInitialized = false;

    public bool IsInitialized => _isInitialized;

    // -------------------------------------------------------
    // Ciclo de vida
    // -------------------------------------------------------

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void OnApplicationQuit()
    {
        CloseConnection();
    }

    private void OnDestroy()
    {
        CloseConnection();
    }

    // -------------------------------------------------------
    // Inicialização
    // -------------------------------------------------------

    public void Initialize()
    {
        if (_isInitialized) return;

        try
        {
            DatabaseConfig.EnsureDatabaseDirectory();
            _connection = new SQLiteConnection(
                DatabaseConfig.DatabasePath,
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create
            );
            CreateTables();
            _isInitialized = true;
            Debug.Log($"[DatabaseManager] Database initialized at: {DatabaseConfig.DatabasePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[DatabaseManager] Failed to initialize database: {e.Message}");
            _isInitialized = false;
            throw;
        }
    }

    private void CreateTables()
    {
        lock (_lock)
        {
            _connection.CreateTable<RankingEntity>();
            _connection.CreateTable<CachedImageEntity>();
            _connection.CreateTable<SyncMetadataEntity>();
            Debug.Log("[DatabaseManager] Tables created successfully");
        }
    }

    // -------------------------------------------------------
    // IDatabaseManager
    // -------------------------------------------------------

    public SQLiteConnection GetConnection()
    {
        if (_connection == null)
            Initialize();
        return _connection;
    }

    public void ExecuteInTransaction(Action action)
    {
        lock (_lock)
        {
            _connection.RunInTransaction(action);
        }
    }

    public void ClearAllData()
    {
        lock (_lock)
        {
            _connection.DeleteAll<RankingEntity>();
            _connection.DeleteAll<CachedImageEntity>();
            _connection.DeleteAll<SyncMetadataEntity>();
            Debug.Log("[DatabaseManager] All data cleared");
        }
    }

    public void DeleteDatabase()
    {
        lock (_lock)
        {
            CloseConnection();
            DatabaseConfig.DeleteDatabase();
            _isInitialized = false;
            Debug.Log("[DatabaseManager] Database deleted");
        }
    }

    // -------------------------------------------------------
    // Helper privado
    // -------------------------------------------------------

    private void CloseConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }
}