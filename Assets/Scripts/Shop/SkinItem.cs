using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinItem", menuName = "Shop/SkinItem")]
public class SkinItem : ShopItem
{
    public int SkinID;
    public override void ApplyEffect()
    {
        Debug.Log("Unlock new skin " + SkinID);
        PlayerPrefs.SetInt("SelectedSkinID", SkinID);
        PlayerPrefs.Save();
    }
}
