using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class BaseTab : MonoBehaviour
{
    [SerializeField] protected List<ShopItem> items;
    [SerializeField] protected Transform itemContainer;
    [SerializeField] protected GameObject itemUIPrefab;
    
    protected abstract void LoadTab();
    protected abstract void TryBuyItem(ShopItem item, Button buyButton, Image imageButton);

    // Text Button
    public void ResetPurchasedItems()
    {
        foreach (ShopItem item in items)
        {
            PlayerPrefs.DeleteKey("Purchased_" + item.itemName);

            item.isPurchased = false;
        }
        PlayerPrefs.SetInt("SelectedSkinID", 0);
        PlayerPrefs.Save();
        Debug.Log("Đã reset trạng thái mua của tất cả item.");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
