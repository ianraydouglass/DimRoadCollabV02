using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OutputType { Item, Tool }
[CreateAssetMenu(fileName = "New CraftingRecipe", menuName = "Crafting and Repairs/Crafting Recipe")]

public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    private string displayName = "New Crafting Recipe";
    [SerializeField]
    private OutputType outputType = OutputType.Item;
    [SerializeField]
    private GameObject outputObject;
    [Header("If outut type is item")]
    [SerializeField]
    private GameItem outputItem;
    [Header("Limit to 5 slots")]
    [SerializeField]
    private List<TraitSlot> requiredTraits;

    public void ConfigureRecipe()
    {
        //this should remove every entry in the list beyond the 5th one, needs to test
        if (requiredTraits.Count > 5)
        {
            for (int i = requiredTraits.Count - 1; i >= 5; i--)
            {
                requiredTraits.RemoveAt(i);
            }
            Debug.Log("Requirements too long on " + displayName + " so it was shortened to " + GetTraits());
        }
    }

    public string GetName()
    {
        return displayName;
    }

    public GameObject GetOutput()
    {
        return outputObject;
    }

    public string GetTraits()
    {
        string traitList = "";
        foreach (TraitSlot thisTrait in requiredTraits)
        {
            traitList = traitList + thisTrait.requiredTrait.GetName() + " ";
        }
        return traitList;
    }
    public int GetTraitCount()
    {
        return requiredTraits.Count;
    }
    public PartTrait GetTraitByIndex(int traitIndex)
    {
        return requiredTraits[traitIndex].requiredTrait;
    }

    
}
