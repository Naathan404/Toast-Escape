using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Coin Manager Settings")]
    [SerializeField] private int totalCoin;
    private const string COIN_KEY = "TotalCoin";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Load coin that saved to totalCoin
        LoadCoin();
    }

    public int GetTotalCoin() => totalCoin;

    public void AddCoin(int amount)
    {
        totalCoin += amount;
        SaveCoin();
    }

    public void UseCoin(int amount)
    {
        if (amount < totalCoin)
        {
            Debug.Log("Not enough coin");
            return;
        }
        totalCoin -= amount;
        SaveCoin();
    }

    private void SaveCoin()
    {
        PlayerPrefs.SetInt(COIN_KEY, totalCoin);
        PlayerPrefs.Save();
    }

    private void LoadCoin()
    {
        totalCoin = PlayerPrefs.GetInt(COIN_KEY, 0);
    }
}
