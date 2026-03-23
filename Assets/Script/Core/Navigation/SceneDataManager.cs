using UnityEngine;
using System.Collections.Generic;

public class SceneDataManager : MonoBehaviour, ISceneDataService
{
    private Dictionary<string, object> _sceneData = new Dictionary<string, object>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // -------------------------------------------------------
    // ISceneDataService
    // -------------------------------------------------------

    public void SetData(Dictionary<string, object> data)
    {
        _sceneData = data;
        Debug.Log($"[SceneDataManager] Data set: {string.Join(", ", data.Keys)}");
    }

    public Dictionary<string, object> GetData() => _sceneData;

    public T GetValue<T>(string key)
    {
        if (_sceneData.ContainsKey(key))
        {
            try
            {
                return (T)_sceneData[key];
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SceneDataManager] Erro ao obter valor para key '{key}': {e.Message}");
                return default;
            }
        }

        Debug.LogWarning($"[SceneDataManager] Key '{key}' não encontrada");
        return default;
    }

    public void ClearData()
    {
        _sceneData.Clear();
        Debug.Log("[SceneDataManager] Dados limpos");
    }
}