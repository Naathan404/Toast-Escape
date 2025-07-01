using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostsTab : BaseTab
{
    void Start()
    {
        LoadTab();
        ShopManager.instance.UpdateCoinUI();
    }

    protected override void LoadTab()
    {
        foreach (ShopItem item in items)
        {
            item.isPurchased = PlayerPrefs.GetInt("Purchased_" + item.itemName, 0) == 1;
            // Hien thi UI
            GameObject itemUI = Instantiate(itemUIPrefab, itemContainer);
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
            itemUI.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = item.price.ToString();
            itemUI.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            itemUI.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;

            Button buyButton = itemUI.transform.Find("BuyButton").GetComponent<Button>();
            Image imageButton = itemUI.transform.Find("BuyButton").GetComponent<Image>();
            imageButton.color = new Color(1f, 0.5f, 0.5f, 1f);

            // Kiem tra da mua hay chua, neu da mua thi hien "Owned" khong hien "Buy"
            // PlayerPrefs.GetInt(item.itemName, 0) == 1
            if (BoostManager.instance.isBoostActivate(item.itemName))
            {
                buyButton.interactable = false;
                buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
                imageButton.color = new Color(0f, 1f, 0f, 1f);
            }

            buyButton.onClick.AddListener(() =>
            {
                TryBuyItem(item, buyButton, imageButton);
            });
        }
    }

    protected override void TryBuyItem(ShopItem item, Button buyButton, Image imageButton)
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoin", 0);

        if (currentCoins >= item.price)
        {
            PlayerPrefs.SetInt("TotalCoin", currentCoins - item.price);
            PlayerPrefs.Save();

            // Cap nhat nut bam
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
            imageButton.color = new Color(0f, 1f, 0f, 1f);

            // Cap nhat so tien con lai
            ShopManager.instance.UpdateCoinUI();

            // Goi chuc nang cua item vua mua
            BoostManager.instance.ActivateBoost(item.itemName);
            item.ApplyEffect();
        }
        else
        {
            Debug.Log("Khong du tien hoac item da bi mua");
            imageButton.color = new Color(1f, 0.5f, 0.5f, 1f);
        }
    }
}
