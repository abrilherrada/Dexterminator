using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SavingManager savingManager;
    public static GameManager Instance;

    [SerializeField] private PlayerController player;

    private string username;
    private float health;
    private int ammunition;

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

    public int GetSavedAmmunition() => ammunition;

    public void AddScore(int score)
    {
        scoreManager.Add(score);
    }

    public int GetScore()
    {
        var score = scoreManager.Get();
        return score;
    }

    public void SetScore(int score)
    {
        scoreManager.Set(score);
    }

    public void TryToLoadLevel(string levelName)
    {
        levelManager.LoadLevel(levelName);
    }

    public void SaveData(float currentHealth, int currentAmmunition, int currentScore)
    {
        savingManager.SaveData(username, currentHealth, currentAmmunition, currentScore);
        LoadData();
    }

    public void LoadData()
    {
        SavedCharacterData savedData = savingManager.LoadData(username);
        health = savedData.health;
        ammunition = savedData.ammunition;
        SetScore(savedData.score);
        //SaveData(savedData.health, savedData.initialPosition);
    }
}
