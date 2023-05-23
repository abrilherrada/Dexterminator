using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] private GameObject ingameUI;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarBackground;

    [SerializeField] private TMP_Text collectedHealersAmount;
    [SerializeField] private TMP_Text collectedEnemiesAmount;
    [SerializeField] private TMP_Text collectedAmmunitionAmount;
    [SerializeField] private TMP_Text collectedAmmunitionAmountIG;

    private void Awake()
    {
        ingameUI.SetActive(true);
        pauseMenu.SetActive(false);

        menuButton.onClick.AddListener(GoToMainMenu);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);

        healthBar.interactable = false;
    }

    private void GoToMainMenu()
    {
        UpdateCollectedHealers();
        UpdateCollectedEnemies();

        ingameUI.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        ingameUI.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void QuitGame()
    {
        GameManager.Instance.TryToLoadLevel("MainMenu");
    }
    private void UpdateCollectedHealers()
    {
        var collectedHealers = playerController.GetCollectedHealers();
        collectedHealersAmount.text = collectedHealers.ToString();
    }

    private void UpdateCollectedAmmunition()
    {
        var collectedAmmunition = playerController.GetCollectedAmmunition();
        collectedAmmunitionAmount.text = collectedAmmunition.ToString();
        collectedAmmunitionAmountIG.text = collectedAmmunition.ToString();
    }

    private void UpdateCollectedEnemies()
    {
        var collectedEnemies = playerController.GetCollectedEnemies();
        collectedEnemiesAmount.text = collectedEnemies.ToString();
    }

    private void UpdateHealthBar()
    {
        var playerHealth = playerController.GetHealth();
        healthBar.value = playerHealth / 100;
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateCollectedAmmunition();
    }

}
