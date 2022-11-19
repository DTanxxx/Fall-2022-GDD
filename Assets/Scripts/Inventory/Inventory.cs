using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int coins = 10;

    public int GetCoins()
    {
        return coins;
    }

    public void GainCoins(int amount)
    {
        coins += amount;
        if (Shop.shopUpdated != null)
        {
            Shop.shopUpdated();
        }
    }
}
