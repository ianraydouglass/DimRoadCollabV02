using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Equipment System/Tool Item")]
public class ToolItem : ScriptableObject
{
    [SerializeField]
    private string displayName = "Misc. Tool";
    [SerializeField]
    private ToolType type;
    [SerializeField]
    private Sprite sprite;
    [Range(0, 1000)]
    private int bulkValue = 1;
    [SerializeField]
    private int useTime = 3;
    [SerializeField]
    private string displayDescription = "Unknown Use";

    private List<GamePart> currentParts = new List<GamePart>();
    [SerializeField]
    private List<PartTrait> toolTraits = new List<PartTrait>();
    [SerializeField]
    private List<PartPurposeSlot> contents = new List<PartPurposeSlot>();

    private bool brokenItem = false;
    private bool removalFlag = false;
    private int maxHealth;
    private int currentHealth;

    public string GetName()
    {
        if (brokenItem)
        {
            return ("Broken " + displayName);
        }
        else
        {
            return displayName;
        }

    }
    public Sprite GetSprite()
    {
        return sprite;
    }

    public ToolType GetToolType()
    {
        return type;
    }

    public int GetUseTime()
    {
        return useTime;
    }
    public List<PartPurposeSlot> GetContents()
    {
        return contents;
    }
    public string GetDescription()
    {
        return displayDescription;
    }

    public void CraftFromParts(List<GamePart> partsToAdd)
    {
        List<PartPurposeSlot> mysteryContents = new List<PartPurposeSlot>();
        foreach (GamePart incomingPart in partsToAdd)
        {
            PartPurposeSlot mysterySlot = new PartPurposeSlot();
            mysterySlot.defaultPart = incomingPart;
            mysteryContents.Add(mysterySlot);
        }
        contents = mysteryContents;
        SetUpFromContents();
        currentParts = partsToAdd;
        ReBuildItem();
    }

    public void SetUpFromContents()
    {
        int bulkCount = 0;
        int healthCount = 0;
        currentParts.Clear();
        foreach (PartPurposeSlot thisPart in contents)
        {
            thisPart.SetForCreation();
            GamePart defaultPart = thisPart.defaultPart;
            GamePart partCopy = Instantiate(defaultPart) as GamePart;
            currentParts.Add(partCopy);
            thisPart.SetPart(partCopy);
            bulkCount += defaultPart.GetBulk();
            healthCount += defaultPart.GetMaxHealth();
        }
        if (bulkCount > 1000)
        {
            bulkCount = 1000;
        }
        bulkValue = bulkCount;
        maxHealth = healthCount;
        currentHealth = healthCount;
    }

    public void ReBuildItem()
    {
        int bulkCount = 0;
        int healthCount = 0;
        int currentHealthCount = 0;
        List<GamePart> partsToRemove = new List<GamePart>();

        if (currentParts.Count == 0)
        {
            removalFlag = true;
        }
        else
        {
            foreach (GamePart thisPart in currentParts)
            {
                if (thisPart.GetCurrentHealth() <= 0 || thisPart.GetRemovalFlag())
                {
                    partsToRemove.Add(thisPart);
                }
                else
                {
                    bulkCount += thisPart.GetBulk();
                    currentHealthCount += thisPart.GetCurrentHealth();
                }
            }
            if (bulkCount > 1000)
            {
                bulkCount = 1000;
            }
        }
        //making sure max health is based on contents
        if (contents.Count > 0)
        {
            foreach (PartPurposeSlot thisPart in contents)
            {
                healthCount += thisPart.GetPart().GetMaxHealth();
            }
        }
        else
        {
            foreach (GamePart thisPart in currentParts)
            {
                healthCount += thisPart.GetMaxHealth();
            }
        }
        bulkValue = bulkCount;
        maxHealth = healthCount;
        currentHealth = currentHealthCount;
        if (partsToRemove.Count > 0)
        {
            RemoveParts(partsToRemove);
        }
        else
        {
            //refresh complete
        }
    }

    public void RemoveParts(List<GamePart> partsToRemove)
    {
        brokenItem = true;
        foreach (GamePart thisPart in partsToRemove)
        {
            currentParts.Remove(thisPart);
        }
        if (currentParts.Count == 0)
        {
            removalFlag = true;
        }
        ReBuildItem();
        //refresh complete
        //run cleanup on item manager

    }
}
