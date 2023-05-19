using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    [SerializeField] private TMP_Text userButtonText;

    public void SetText(string text)
    {
        userButtonText.text = text;
    }

    public string GetText()
    {
        return userButtonText.text;
    }
}
