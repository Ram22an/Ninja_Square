using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int LevelNumber;
    public int LevelId;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get;  set; }

    private LevelData currentLevelData;
    private string savePath;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        savePath = Path.Combine(Application.dataPath, "levelData.json");
        LoadLevelData();
    }
    public void SaveLevelData(int levelNumber, int someNumber)
    {
        currentLevelData = new LevelData
        {
            LevelNumber = levelNumber,
            LevelId = someNumber
        };

        string json = JsonUtility.ToJson(currentLevelData);
        File.WriteAllText(savePath, json);
        Debug.Log($"Saved: {json}");
    }

    public float[] LoadLevelData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentLevelData = JsonUtility.FromJson<LevelData>(json);
            Debug.Log($"Loaded: Level {currentLevelData.LevelNumber}, LevelId {currentLevelData.LevelId}");
        }
        else
        {
            currentLevelData = new LevelData { LevelNumber = 1, LevelId = 0 };
            Debug.Log("No save file found. Initialized with default values.");
        }
        return new float[] {currentLevelData.LevelNumber, currentLevelData.LevelId};
    }

    public int GetCurrentLevel()
    {
        return currentLevelData.LevelNumber;
    }

    public int GetLevelId()
    {
        return currentLevelData.LevelId;
    }
}
