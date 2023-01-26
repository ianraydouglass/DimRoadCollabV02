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
    public List<CraftingRecipe> recipeCatalog = new List<CraftingRecipe>();
    public int currentRecipeIndex = 0;
    public CraftingRecipe currentRecipe;
    public RecipeHolder recipeHolder;
    public List<TextMeshProUGUI> rowHeaders = new List<TextMeshProUGUI>();

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

    }

    public void PrepRecipe(int i)
    {
        currentRecipe = recipeCatalog[i];
        currentRecipeIndex = i;
        //set up the recipe navigation to show the current recipe output item image and name
        GameItem currentItem = currentRecipe.GetOutputItem();
        recipeHolder.itemImage.sprite = currentItem.GetSprite();
        recipeHolder.titleText.text = currentItem.GetName();
        SetRowHeaders();
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
                rowHeaders[i].text = currentRecipe.GetPurposeByIndex(i);
            }
        }
    }

    public int RecipeMaxIndex()
    {
        int indexValue = (recipeCatalog.Count - 1);
        return indexValue;
    }
}
