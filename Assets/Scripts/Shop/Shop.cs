using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopItem[] itemsForSale;

    private GameManager gameManager;

    public static Action shopUpdated;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        return gameManager.GetCoins();
    }

    public bool PurchaseItem(int index)
    {
        ShopItem item = itemsForSale[index];

        bool success = item.GetPrice() <= gameManager.GetCoins();
        if (!success)
        {
            Debug.Log("Not enough money to purchase " + item.GetDisplayName());
            return false;
        }

        Debug.Log("Purchased " + item.GetDisplayName());
        item.Purchase();
        gameManager.AddCoins(-item.GetPrice());
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
    }

    public ShopItem[] GetShopItems()
    {
        return itemsForSale;
    }
}
