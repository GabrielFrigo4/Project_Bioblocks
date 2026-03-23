using System.Collections.Generic;

/// <summary>
/// Contrato para passagem de dados entre cenas.
/// Persiste entre carregamentos de cena via DontDestroyOnLoad.
/// </summary>
public interface ISceneDataService
{
    void SetData(Dictionary<string, object> data);

    Dictionary<string, object> GetData();

    T GetValue<T>(string key);

    void ClearData();
}