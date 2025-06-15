using UnityEngine;

[CreateAssetMenu(fileName = "NewBoostItem", menuName = "Shop/BoostItem")]
public class BoostItem : ShopItem
{
    public int BoostID;
    public override void ApplyEffect()
    {
        Debug.Log("Da mua " + this.name + " cho lan choi tiep theo");
    }
}
