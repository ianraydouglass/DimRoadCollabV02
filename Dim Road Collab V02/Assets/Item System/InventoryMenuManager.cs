using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuManager : MonoBehaviour
{
    public ItemCardHolder itemCard;
    public GameObject inventoryPanel;
    public InventoryItem currentItem;
    public InventoryManager inventoryManager;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryCanvas;
    public GameObject dropButton;

    public void OpenInventoryMenu()
    {
        inventoryCanvas.SetActive(true);
        RefreshInventoryPanel();
    }

    public void DropCurrentItem()
    {
        if (!currentItem)
        {
            return;
        }
        currentItem.heldForDrop = false;
        inventoryManager.DropItemByItem(currentItem.item);
        RefreshInventoryPanel();

    }

    public void RefreshInventoryPanel()
    {
        dropButton.SetActive(false);
        currentItem = null;
        itemCard.HideCardContents();
        foreach (Transform t in inventoryPanel.transform)
        {
            GameObject.Destroy(t.gameObject);
        }

        foreach (GameItem item in inventoryManager.currentItems)
        {
            GameObject i = Instantiate(inventoryItemPrefab, inventoryPanel.transform);
            InventoryItem thisItem = i.GetComponent<InventoryItem>();
            thisItem.itemCard = itemCard;
            thisItem.item = item;
            thisItem.itemImage.sprite = item.GetSprite();
            thisItem.inventoryMenu = this;


        }
    }

    public void ShowCurrentItem()
    {
        if (!currentItem)
        {
            itemCard.HideCardContents();
            return;
        }
        currentItem.ShowInventoryDescription();
    }

    public void ReleasePrevious()
    {
        currentItem.heldForDrop = false;
        currentItem.CheckColor();
    }

    public void HoldNew(InventoryItem newCurrent)
    {
        if (currentItem)
        {
            ReleasePrevious();
        }
        currentItem = newCurrent;
        newCurrent.CheckColor();
        dropButton.SetActive(true);
    }


}
