using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTab : BaseTab
{
    void Start()
    {
        LoadTab();
        ShopManager.instance.UpdateCoinUI();
    }

    protected override void LoadTab()
    {
        // foreach (ShopItem item in items)
        // {
        //     item.isPurchased = PlayerPrefs.GetInt("Purchased_" + item.itemName, 0) == 1;
        //     // Hien thi UI
        //     GameObject itemUI = Instantiate(itemUIPrefab, itemContainer);
        //     itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        //     itemUI.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = item.price.ToString();
        //     itemUI.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
        //     itemUI.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;

        //     Button buyButton = itemUI.transform.Find("BuyButton").GetComponent<Button>();

        //     // Kiem tra da mua hay chua, neu da mua thi hien "Owned" khong hien "Buy"
        //     // PlayerPrefs.GetInt(item.itemName, 0) == 1
        //     if (item.isPurchased)
        //     {
        //         buyButton.interactable = false;
        //         buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
        //     }

        //     buyButton.onClick.AddListener(() =>
        //     {
        //         TryBuyItem(item, buyButton);
        //     });
        // }
    }

    protected override void TryBuyItem(ShopItem item, Button buyButton)
    {
        // int currentCoins = PlayerPrefs.GetInt("TotalCoin", 0);

        // if (currentCoins >= item.price && item.isPurchased == false)
        // {
        //     PlayerPrefs.SetInt("TotalCoin", currentCoins - item.price);
        //     item.isPurchased = true; // Danh dau da mua
        //     PlayerPrefs.SetInt("Purchased_" + item.itemName, 1);
        //     PlayerPrefs.Save();
        //     // Tat tuong tac va hien "Owned"
        //     buyButton.interactable = false;
        //     buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
        //     // Cap nhat so tien con lai
        //     ShopManager.instance.UpdateCoinUI();
        //     // Goi chuc nang cua item vua mua
        //     item.ApplyEffect();
        // }
        // else
        // {
        //     Debug.Log("Không đủ tiền hoặc đã mua");
        // }
    }
}
