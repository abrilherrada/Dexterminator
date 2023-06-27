using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool isLevelFinished;

    private void Start()
    {
        isLevelFinished = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.TryGetComponent(out PlayerController player))
        {
            isLevelFinished = true;
            player.transform.position = player.secondInitialPosition;
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}