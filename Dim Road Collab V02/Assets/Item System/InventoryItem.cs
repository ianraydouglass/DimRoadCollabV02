using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image itemImage;
    public GameItem item;
    public InventoryMenuManager inventoryMenu;
    //public GameObject partHome;/
    private Color heldColor = new Color32(253, 140, 87, 255);
    private Color startingColor = new Color32(255, 255, 255, 255);
    public ItemCardHolder itemCard;
    public bool heldForDrop;

    public void ShowInventoryDescription()
    {
        itemCard.DisplayItemObjectInfo(item);
    }

    public void HideThisDescription()
    {
        inventoryMenu.ShowCurrentItem();
    }

    public void HoldThis()
    {
        heldForDrop = true;
        inventoryMenu.HoldNew(this);
    }

    public void CheckColor()
    {
        if (heldForDrop)
        {
            itemImage.color = heldColor;
        }
        else
        {
            itemImage.color = startingColor;
        }
    }



}
