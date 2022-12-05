using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopItem[] itemsForSale;

    private Inventory inventory;

    public static Action shopUpdated;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        foreach (var item in itemsForSale)
        {
            item.ResetPurchasedState();
        }
    }

    // Any script can retrieve the shop instance
    public static Shop GetShop()
    {
        return FindObjectOfType<Shop>();
    }

    public int GetPlayerCoins()
    {
        return inventory.GetCoins();
    }

    public bool PurchaseItem(int index)
    {
        ShopItem item = itemsForSale[index];

        bool success = item.GetPrice() <= inventory.GetCoins();
        if (!success)
        {
            //Debug.Log("Not enough money to purchase " + item.GetDisplayName());
            return false;
        }

        //Debug.Log("Purchased " + item.GetDisplayName());
        item.Purchase();
        inventory.GainCoins(-item.GetPrice());
        ApplyItemEffect(index);

        if (shopUpdated != null)
        {
            shopUpdated();
        }
        return true;
    }

    private void ApplyItemEffect(int index)
    {
        // EXTRA LOGIC TO DETERMINE HOW ITEM AT INDEX index SHOULD CHANGE GAMEPLAY
        // For now, the only item for sale is full health restoration
        FindObjectOfType<PlayerMovement>().GetComponent<Health>().Heal(100);
    }

    public ShopItem[] GetShopItems()
    {
        return itemsForSale;
    }
}
