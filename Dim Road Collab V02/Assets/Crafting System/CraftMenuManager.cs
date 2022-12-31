using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftMenuManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject craftMenuPanel;
    public GameObject itemPartPrefab;
    public GameObject itemCard;
    public ItemCardHolder cardHolder;
    public CraftingRecipe currentRecipe;
    public int rowCount;
    public GameObject partSlot1;
    public GameObject partSlot2;
    public GameObject partSlot3;
    public GameObject partSlot4;
    public GameObject partSlot5;
    public GameObject partRow1;
    public GameObject partRow2;
    public GameObject partRow3;
    public GameObject partRow4;
    public GameObject partRow5;
    public GameObject currentPartRow;
    public int currentRowNumber;
    public PartTrait rowTrait;
    public List<GameItem> itemsFound;
    public List<GamePart> partsFound;
    public List<GameObject> allPartRows = new List<GameObject>();
    public List<CraftingRecipe> recipeCatalog = new List<CraftingRecipe>();
    void Start()
    {
        allPartRows.Add(partRow1);
        allPartRows.Add(partRow2);
        allPartRows.Add(partRow3);
        allPartRows.Add(partRow4);
        allPartRows.Add(partRow5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCraftingMenu(List<CraftingRecipe> incomingRecipes)
    {
        craftMenuPanel.SetActive(true);
        recipeCatalog = incomingRecipes;
        PrepRecipe(recipeCatalog[0]);
        ShiftFromRow(1);
        ShiftToRow(1);
    }

    public void PrepRecipe(CraftingRecipe recipe)
    {
        currentRecipe = recipe;
        rowCount = recipe.GetTraitCount();
        partSlot1.SetActive(true);
        partSlot2.SetActive(true);
        partSlot3.SetActive(true);
        partSlot4.SetActive(true);
        partSlot5.SetActive(true);
        partRow1.SetActive(true);
        partRow2.SetActive(true);
        partRow3.SetActive(true);
        partRow4.SetActive(true);
        partRow5.SetActive(true);
        currentPartRow = partRow1;
        if (rowCount <=4)
        {
            partSlot5.SetActive(false);
            partRow5.SetActive(false);
        }
        if (rowCount <=3)
        {
            partSlot4.SetActive(false);
            partRow4.SetActive(false);
        }
        if (rowCount <=2)
        {
            partSlot3.SetActive(false);
            partRow3.SetActive(false);
        }
        if (rowCount <=1)
        {
            partSlot2.SetActive(false);
            partRow2.SetActive(false);
        }
    }

    public void ShiftFromRow(int currentRow)
    {
        foreach (Transform child in currentPartRow.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ShiftToRow(int nextRow)
    {
        if (nextRow >= rowCount)
        {
            currentRowNumber = rowCount + 1;
        }
        else
        {
            int rowIndex = nextRow - 1;
            currentPartRow = allPartRows[rowIndex];
            rowTrait = currentRecipe.GetTraitByIndex(rowIndex);
            AddEndCap(currentPartRow);
            List<GameItem> validItems = inventoryManager.GetItemsWithPartTrait(rowTrait);
            itemsFound = validItems;
            if (validItems.Count > 0)
            {
                foreach (GameItem item in validItems)
                {
                    List<GamePart> validParts = item.GetPartsWithTrait(rowTrait);
                    partsFound = validParts;
                    foreach (GamePart part in validParts)
                    {
                        AddPart(currentPartRow, item, part);
                    }

                }
            }
        }
    }

    public void AddEndCap(GameObject partRow)
    {
        GameObject p = Instantiate(itemPartPrefab, partRow.transform);
        p.GetComponent<Image>().enabled = false;
        //p.transform.SetParent(partRow.transform);
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.isEndCap = true;
    }

    public void AddPart(GameObject partRow, GameItem hostItem, GamePart thisPart)
    {
        GameObject p = Instantiate(itemPartPrefab, partRow.transform);
        //p.transform.SetParent(partRow.transform);
        //p.transform.parent = partRow.transform;
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.hostItem = hostItem;
        ip.thisPart = thisPart;
        ip.image.sprite = thisPart.GetSprite();
        ip.itemCard = itemCard;
        ip.cardHolder = cardHolder;
    }
}
