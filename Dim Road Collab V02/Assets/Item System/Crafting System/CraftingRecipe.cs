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
    [Header("If outut type is tool")]
    [SerializeField]
    private ToolItem outputTool;
    [Header("Limit to 5 slots")]
    [SerializeField]
    private List<PartPurposeSlot> requiredTraits;

    public void ConfigureRecipe()
    {
        if (requiredTraits.Count == 0 && outputType == OutputType.Item)
        {
            requiredTraits = outputItem.GetContents();
        }
        if (requiredTraits.Count == 0 && outputType == OutputType.Tool)
        {
            requiredTraits = outputTool.GetContents();
        }
        //this should remove every entry in the list beyond the 5th one, needs to test
        if (requiredTraits.Count > 5)
        {
            for (int i = requiredTraits.Count - 1; i >= 5; i--)
            {
                requiredTraits.RemoveAt(i);
            }
            Debug.Log("Requirements too long on " + displayName + " so it was shortened to " + GetTraits());
        }
        CheckPurposeText();
    }

    public void CheckPurposeText()
    {
        foreach (PartPurposeSlot thisPart in requiredTraits)
        {
            thisPart.SetUpPurpose();
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

    public OutputType GetOutputType()
    {
        return outputType;
    }

    public string GetTraits()
    {
        string traitList = "";
        foreach (PartPurposeSlot thisTrait in requiredTraits)
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
    public string GetPurposeByIndex(int purposeIndex)
    {
        return requiredTraits[purposeIndex].purpose;
    }
    public GameItem GetOutputItem()
    {
        return outputItem;
    }
    public ToolItem GetOutputTool()
    {
        return outputTool;
    }
    public string GetDescription()
    {
        if(outputItem)
        {
            return outputItem.GetDescription();
        }
        if(outputTool)
        {
            return outputTool.GetDescription();
        }
        return "no description available";
    }


}
