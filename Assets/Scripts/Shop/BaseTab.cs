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
    protected abstract void TryBuyItem(ShopItem item, Button buyButton);

    // Text Button
    public void ResetPurchasedItems()
    {
        foreach (ShopItem item in items)
        {
            // Xóa key lưu trạng thái mua trong PlayerPrefs (nếu bạn lưu thủ công theo key riêng)
            PlayerPrefs.DeleteKey("Purchased_" + item.itemName);

            // Reset biến isPurchased trong ScriptableObject
            item.isPurchased = false;
        }
        PlayerPrefs.SetInt("SelectedSkinID", 0);
        PlayerPrefs.Save();
        Debug.Log("Đã reset trạng thái mua của tất cả item.");

        // Reload lại shop UI để thấy thay đổi
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
