using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text totalScoreText;

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

    /// <summary>
    /// Initialize buttons and screens
    /// </summary>
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

    /// <summary>
    /// Enable Lobby
    /// </summary>
    public void ShowLobbyScreen()
    {
        lobbyScreen.SetActive(true);
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    /// <summary>
    /// Enable gameplay screen
    /// </summary>
    public void ShowGameplayScreen()
    {
        lobbyScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        levelNumberText.text = "Level " + (GameManager.Instance.currentLevel+1).ToString();
    }

    /// <summary>
    /// Enable game over screen
    /// </summary>
    public void ShowGameOverScreen()
    {
        lobbyScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        scoreText.text="Score: "+GameManager.Instance.currentScore.ToString();
        totalScoreText.text = "Total Score: " + GameManager.Instance.totalScore.ToString();
        AudioManager.Instance.PlayGameOver();
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
