using TMPro;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    [SerializeField] private TMP_Text userButtonText;

    public event System.Action<string> ButtonClicked;

    public void SetText(string text)
    {
        userButtonText.text = text;
    }

    public void OnButtonClick()
    {
        ButtonClicked?.Invoke(userButtonText.text);
    }
}
