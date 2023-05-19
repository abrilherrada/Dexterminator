using System.Collections;
using System.Collections.Generic;
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

    private string username;

    public string GetUsername()
    {
        return username;
    }

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
        startButton.interactable = false;

        enteredName.onValueChanged.AddListener(OnValueChangeHandler);
    }

    private void OnValueChangeHandler(string data)
    {
        errorMessage.enabled = enteredName.text.Length < 3;
        callToActionMessage.enabled = false;

        if (enteredName.text.Length > 3)
        {
            username = enteredName.text;
            successMessage.enabled = true;
            startButton.interactable = true;
        }
        else
        {
            errorMessage.enabled = true;
        }
    }
}
