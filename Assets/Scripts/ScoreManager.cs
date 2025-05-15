using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore,textGem;
    public int score,gem;

    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI textScoreFinal, textHighScore;

    [Space] [SerializeField] private GameObject hurtFx;
    
    private IEnumerator hurtFxCoroutine;
    public static ScoreManager instance;
    [Space]
    
    [SerializeField] private GameObject TutorialPanel;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        gem = PlayerPrefs.GetInt("gem", 1);
        textScore.text = $"x{score}";
        textGem.text = $"x{gem}";
        
        TutorialPanel.SetActive(true);
    }

    private void Update()
    {
        if (TutorialPanel.activeSelf)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        textScore.text = $"x{score}";
    }
    public void AddGem(int newGem)
    {
        gem += newGem;
        textGem.text = $"x{gem}";

        PlayerPrefs.SetInt("gem", gem);
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

    public void ShowHurtFX()
    {
        if(hurtFxCoroutine!=null) StopCoroutine(hurtFxCoroutine);
        StartCoroutine(ShowHurtFXCoroutine());
    }

    private IEnumerator ShowHurtFXCoroutine()
    {
        hurtFx.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hurtFx.SetActive(false);
    }
}
