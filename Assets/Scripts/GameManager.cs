using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Score Manager Settings")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentCoin = 0;
    [SerializeField] private bool isGameOver = false;
    private int rewardPerCoin = 1;
    private const string HIGH_SCORE = "HighScore";

    [Header("Boosts Settings")]
    [SerializeField] private GameObject bonusMinionsGroup;

    // Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        InGameUIManager.instance.SetPlayingPanel(true);
        InGameUIManager.instance.SetGameOverPanel(false);
        currentScore = CatGroupManager.instance.catList.Count;
        if (BoostManager.instance != null)
            ActivateSelectedBoost();
    }

    /// Handling game over
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Bat UI Game over
        InGameUIManager.instance.ActivateGameOverStage(currentScore, PlayerPrefs.GetInt(HIGH_SCORE, 0));

        // Luu so tien da thu tha duoc
        int totalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
        PlayerPrefs.SetInt("TotalCoin", totalCoin + currentCoin);
        PlayerPrefs.Save();

        // Reset lai boost da mua
        if (BoostManager.instance != null)
            BoostManager.instance.ClearBoosts();
    }

    // Restart game
    public void Restart()
    {
        isGameOver = false;
        Time.timeScale = 1f;
    }

    public void IncreaseScore()
    {
        currentScore++;
        InGameUIManager.instance.UpdateCurrentToastNumberText(CatGroupManager.instance.catList.Count);
        InGameUIManager.instance.UpdateCurrentScoreText(currentScore);
        if (currentScore > PlayerPrefs.GetInt(HIGH_SCORE, 0))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, currentScore);
        }
    }
    public void DecreaseScore()
    {
        InGameUIManager.instance.UpdateCurrentToastNumberText(CatGroupManager.instance.catList.Count);
    }
    public void IncreaseCoin()
    {
        currentCoin += rewardPerCoin;
        InGameUIManager.instance.UpdateCoinText(currentCoin);
    }

    private void ActivateSelectedBoost()
    {
        if (BoostManager.instance.isBoostActivate("Squad Arrived!"))
        {
            bonusMinionsGroup.SetActive(true);
            Debug.Log("Activate bonus toast");
        }
        if (BoostManager.instance.isBoostActivate("Double Coin"))
        {
            rewardPerCoin = 2;
            Debug.Log("Activate double coin");
        }
        if (BoostManager.instance.isBoostActivate("Magnet"))
        {
            Debug.Log("Activate begin magnet");
            StartCoroutine(ActiveMagnet(30f));
        }
    }

    IEnumerator ActiveMagnet(float magnetDuration)
    {
        CoinMagnet magnet = GameObject.FindFirstObjectByType<CoinMagnet>();
        magnet.InstanceActivate();
        yield return new WaitForSeconds(magnetDuration);
        Debug.Log("Hieu ung nam cham ket thuc");
        magnet.InstanceDeactivate();
    }

    public void DoubleCoinReward(bool isMultiple)
    {
        if (isMultiple)
            rewardPerCoin *= 2;
        else
            rewardPerCoin /= 2;
    }
}
