using System.Diagnostics.Contracts;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public int price;
    public bool isPurchased;
    public abstract void ApplyEffect();
}