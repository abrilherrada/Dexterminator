using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SavingManager savingManager;
    public static GameManager Instance;

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
        savingManager.SaveData(mainMenu.username, currentHealth, playerPosition);
    }

    public void LoadData()
    {
        savingManager.LoadData(mainMenu.username);
    }
}
