using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public LevelData levelData;
    public GameController gameController;

    public int currentLevel = 1;
    public int currentScore = 0;
    public int totalScore = 0;

    private int levelScore;
    private int freeClicks;
    private int usedClicks;

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
        totalScore = PlayerPrefs.GetInt("totalscore", 0);
    }

    public void StartLevel()
    {
        currentLevel = PlayerPrefs.GetInt("level",0);
        StartLevel(currentLevel);
    }

    public void PlayNextLevel()
    {
        StartLevel(currentLevel);
    }

    public void RestartLevel()
    {
        StartLevel(currentLevel);
    }

    public void StartLevel(int levelNumber)
    {
        if (levelNumber >= levelData.levelInfo.Length)
        {
            levelNumber = levelData.levelInfo.Length-1;
        }

        currentLevel = levelNumber;

        LevelInfo info = levelData.levelInfo[levelNumber];

        // Initialize scoring - Temporary scoring mechanism
        levelScore = (info.rows + info.columns) * 100;
        freeClicks = (info.rows + info.columns) * 2;
        usedClicks = 0;
        currentScore = levelScore;


        gameController.StartLevel(info);
    }

    public void RegisterClick()
    {
        usedClicks++;

        if (usedClicks > freeClicks)
        {
            currentScore -= 10;
            if (currentScore < 100)
                currentScore = 100;
        }
    }

    public void OnGameWon()
    {
        Debug.Log("Game Won!");

        // Proceed to next level
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);

        if (currentLevel > levelData.levelInfo.Length)
        {
            Debug.Log("No more levels. Restarting with last level info");
            currentLevel = levelData.levelInfo.Length-1;
        }

        //Score update and save
        totalScore += currentScore;
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("totalscore", totalScore);

        StartCoroutine(WaitBeforeLoadingNextLevel());
    }

    IEnumerator WaitBeforeLoadingNextLevel()
    {
        yield return new WaitForSeconds(1f);
        //StartLevel(currentLevel);
        UIManager.Instance.ShowGameOverScreen();
    }
}
