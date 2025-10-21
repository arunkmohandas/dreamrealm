using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public LevelData levelData;
    public GameController gameController;

    public int currentLevel = 1;
    public int score = 0;

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
        currentLevel = PlayerPrefs.GetInt("level",0);
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

        Debug.Log($"Starting Level {levelNumber}: {info.rows}x{info.columns}");

        gameController.StartLevel(info);
    }

    public void OnGameWon()
    {
        Debug.Log("Game Won!");

        score += 100; // add example score

        // Proceed to next level
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);

        if (currentLevel > levelData.levelInfo.Length)
        {
            Debug.Log("No more levels. Restarting last level...");
            currentLevel = levelData.levelInfo.Length-1;
        }
        PlayerPrefs.SetInt("level", currentLevel);
        // Restart the same or next level
        StartCoroutine(WaitBeforeLoadingNextLevel());
    }

    IEnumerator WaitBeforeLoadingNextLevel()
    {
        yield return new WaitForSeconds(3);
        StartLevel(currentLevel);
    }
}
