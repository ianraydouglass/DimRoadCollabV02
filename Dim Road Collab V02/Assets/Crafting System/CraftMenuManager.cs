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
    public List<GameObject> currentRowItemParts = new List<GameObject>();
    public List<GameObject> currentRowCells = new List<GameObject>();
    public PartTrait rowTrait;
    public List<GameItem> itemsFound;
    public List<GamePart> partsFound;
    public List<GameObject> allPartRows = new List<GameObject>();
    public List<CraftingRecipe> recipeCatalog = new List<CraftingRecipe>();
    public GameEvent openMenu;
    public List<AcceptedPartHolder> acceptedParts;
    public GameEvent conditionsMet;
    public GameEvent conditionsNotMet;

    //zero represents the end-cap
    private int row1Position = 0;
    private int row2Position = 0;
    private int row3Position = 0;
    private int row4Position = 0;
    private int row5Position = 0;
    private GameObject currentItem;
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
        openMenu.Raise();
        craftMenuPanel.SetActive(true);
        recipeCatalog = incomingRecipes;
        PrepRecipe(recipeCatalog[0]);
        ShiftFromRow();
        ShiftToRow(1);
    }

    public void PrepRecipe(CraftingRecipe recipe)
    {
        currentItem = null;
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
        row1Position = 0;
        row2Position = 0;
        row3Position = 0;
        row4Position = 0;
        row5Position = 0;
        currentPartRow = partRow1;
        if (rowCount <= 4)
        {
            partSlot5.SetActive(false);
            partRow5.SetActive(false);
        }
        if (rowCount <= 3)
        {
            partSlot4.SetActive(false);
            partRow4.SetActive(false);
        }
        if (rowCount <= 2)
        {
            partSlot3.SetActive(false);
            partRow3.SetActive(false);
        }
        if (rowCount <= 1)
        {
            partSlot2.SetActive(false);
            partRow2.SetActive(false);
        }
        RefreshButtons();
    }

    //this clears the contents of the row
    public void ShiftFromRow()
    {
        currentRowItemParts.Clear();
        currentRowCells.Clear();
        currentItem = null;
        foreach (Transform child in currentPartRow.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (AcceptedPartHolder acceptedPart in acceptedParts)
        {
            acceptedPart.TogglePartVisibility(true);
        }
    }
    //this populates the next row with your options
    public void ShiftToRow(int nextRow)
    {
        if (nextRow > rowCount)
        {
            currentRowNumber = rowCount + 1;
        }
        else
        {
            if (nextRow < 1)
            {
                nextRow = 1;
                currentRowNumber = 1;
            }
            currentRowNumber = nextRow;
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
            RefreshDisplayedItem(currentPartRow);
            acceptedParts[rowIndex].TogglePartVisibility(false);
        }
    }

    public void ShiftRowRight()
    {
        //don't shift if there is only one icon, or if it's the confirm or cancel button
        if (currentPartRow.transform.childCount <= 1)
        {
            return;
        }
        if (CheckRowPosition(currentPartRow) <= 0)
        {
            return;
        }
        RectTransform thisTransform = currentPartRow.GetComponent<RectTransform>();
        float posX = thisTransform.anchoredPosition.x;
        float posY = thisTransform.anchoredPosition.y;
        thisTransform.anchoredPosition = new Vector2((posX + 37.5f), posY);
        RefreshRowPosition(currentPartRow, -1);
        RefreshDisplayedItem(currentPartRow);
    }

    public void ShiftRowLeft()
    {
        //don't shift if there is only one icon, or if it's the confirm or cancel button
        if (currentPartRow.transform.childCount <= 1)
        {
            return;
        }
        if (CheckRowPosition(currentPartRow) >= (currentRowItemParts.Count))
        {
            return;
        }
        RectTransform thisTransform = currentPartRow.GetComponent<RectTransform>();
        float posX = thisTransform.anchoredPosition.x;
        float posY = thisTransform.anchoredPosition.y;
        thisTransform.anchoredPosition = new Vector2((posX - 37.5f), posY);
        RefreshRowPosition(currentPartRow, 1);
        RefreshDisplayedItem(currentPartRow);
    }

    public void RefreshRowPosition(GameObject partRow, int moveValue)
    {
        if (partRow == partRow1)
        {
            row1Position = row1Position + moveValue;
        }
        if (partRow == partRow2)
        {
            row2Position = row2Position + moveValue;
        }
        if (partRow == partRow3)
        {
            row3Position = row3Position + moveValue;
        }
        if (partRow == partRow4)
        {
            row4Position = row4Position + moveValue;
        }
        if (partRow == partRow5)
        {
            row5Position = row5Position + moveValue;
        }

    }

    public void RefreshDisplayedItem(GameObject partRow)
    {
        int currentPosition = CheckRowPosition(partRow);
        currentItem = currentRowCells[currentPosition];
        if (!currentItem)
        {
            Debug.Log("No current item found after row move");
            return;
        }
        ItemPartHolder itemHolder = currentItem.GetComponent<ItemPartHolder>();
        if (!itemHolder)
        {
            Debug.Log("No part holder found on item after move");
            return;
        }
        itemHolder.ViewItemCard();
    }
    public void AcceptCurrentPart()
    {
        if(currentItem)
        {
            ItemPartHolder itemHolder = currentItem.GetComponent<ItemPartHolder>();
            if(itemHolder.isEndCap)
            {
                UnacceptPartInRow();
                
            }
            else
            {
                UnacceptPartInRow();
                AcceptThisPart();
            }
        }
        RefreshButtons();
    }
    public void UnacceptPartInRow()
    {
        
        //unaccept whatever part is in the holder of that row
        AcceptedPartHolder destinationHolder = acceptedParts[0];
        foreach (AcceptedPartHolder acceptedPartHolder in acceptedParts)
        {
            if (acceptedPartHolder.GetRowValue() == currentRowNumber)
            {
                destinationHolder = acceptedPartHolder;
            }
        }
        destinationHolder.UnacceptPart();

        //unaccept all parts in row and in slot if there are any
        currentPartRow.BroadcastMessage("UnAcceptPart");
        /*
        foreach (Transform child in currentPartRow.transform)
        {
            GameObject tempObject = (child.gameObject);
            ItemPartHolder tempHolder
        }
        */
    }
    public void AcceptThisPart()
    {
        if (acceptedParts.Count == 0)
        {
            Debug.Log("No accepted part holders on list");
            return;
        }
        //accept the current item where it is
        ItemPartHolder itemHolder = currentItem.GetComponent<ItemPartHolder>();
        itemHolder.AcceptPart();

        //figure out where to display the accepted item
        int currentRowIndex = currentRowNumber - 1;
        AcceptedPartHolder destinationHolder = acceptedParts[currentRowIndex];
        GameObject acceptedPartDestination = destinationHolder.gameObject;
        
        //instantiate a copy of the holder at the destination
        
        GameObject p = Instantiate(itemPartPrefab, acceptedPartDestination.transform);
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.hostItem = itemHolder.hostItem;
        ip.thisPart = itemHolder.thisPart;
        ip.image.sprite = itemHolder.thisPart.GetSprite();
        ip.itemCard = itemCard;
        ip.cardHolder = cardHolder;
        ip.AcceptPart();
        destinationHolder.SetPartAccept(p);

        //it should hide it's version of the part as soon as its set

    }

    public bool PartsSatisfyRecipe()
    {
        //rowCount should always be the number of traits in the current recipe
        //int traitsToSatisfy = currentRecipe.GetTraitCount();
        //this loop should run a number of times equal to the row count
        for (int i = 0; i < rowCount; i++)
        {
            PartTrait thisTrait = currentRecipe.GetTraitByIndex(i);
            GamePart acceptedPart = acceptedParts[i].acceptedPart;
            if (!acceptedPart)
            {
                return false;
            }
            if (!acceptedPart.HasTrait(thisTrait))
            {
                return false;
            }
        }
        return true;
    }

    public void RefreshButtons()
    {
        if (PartsSatisfyRecipe())
        {
            conditionsMet.Raise();
        }
        else 
        {
            conditionsNotMet.Raise();                    
        }
    }

    public int CheckRowPosition(GameObject partRow)
    {
        if (partRow == partRow1)
        {
            return row1Position;
        }
        if (partRow == partRow2)
        {
            return row2Position;
        }
        if (partRow == partRow3)
        {
            return row3Position;
        }
        if (partRow == partRow4)
        {
            return row4Position;
        }
        if (partRow == partRow5)
        {
            return row5Position;
        }
        return 6;
    }

    public void AddEndCap(GameObject partRow)
    {
        GameObject p = Instantiate(itemPartPrefab, partRow.transform);
        currentRowCells.Add(p);
        p.GetComponent<Image>().enabled = false;
        //p.transform.SetParent(partRow.transform);
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.cardHolder = cardHolder;
        ip.isEndCap = true;
    }

    public void AddPart(GameObject partRow, GameItem hostItem, GamePart thisPart)
    {
        int currentRowIndex = currentRowNumber - 1;
        GamePart acceptedRowPart = acceptedParts[currentRowIndex].acceptedPart;
        GameObject p = Instantiate(itemPartPrefab, partRow.transform);
        currentRowItemParts.Add(p);
        currentRowCells.Add(p);
        //p.transform.SetParent(partRow.transform);
        //p.transform.parent = partRow.transform;
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.hostItem = hostItem;
        ip.thisPart = thisPart;
        ip.image.sprite = thisPart.GetSprite();
        ip.itemCard = itemCard;
        ip.cardHolder = cardHolder;
        if (acceptedRowPart)
        {
            if (acceptedRowPart == thisPart)
                ip.isAccepted = true;
        }
        ip.PartColorCheck();
    }
}
