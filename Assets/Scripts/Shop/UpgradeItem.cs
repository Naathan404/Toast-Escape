using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeItem", menuName = "Shop/UpgradeItem")]
public class UpgradeItem : ShopItem
{
    public int UpgradeID;
    public override void ApplyEffect()
    {
        Debug.Log("Ungrade " + UpgradeID);
    }
}