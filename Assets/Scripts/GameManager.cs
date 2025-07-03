using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [Header("Score Manager Settings")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentCoin = 0;
    [SerializeField] private bool isGameOver = false;
    private int rewardPerCoin = 1;
    private const string HIGH_SCORE = "HighScore";
    private bool isNewRecord = false;
    private bool isDoubleCoinBoostSelected = false;

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
        currentScore = CatGroupManager.instance.toastList.Count;
        if (BoostManager.instance != null)
            ActivateSelectedBoost();
    }

    /// Handling game over
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Bat UI Game over
        InGameUIManager.instance.ActivateGameOverStage(currentScore, PlayerPrefs.GetInt(HIGH_SCORE, 0), currentCoin);

        // Luu so tien da thu thap duoc
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
        InGameUIManager.instance.UpdateCurrentToastNumberText(CatGroupManager.instance.toastList.Count);
        InGameUIManager.instance.UpdateCurrentScoreText(currentScore);
        if (currentScore > PlayerPrefs.GetInt(HIGH_SCORE, 0))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, currentScore);
            if (!isNewRecord && PlayerPrefs.GetInt(HIGH_SCORE) > 1)
            {
                isNewRecord = true;
                AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);
                InGameUIManager.instance.SetNewRecordTextStatus(true);
            }
        }
    }
    public void DecreaseScore()
    {
        InGameUIManager.instance.UpdateCurrentToastNumberText(CatGroupManager.instance.toastList.Count);
    }
    public void IncreaseCoin()
    {
        int coinRewardFactor = 1;
        if (isDoubleCoinBoostSelected)
        {
            float ratio = Random.Range(0f, 1f);
            if (ratio <= 0.6f)
                coinRewardFactor = 1;
            else if (ratio > 0.6f && ratio <= 0.9f)
                coinRewardFactor = 2;
            else
                coinRewardFactor = 3;
        }
        currentCoin += rewardPerCoin * coinRewardFactor;
        InGameUIManager.instance.UpdateCoinText(currentCoin);
    }

    private void ActivateSelectedBoost()
    {
        if (BoostManager.instance.isBoostActivate("Squad Arrived!"))
        {
            bonusMinionsGroup.SetActive(true);
            Debug.Log("Activate bonus toast");
        }
        if (BoostManager.instance.isBoostActivate("MultiCoin"))
        {
            // rewardPerCoin = 2;
            isDoubleCoinBoostSelected = true;
            Debug.Log("Activate Multicoin");
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