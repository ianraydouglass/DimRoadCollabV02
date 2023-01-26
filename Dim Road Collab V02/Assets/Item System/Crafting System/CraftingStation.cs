using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public string stationDisplayName = "Basic Crafting Station";
    public CraftMenuManager craftMenu;
    public CraftMenuManager2 craftMenu2;
    public InventoryManager inventory;
    public List<CraftingRecipe> stationRecipes = new List<CraftingRecipe>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(CraftingRecipe thisRecipe in stationRecipes)
        {
            thisRecipe.ConfigureRecipe();
        }
    }

    //duplication for compatiblity
    public void StationActive()
    {
        if (craftMenu)
        {
            craftMenu.OpenCraftingMenu(stationRecipes);
        }
        if (craftMenu2)
        {
            craftMenu2.OpenCraftingMenu(stationRecipes, stationDisplayName);
        }
    }

}
