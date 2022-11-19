using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image image = null;
    [SerializeField] private TextMeshProUGUI costLabel = null;
    [SerializeField] private float purchasedAlphaDim = 0.2f;

    private int index;
    private ShopItem item;

    // Called by button click
    public void OnClick()
    {
        if (item == null || item.GetPurchased())
        {
            return;
        }

        PurchaseItem();
    }

    private void PurchaseItem()
    {
        Shop.GetShop().PurchaseItem(index);
    }

    public void SetItem(ShopItem item, int index)
    {
        this.index = index;
        this.item = item;
        image.sprite = item.GetSprite();
        costLabel.text = "Cost: " + item.GetPrice().ToString();

        if (item.GetPurchased())
        {
            //priceText.text = "Purchased!";
            var c = image.color;
            c.a = purchasedAlphaDim;
            image.color = c;
        }
        else
        {
            var c = image.color;
            c.a = 1f;
            image.color = c;
        }
    }
}
