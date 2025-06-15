using UnityEngine;
using System.Collections;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    // Singleton
    public static InGameUIManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    [Header("Panel Control")]
    [SerializeField] private GameObject playingPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private CanvasGroup screenFadeCanvas;
    [SerializeField] private float slowDuration;
    [SerializeField] private float minTimeScale;
    [SerializeField] private float fadingTime;

    [Header("Text Settings")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text toastNumberText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;


    public void SetPlayingPanel(bool isActivate)
    {
        playingPanel.SetActive(isActivate);
    }

    public void SetGameOverPanel(bool isActivate)
    {
        gameOverPanel.SetActive(isActivate);
    }
    

    public void ActivateGameOverStage(int finalScore, int highScore)
    {
        StartCoroutine(SetStagetGameOver(finalScore, highScore));
    }

    private IEnumerator SetStagetGameOver(int finalScore, int highScore)
    {
        // Stage 1: Slow down screen
        AudioManager.instance.FadeOutMusic();
        float timer = 0f;
        while (timer < slowDuration)
        {
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1f, minTimeScale, timer / slowDuration);
            yield return null;
        }
        Time.timeScale = 0f;

        // Stage 2: Make screen grey
        if (screenFadeCanvas)
        {
            screenFadeCanvas.gameObject.SetActive(true);
            float fadeProgress = 0f;

            while (fadeProgress < 1f)
            {
                fadeProgress += Time.unscaledDeltaTime / fadingTime;
                screenFadeCanvas.alpha = fadeProgress;
                yield return null;
            }
        }

        // Stage 3: Pop up gaeme over UI
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
            UpdateFinalScoreText(finalScore);
            UpdateHighScoreText(highScore);
        }
        playingPanel.SetActive(false);
    }


    public void UpdateCoinText(int coinAmount)
    {
        coinText.text = "X" + coinAmount;
    }

    public void UpdateCurrentToastNumberText(int toastNumber)
    {
        toastNumberText.text = "X" + toastNumber;
    }

    public void UpdateCurrentScoreText(int currentScore)
    {
        scoreText.text = "X" + currentScore;
    }

    public void UpdateFinalScoreText(int finalScore)
    {
        finalScoreText.text = finalScore.ToString();
    }

    public void UpdateHighScoreText(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }
}
