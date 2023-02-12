using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public string stationDisplayName = "Basic Crafting Station";
    
    public CraftMenuManager2 craftMenu2;
    public InventoryManager inventory;
    public ToolUseManager toolManager;
    public List<CraftingRecipe> stationRecipes = new List<CraftingRecipe>();
    // Start is called before the first frame update
    void Start()
    {
        if (!craftMenu2)
        {
            GameObject holder = GameObject.Find("Inventory Holder Temporary");
            craftMenu2 = holder.GetComponent<CraftMenuManager2>();
        }
        if(!inventory)
        {
            GameObject holder = GameObject.Find("Inventory Holder Temporary");
            inventory = holder.GetComponent<InventoryManager>();
        }
        if(!toolManager)
        {
            GameObject player = GameObject.Find("PlayerCapsule");
            toolManager = player.GetComponent<ToolUseManager>();
        }
        foreach(CraftingRecipe thisRecipe in stationRecipes)
        {
            thisRecipe.ConfigureRecipe();
        }
    }

    //duplication for compatiblity
    public void StationActive()
    {
        
        if (craftMenu2)
        {
            craftMenu2.OpenCraftingMenu(stationRecipes, stationDisplayName);
        }
    }

}
