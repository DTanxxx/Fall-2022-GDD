using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("GDD/ShopItem"))]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private int price;
    [SerializeField] private Sprite sprite;
    // EXTRA FIELD TO BE ADDED FOR WHAT THIS ITEM ACTUALLY IS

    private bool purchased = false;

    public int GetPrice()
    {
        return price;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public bool GetPurchased()
    {
        return purchased;
    }

    public void ResetPurchasedState()
    {
        purchased = false;
    }

    public void Purchase()
    {
        purchased = true;
    }
}
