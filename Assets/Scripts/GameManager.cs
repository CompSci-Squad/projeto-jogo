using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 3;
    public int score = 0;

    public TMP_Text scoreText;
    public GameObject[] hearts;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public TMP_Text gameOverScoreText;
    public TMP_Text winScoreText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateHearts();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Moedas: " + score;
        Time.timeScale = 0f; // pausa o jogo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        scoreText.text = "Moedas: " + score;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        winScoreText.text = "Moedas: " + score;
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}