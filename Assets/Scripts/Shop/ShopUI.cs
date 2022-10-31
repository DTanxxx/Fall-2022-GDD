using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel = null;
    [SerializeField] private GameObject itemSlot = null;
    [SerializeField] private TextMeshProUGUI coinText = null;

    private Shop shop;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
    }

    private void OnEnable()
    {
        Shop.shopUpdated += UpdateUI;
    }

    private void OnDestroy()
    {
        Shop.shopUpdated -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        ShopItem[] shopItems = shop.GetShopItems();
        for (int i = 0; i < shopItems.Length; ++i)
        {
            var slotInstance = Instantiate(itemSlot, transform);
            slotInstance.GetComponent<SlotUI>().SetItem(shopItems[i], i);
        }

        coinText.text = "Coins: " + shop.GetPlayerCoins().ToString();
    }
}
