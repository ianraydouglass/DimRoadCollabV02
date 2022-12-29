using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameItem> currentItems = new List<GameItem>();
    public GameObject itemHolder;

    public void AddToInventory(GameItem item)
    {
        if (item)
        {
            currentItems.Add(item);
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
}
