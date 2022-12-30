using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartingPartGiver : MonoBehaviour
{
    [SerializeField]
    private List<GamePart> partsToGive = new List<GamePart>();
    [SerializeField]
    private List<GameItem> itemsToGive = new List<GameItem>();
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private GameItem scrapItem;

    // Start is called before the first frame update
    public void GiveItems()
    {
        if (inventoryManager)
        {
            if (partsToGive.Count > 0)
            {
                //this didnt' work as intended
                /*
                foreach (GamePart thisPart in partsToGive)
                {
                    GameItem shapedItem = scrapItem;
                    List<GamePart> thisItemParts = new List<GamePart>();
                    thisItemParts.Add(thisPart);
                    shapedItem.CraftFromParts(thisItemParts);
                    itemsToGive.Add(shapedItem);
                }
                */
                scrapItem.CraftFromParts(partsToGive);
                itemsToGive.Add(scrapItem);
            }
            if (itemsToGive.Count > 0)
            {
                foreach (GameItem thisItem in itemsToGive)
                {
                    inventoryManager.AddToInventory(thisItem);
                }
            }
        }
    }
}
