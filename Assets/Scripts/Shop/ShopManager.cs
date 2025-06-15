using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    [Header("Shop Settings")]
    public GameObject shopCanvas;
    [SerializeField] private GameObject boostsScrollView;
    [SerializeField] private GameObject skinsScrollView;
    [SerializeField] private GameObject upgradesScrollView;
    [SerializeField] private GameObject boostTabSprite;
    [SerializeField] private GameObject skinTabSprite;
    [SerializeField] private GameObject upgradeTabSprite;
    [SerializeField] private TMP_Text coinText;

    private void Start()
    {
        boostsScrollView.SetActive(true);
        skinsScrollView.SetActive(false);
        upgradesScrollView.SetActive(false);
        skinTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        upgradeTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        UpdateCoinUI();
    }

    public void OnUpgradeButtonClick()
    {
        upgradesScrollView.SetActive(true);
        skinsScrollView.SetActive(false);
        boostsScrollView.SetActive(false);
        skinTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        upgradeTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
        boostTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void OnSkinButtonClick()
    {
        boostsScrollView.SetActive(false);
        upgradesScrollView.SetActive(false);
        skinsScrollView.SetActive(true);
        upgradeTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        skinTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
        boostTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void OnBoostButtonClick()
    {
        boostsScrollView.SetActive(true);
        upgradesScrollView.SetActive(false);
        skinsScrollView.SetActive(false);
        upgradeTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        skinTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        boostTabSprite.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
    }    

    // Nut tro ve main menu
    public void OnBackToMainMenuButton()
    {
        SceneManager.LoadScene("0_MainMenu");
    }

    public void UpdateCoinUI()
    {
        coinText.text = PlayerPrefs.GetInt("TotalCoin", 0).ToString();
    }
}
