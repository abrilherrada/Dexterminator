using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UsernameValidator validator;
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.interactable = false;

        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Debug.Log("current username: " + GameManager.Instance.GetUsername());
        GameManager.Instance.TryToLoadLevel("Level1");
    }

    private void Update()
    {
        if (GameManager.Instance.GetUsername() != null)
        {
            startButton.interactable = true;
        }
    }
}
