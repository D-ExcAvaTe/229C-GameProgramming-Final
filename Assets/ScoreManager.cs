using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    public int score;

    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI textScoreFinal, textHighScore;
    
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        textScore.text = $"x{score}";
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        textScore.text = $"x{score}";
    }

    public void GameOver()
    {
        int newScore = score;
        if (newScore > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore",newScore);
        }
        gameOverPanel.SetActive(true);
        textScoreFinal.text = "X"+score;
        textHighScore.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
