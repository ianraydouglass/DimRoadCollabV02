using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDirector : MonoBehaviour
{

    public InventoryManager inventoryManager;

    public bool CheckCapacity(int bulk)
    {
        //we can implement this later, checking if the player can carry the item we are colliding with
        return true;
    }

    public void CollectItem(GameItem item)
    {
        inventoryManager.AddToInventory(item);
    }
}
