using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameValidator : MonoBehaviour
{
    [SerializeField] private SavingManager savingManager;

    [SerializeField] private TMP_InputField enteredName;

    [SerializeField] private TMP_Text callToActionMessage;
    [SerializeField] private TMP_Text errorMessage; 
    [SerializeField] private TMP_Text successMessage;

    [SerializeField] private Button startButton;

    private void Awake()
    {
        var existingUsers = savingManager.GetSavedUsers().Count();
        if (existingUsers > 7)
        {
            enteredName.interactable = false;
        }
    }

    private void Start()
    {
        errorMessage.enabled = false;
        successMessage.enabled = false;
        callToActionMessage.enabled = true;

        enteredName.onValueChanged.AddListener(OnValueChangeHandler);
        enteredName.onEndEdit.AddListener(OnEndEditHandler);
    }

    private void OnValueChangeHandler(string data)
    {
        errorMessage.enabled = enteredName.text.Length < 3;
        callToActionMessage.enabled = false;

        if (enteredName.text.Length > 3)
        {
            GameManager.Instance.SetUsername(enteredName.text);
            successMessage.enabled = true;
        }
        else
        {
            errorMessage.enabled = true;
        }
    }

    private void OnEndEditHandler(string data)
    {
        GameManager.Instance.SaveData(100, Vector3.zero);
    }
}
