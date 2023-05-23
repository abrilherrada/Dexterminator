using UnityEngine;

public class UIItemGenerator : MonoBehaviour
{
    [SerializeField] private UIItem userButton;
    [SerializeField] private RectTransform parent;
    [SerializeField] private SavingManager savingManager;

    private SavedUser[] savedUsers;
    

    private void Awake()
    {
        savedUsers = savingManager.GetSavedUsers();

        foreach(SavedUser user in savedUsers)
        {
            var username = user.username;
            var button = Instantiate(userButton, parent);
            button.SetText(username);
            button.ButtonClicked += OnUserButtonClick;
        }
    }

    private void OnUserButtonClick(string username)
    {
        GameManager.Instance.SetUsername(username);
        GameManager.Instance.LoadAndSaveData();
        
    }
}
