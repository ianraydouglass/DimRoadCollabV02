using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameItem> currentItems = new List<GameItem>();
    public GameObject itemHolder;
    public NotificationManager notificationManager;


    public void AddToInventory(GameItem item)
    {
        if (item)
        {
            GameItem newItem = Instantiate(item) as GameItem;
            currentItems.Add(newItem);
            newItem.SetupItem();
            NotifyAdd(newItem);
        }
    }

    public void RemoveFromInventory(GameItem item)
    {
        if (item)
        {
            if (currentItems.Contains(item))
            {
                currentItems.Remove(item);
            }
        }
    }

    public void DropItem(Vector3 dropPosition)
    {
        if (currentItems.Count > 0)
        {
            GameItem toDrop = currentItems[0];
            GameObject i = Instantiate(itemHolder, dropPosition, transform.rotation);
            ItemObject itemDropping = i.GetComponent<ItemObject>();
            itemDropping.gameItem = toDrop;
            itemDropping.ChangeSprite();
            currentItems.Remove(toDrop);

        }
    }

    public void NotifyAdd(GameItem item)
    {
        string notifText = item.GetName() + " added to inventory.";
        notificationManager.DisplayNotification(item.GetSprite(), notifText);
    }

    public List<GameItem> GetItemsWithPartTrait(PartTrait trait)
    {
        List<GameItem> itemsWithPartTrait = new List<GameItem>();
        foreach (GameItem item in currentItems)
        {
            if (item.HasPartWithTrait(trait))
            {
                itemsWithPartTrait.Add(item);
            }
        }
        return itemsWithPartTrait;
    }

    public void CleanUpItems()
    {
        List<GameItem> itemsRemove = new List<GameItem>();
        foreach (GameItem item in currentItems)
        {
            item.ReBuildItem();
            if (item.GetRemovalFlag())
            {
                itemsRemove.Add(item);
            }
        }
        if (itemsRemove.Count == 0)
        {
            return;
        }
        foreach(GameItem item in itemsRemove)
        {
            currentItems.Remove(item);
            //notify removal
        }
    }

}
