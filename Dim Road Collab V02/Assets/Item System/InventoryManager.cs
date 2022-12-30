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
            currentItems.Add(item);
            NotifyAdd(item);
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
}
