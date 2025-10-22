using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject lobbyScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button[] homeButtons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayLevelButtonClick);
        continueButton.onClick.AddListener(ContinueButtonClick);
        restartButton.onClick.AddListener(RestartLevelButtonClick);
        foreach (var homeButton in homeButtons)
        {
            homeButton.onClick.AddListener(HomeButtonClick);
        }

        ShowLobbyScreen();
    }

    public void ShowLobbyScreen()
    {
        lobbyScreen.SetActive(true);
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void ShowGameplayScreen()
    {
        lobbyScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        lobbyScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void PlayLevelButtonClick()
    {
        GameManager.Instance.StartLevel();
        ShowGameplayScreen();
    }

    private void RestartLevelButtonClick()
    {
        GameManager.Instance.RestartLevel();
        ShowGameplayScreen();
    }

    private void HomeButtonClick()
    {
        ShowLobbyScreen();
    }

    private void ContinueButtonClick()
    {
        GameManager.Instance.PlayNextLevel();
        ShowGameplayScreen();
    }
}
