using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public CraftMenuManager craftMenu;
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
    public void StationActive()
    {
        craftMenu.OpenCraftingMenu(stationRecipes);
    }

}
