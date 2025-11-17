using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   // TextMeshPro
using UnityEngine.SceneManagement;   // put this at the top of the file with the other using lines


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemy Prefabs")]
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    [Header("Powerup Prefabs")]
    public GameObject coinPrefab;
    public GameObject heartPrefab;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header("Values")]
    public int score = 0;
    public int lives = 3;    // start with 3, max 3

    [Header("Game Over")]
    public GameObject gameOverPanel;   // drag GameOverPanel here in Inspector

    private bool isGameOver = false;



    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Enemies
        InvokeRepeating("CreateEnemyOne", 1f, 2f);
        InvokeRepeating("CreateEnemyTwo", 1.5f, 3.25f);

        // Powerups
        InvokeRepeating("SpawnCoin", 3f, 5f);
        InvokeRepeating("SpawnHeart", 8f, 8f);

        UpdateUI();
    }

    void Update()
    {
    }

    // -------- Enemy Spawning --------

    void CreateEnemyOne()
    {
        if (enemyOnePrefab == null) return;
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0f), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        if (enemyTwoPrefab == null) return;
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0f), Quaternion.identity);
    }

    // -------- Powerup Spawning --------

    void SpawnCoin()
    {
        if (coinPrefab == null) return;
        Instantiate(coinPrefab, RandomPositionInPlayerBand(), Quaternion.identity);
    }

    void SpawnHeart()
    {
        if (heartPrefab == null) return;
        Instantiate(heartPrefab, RandomPositionInPlayerBand(), Quaternion.identity);
    }

    Vector3 RandomPositionInPlayerBand()
    {
        Camera cam = Camera.main;

        // bottom-left and bottom-right of the camera
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));

        float minX = bottomLeft.x;
        float maxX = bottomRight.x;
        float minY = bottomLeft.y;
        float maxY = 0f; // middle of the screen (player's top limit)

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, 0f);
    }

    // -------- Score & Lives --------

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void AddLife(int amount)
    {
        lives += amount;
        if (lives > 3) lives = 3;

        if (lives <= 0)
        {
            lives = 0;
            UpdateUI();
            GameOver();
            return;
        }

        UpdateUI();
    }



    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();

        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();
    }

    void GameOver()
    {
        if (isGameOver) return;   // prevent double-call
        isGameOver = true;

        // stop all the InvokeRepeating spawns
        CancelInvoke();

        // pause the game
        Time.timeScale = 0f;

        // show the Game Over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        // un-pause
        Time.timeScale = 1f;

        // reload current scene
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }


}

