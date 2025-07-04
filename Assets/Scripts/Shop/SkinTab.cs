using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class SkinTab : BaseTab
{
    void Start()
    {
        LoadTab();
        ShopManager.instance.UpdateCoinUI();
    }

    protected override void LoadTab()
    {
        int selectedSkinID = PlayerPrefs.GetInt("SelectedSkinID", 0);
        foreach (ShopItem item in items)
        {
            SkinItem skinItem = item as SkinItem;
            item.isPurchased = PlayerPrefs.GetInt("Purchased_" + item.itemName, 0) == 1;
            if (skinItem.SkinID == 0) item.isPurchased = true;
            // Hien thi UI
            GameObject itemUI = Instantiate(itemUIPrefab, itemContainer);
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
            itemUI.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = item.price.ToString();
            itemUI.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            itemUI.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;

            Button buyButton = itemUI.transform.Find("BuyButton").GetComponent<Button>();
            Image imageButton = itemUI.transform.Find("BuyButton").GetComponent<Image>();
            TextMeshProUGUI buttonText = buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Kiem tra da mua hay chua, neu da mua thi hien "Owned" khong hien "Buy"
            // PlayerPrefs.GetInt(item.itemName, 0) == 1
            if (item.isPurchased)
            {
                //itemUI.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "Owed";
                if (skinItem.SkinID == selectedSkinID)
                {
                    buttonText.text = "IN USE";
                    imageButton.color = new Color(0f, 1f, 0f, 1f);
                    buyButton.interactable = false;
                }
                else
                {
                    buttonText.text = "USE";
                    imageButton.color = new Color(0f, 1f, 0f, 1f);
                    buyButton.onClick.AddListener(() =>
                    {
                        SelectSkin(skinItem, buttonText);
                    });
                }
            }

            if (!item.isPurchased)
            {
                buyButton.onClick.AddListener(() =>
                {
                    TryBuyItem(item, buyButton, imageButton);
                });
                buttonText.text = "BUY";
                imageButton.color = new Color(1f, 0.5f, 0.5f, 1f);
            }
        }
    }

    protected override void TryBuyItem(ShopItem item, Button buyButton, Image imageButton)
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoin", 0);

        if (currentCoins >= item.price && item.isPurchased == false)
        {
            PlayerPrefs.SetInt("TotalCoin", currentCoins - item.price);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Purchased_" + item.itemName, 1);

            AudioManager.instance.PlaySFX(AudioManager.instance.accept);

            // Dat skin moi mua lam skin su dung
            buyButton.interactable = false;
            SkinItem skinItem = item as SkinItem;
            PlayerPrefs.SetInt("SelectedSkinID", skinItem.SkinID);
            PlayerPrefs.Save();
            // Cap nhat so tien con lai
            ShopManager.instance.UpdateCoinUI();
            // Goi chuc nang cua item vua mua
            item.ApplyEffect();
            // Reload lai Tab de cap nhat UI
            ReloadTab();
        }
        else
        {
            Debug.Log("Không đủ tiền hoặc đã mua");
            AudioManager.instance.PlaySFX(AudioManager.instance.cancle);
            imageButton.color = new Color(1f, 0.5f, 0.5f, 1f);
        }
    }

    void SelectSkin(SkinItem skinItem, TextMeshProUGUI buttonText)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.fillerSfx);
        PlayerPrefs.SetInt("SelectedSkinID", skinItem.SkinID);
        PlayerPrefs.Save();
        Debug.Log("Skin selected: " + skinItem.itemName);
        ReloadTab();
    }

    void ReloadTab()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        LoadTab();
    }

    void ResetColor(Image image)
    {
        image.color = new Color(1f, 0.5f, 0.5f, 1f);
    }
}