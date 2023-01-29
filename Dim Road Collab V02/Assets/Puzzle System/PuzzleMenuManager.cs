using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleMenuManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public ItemCardHolder itemCard;
    public GameObject inventoryPanel;
    public InventoryItem currentItem;
    public InventoryManager inventoryManager;
    public GameObject inventoryItemPrefab;
    public GameObject itemPuzzleCanvas;
    public TextMeshProUGUI warningText;
    public GameObject useButton;
    public GameEvent closeEvent;
    public bool itemConsume;
    public GameObject puzzleObject;

    public void SetPuzzleObject(GameObject puzzle)
    {
        puzzleObject = puzzle;
    }
    public void OpenPuzzleMenu(string title, PartTrait trait, bool consumeItem)
    {
        itemPuzzleCanvas.SetActive(true);
        titleText.text = title;
        if (consumeItem == true)
        {
            warningText.text = "The chosen item will be consumed";
            itemConsume = true;
        }
        else
        {
            warningText.text = "The chosen item may take damage";
            itemConsume = false;
        }
        RefreshInventoryPanel(trait);

    }

    public void RefreshInventoryPanel(PartTrait trait)
    {
        useButton.SetActive(false);
        currentItem = null;
        itemCard.HideCardContents();
        foreach (Transform t in inventoryPanel.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
        //if there are no qualifying items, should display text to that effect
        int inventoryItems = 0;
        foreach (GameItem item in inventoryManager.currentItems)
        {
            
            //we want the menu to show items with the item-level trait, AND items with a part with that trait.
            //but we don't want one item to show up twice.
            if (item.HasItemTrait(trait))
            {
                GameObject i = Instantiate(inventoryItemPrefab, inventoryPanel.transform);
                InventoryItem thisItem = i.GetComponent<InventoryItem>();
                thisItem.itemCard = itemCard;
                thisItem.item = item;
                thisItem.itemImage.sprite = item.GetSprite();
                thisItem.puzzleMenu = this;
                inventoryItems += 1;
            }
            else if(item.HasPartWithTrait(trait))
            {
                GameObject i = Instantiate(inventoryItemPrefab, inventoryPanel.transform);
                InventoryItem thisItem = i.GetComponent<InventoryItem>();
                thisItem.itemCard = itemCard;
                thisItem.item = item;
                thisItem.itemImage.sprite = item.GetSprite();
                thisItem.puzzleMenu = this;
                inventoryItems += 1;
            }
            
        }
        if(inventoryItems == 0)
        {
            warningText.text = "No items have trait: " + trait.GetName();
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
        useButton.SetActive(true);
    }

    public void UseCurrentItem()
    {
        if (!currentItem)
        {
            return;
        }
        currentItem.heldForDrop = false;
        if(itemConsume)
        {
            inventoryManager.RemoveFromInventory(currentItem.item);
        }
        else
        {
            //damage item once we add durability
        }
        
        ClosePuzzleMenu();
        puzzleObject.BroadcastMessage("PuzzleActive");

    }

    public void ClosePuzzleMenu()
    {
        itemPuzzleCanvas.SetActive(false);
        closeEvent.Raise();
    }
}
