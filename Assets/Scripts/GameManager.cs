using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SavingManager savingManager;
    public static GameManager Instance;

    private string username;
    private float health;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Update()
    {
        if(levelManager.isLevelFinished)
        {
            levelManager.isLevelFinished = false;
            TryToLoadLevel("Level2");
        }
    }

    public void SetUsername(string name)
    {
        username = name;
    }

    public string GetUsername()
    {
        return username;
    }

    public float GetSavedHealth() => health;

    public void AddScore(int score)
    {
        scoreManager.Add(score);
    }

    public void TryToLoadLevel(string levelName)
    {
        levelManager.LoadLevel(levelName);
    }

    public void SaveData(float currentHealth, Vector3 playerPosition)
    {
        savingManager.SaveData(username, currentHealth, playerPosition);
    }

    public void LoadAndSaveData()
    {
        SavedCharacterData savedData = savingManager.LoadData(username);
        health = savedData.health;
        SaveData(savedData.health, savedData.initialPosition);
    }
}
