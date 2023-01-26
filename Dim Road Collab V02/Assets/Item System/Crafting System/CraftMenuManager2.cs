using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftMenuManager2 : MonoBehaviour
{
    //reference to the text at the top of the crafting menu interface
    public TextMeshProUGUI stationNameText;

    public GameEvent openMenu;
    //actual UI panel in-question
    public GameObject craftMenuPanel;
    public InventoryManager inventoryManager;
    public List<CraftingRecipe> recipeCatalog = new List<CraftingRecipe>();
    public int currentRecipeIndex = 0;
    public CraftingRecipe currentRecipe;
    public RecipeHolder recipeHolder;
    public GameObject inventoryPanel;
    public List<TextMeshProUGUI> rowHeaders = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> rowBodies = new List<TextMeshProUGUI>();
    public List<GameObject> partSlotButtons = new List<GameObject>();
    public GameObject partSelector;
    public List<GamePart> currentParts = new List<GamePart>();
    public GameObject confirmButton;

    public void OpenCraftingMenu(List<CraftingRecipe> incomingRecipes, string stationName)
    {
        openMenu.Raise();
        craftMenuPanel.SetActive(true);
        stationNameText.text = stationName;
        recipeCatalog = incomingRecipes;
        currentRecipeIndex = 0;
        PrepRecipe(0);
        PrepRecipeHolder();

    }

    public void PrepRecipeHolder()
    {
        if (recipeCatalog.Count == 1)
        {
            recipeHolder.NoButtons();
        }
        else
        {
            recipeHolder.DefaultSetup();
        }
    }

    public void NavigateRecipeRight()
    {
        ReleaseAllParts();
        if (currentRecipeIndex >= RecipeMaxIndex())
        {
            //can't go further right if we are at the end
            //should only be called by mistake
            return;
        }
        int newIndex = currentRecipeIndex += 1;
        //give instructions to the holder to enable or disable nav buttons
        if (newIndex >= RecipeMaxIndex())
        {
            recipeHolder.MaxRight();
        }
        else
        {
            recipeHolder.MidRange();
        }
        //reset interface for new recipe
        PrepRecipe(newIndex);
    }

    public void NavigateRecipeLeft()
    {
        ReleaseAllParts();

        if (currentRecipeIndex <= 0)
        {
            //can't go further left
            //should only be called by mistake
            return;
        }
        int newIndex = currentRecipeIndex -= 1;
        //give instructions to the holder to enable or disable nav buttons
        if (newIndex <= 0)
        {
            recipeHolder.MaxLeft();
        }
        else
        {
            recipeHolder.MidRange();
        }
        //reset interface for new recipe
        PrepRecipe(newIndex);
    }

    //the interface you click that causes you to snap to a recipe should be indexed
    public void NavigateToRecipe()
    {
        ReleaseAllParts();

    }

    public void PrepRecipe(int i)
    {
        currentParts.Clear();
        currentRecipe = recipeCatalog[i];
        int req = currentRecipe.GetTraitCount();
        currentParts = new List<GamePart>(req);
        currentRecipeIndex = i;
        //set up the recipe navigation to show the current recipe output item image and name
        GameItem currentItem = currentRecipe.GetOutputItem();
        recipeHolder.itemImage.sprite = currentItem.GetSprite();
        recipeHolder.titleText.text = currentItem.GetName();
        SetRowHeaders();
        SetRowBodies();
        SetSlotButtons();
        CheckConditions();
    }

    public void SetRowHeaders()
    {
        
        for (int i = rowHeaders.Count - 1; i >= 0; i--)
        {
            int rowMaxIndex = currentRecipe.GetTraitCount() - 1;
            if (i > rowMaxIndex)
            {
                rowHeaders[i].text = "";
            }
            else
            {
                string headText = "Needs part with trait: ";
                headText += currentRecipe.GetTraitByIndex(i).GetName();
                rowHeaders[i].text = headText;
            }
        }
    }

    public void SetRowBodies()
    {
        for (int i = rowBodies.Count - 1; i >= 0; i--)
        {
            int rowMaxIndex = currentRecipe.GetTraitCount() - 1;
            if (i > rowMaxIndex)
            {
                rowBodies[i].text = "";
            }
            else
            {
                
                rowBodies[i].text = currentRecipe.GetPurposeByIndex(i);
            }
        }
    }

    public void SetSlotButtons()
    {
        int rowMaxIndex = currentRecipe.GetTraitCount() - 1;
        for (int i = partSlotButtons.Count -1; i>=0; i--)
        {
            if (i > rowMaxIndex)
            {
                partSlotButtons[i].SetActive(false);
            }
            else
            {
                partSlotButtons[i].SetActive(true);
                PartSlotButtonHandler2 buttonHandler = partSlotButtons[i].GetComponent<PartSlotButtonHandler2>();
                buttonHandler.slotTrait = currentRecipe.GetTraitByIndex(i);
                buttonHandler.listIndex = i;
                buttonHandler.slottedPart.SetActive(false);
            }
        }
    }

    public void SetPartSubMenu(GameObject slot, PartTrait trait, int listIndex)
    {
        if ((currentParts.Count -1) >= listIndex)
        {
            currentParts[listIndex] = null;
        }
        inventoryPanel.SetActive(true);
        foreach (Transform t in inventoryPanel.transform) 
        {
            GameObject.Destroy(t.gameObject);
        }
        List<GamePart> partsFound = new List<GamePart>();
        List<GameItem> validItems = inventoryManager.GetItemsWithPartTrait(trait);
        if (validItems.Count > 0)
        {
            foreach (GameItem item in validItems)
            {
                List<GamePart> validParts = item.GetPartsWithTrait(trait);
                //partsFound = validParts;
                foreach (GamePart part in validParts)
                {
                    partsFound.Add(part);
                }

            }
        }
        if (partsFound.Count > 0)
        {
            foreach (GamePart part in partsFound)
            {
                GameObject partButton = Instantiate(partSelector, inventoryPanel.transform);
                PartSubMenuButton b = partButton.GetComponent<PartSubMenuButton>();
                b.partImage.sprite = part.GetSprite();
                b.part = part;
                b.craftMenu = this;
                b.partHome = slot;
                b.listIndex = listIndex;
                b.CheckColor();

                //instantiate the button prefab with icon
            }
        }
        CheckConditions();
    }

    public void AttachPartToSlot(GameObject slot, GamePart part, int listIndex)
    {
        inventoryPanel.SetActive(false);
        //set the slotted part image to appear with the right sprite
        PartSlotButtonHandler2 slotHandler = slot.GetComponent<PartSlotButtonHandler2>();
        slotHandler.slotPart = part;
        slotHandler.slottedPart.SetActive(true);
        SlottedPart partImage = slotHandler.slottedPart.GetComponent<SlottedPart>();
        partImage.partImage.sprite = part.GetSprite();
        currentParts[listIndex] = part;
        CheckConditions();

    }

    public void ReleasePart(GamePart part)
    {
        part.SetLock(false);
    }

    public void ReleaseAllParts()
    {
        currentParts.Clear();
        foreach (GameObject partSlot in partSlotButtons)
        {
            PartSlotButtonHandler2 slotHandler = partSlot.GetComponent<PartSlotButtonHandler2>();
            if (slotHandler.slotPart)
            {
                slotHandler.slotPart.SetLock(false);
            }
        }
        CheckConditions();
    }

    public void CheckConditions()
    {
        bool conditionsMet = true;
        int req = currentRecipe.GetTraitCount();
        int sup = currentParts.Count;
        if (currentParts.Count == 0)
        {
            conditionsMet = false;
            confirmButton.SetActive(conditionsMet);
            return;
        }

        if (req != sup)
        {
            conditionsMet = false;
            confirmButton.SetActive(conditionsMet);
            return;

        }
        
        for (int i = req - 1; i >= 0; i--)
        {
            PartTrait reqTrait = currentRecipe.GetTraitByIndex(i);
            if(currentParts[i].HasTrait(reqTrait))
            {

            }
            else
            {
                conditionsMet = false;
            }

        }
        confirmButton.SetActive(conditionsMet);
    }

    public int RecipeMaxIndex()
    {
        int indexValue = (recipeCatalog.Count - 1);
        return indexValue;
    }
}
