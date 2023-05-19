using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemGenerator : MonoBehaviour
{
    [SerializeField] private UIItem userButton;
    [SerializeField] private RectTransform parent;
    [SerializeField] private SavingManager savingManager;
    private SavedUser[] savedUsers;
    

    private void Start()
    {
        savedUsers = savingManager.GetSavedUsers();

        foreach(SavedUser user in savedUsers)
        {
            var username = user.username;
            var button = Instantiate(userButton, parent);
            button.SetText(username);
        }
    }
}
