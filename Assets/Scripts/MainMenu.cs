using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UsernameValidator validator;
    [SerializeField] private Button startButton;
    [SerializeField] private UIItem user;
    [SerializeField] private Button userButton;
    public string username;

    private void Awake()
    {
        username = validator.GetUsername();

        startButton.onClick.AddListener(StartGame);
        userButton.onClick.AddListener(SetUser);

        Debug.Log($"usuario awake {username}");
    }

    private void SetUser()
    {
        username = user.GetText();
        Debug.Log($"usuario seteado {username}");
    }

    private void StartGame()
    {

        GameManager.Instance.SaveData(100, Vector3.zero);
    }


}
