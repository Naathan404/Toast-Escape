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

            // Kiem tra da mua hay chua, neu da mua thi hien "Owned" khong hien "Buy"
            // PlayerPrefs.GetInt(item.itemName, 0) == 1
            if (BoostManager.instance.isBoostActivate(item.itemName))
            {
                buyButton.interactable = false;
                buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
            }

            buyButton.onClick.AddListener(() =>
            {
                TryBuyItem(item, buyButton);
            });
        }
    }

    protected override void TryBuyItem(ShopItem item, Button buyButton)
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoin", 0);

        if (currentCoins >= item.price)
        {
            PlayerPrefs.SetInt("TotalCoin", currentCoins - item.price);
            PlayerPrefs.Save();

            // Cap nhat nut bam
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";

            // Cap nhat so tien con lai
            ShopManager.instance.UpdateCoinUI();

            // Goi chuc nang cua item vua mua
            BoostManager.instance.ActivateBoost(item.itemName);
            item.ApplyEffect();
        }
        else
        {
            Debug.Log("Khong du tien hoac item da bi mua");
        }
    }
}
