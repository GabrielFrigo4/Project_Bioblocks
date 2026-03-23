using System;
using SQLite4Unity3d;

/// <summary>
/// Contrato para gerenciamento do banco de dados SQLite local.
/// </summary>
public interface IDatabaseManager
{
    bool IsInitialized { get; }

    SQLiteConnection GetConnection();

    void ExecuteInTransaction(Action action);

    void ClearAllData();

    void DeleteDatabase();
}