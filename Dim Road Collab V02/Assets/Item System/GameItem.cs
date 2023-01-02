using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameItem", menuName = "Item System/Game Item")]
public class GameItem : ScriptableObject
{
    [SerializeField]
    private string displayName = "Misc. Item";
    [SerializeField]
    private Sprite sprite;
    [Range(0, 1000)]
    private int bulkValue = 1;

    [SerializeField]
    private List<GamePart> defaultParts = new List<GamePart>();
    private List<GamePart> currentParts = new List<GamePart>();

    [SerializeField]
    private List<PartTrait> itemTraits = new List<PartTrait>();

    private bool brokenItem = false;
    private bool removalFlag = false;
    private int maxHealth;
    private int currentHealth;

    public void SetupItem()
    {
        
        int bulkCount = 0;
        int healthCount = 0;
        currentParts.Clear();
        foreach (GamePart thisPart in defaultParts)
        {
            GamePart partCopy = Instantiate(thisPart) as GamePart;
            currentParts.Add(partCopy);
            bulkCount += thisPart.GetBulk();
            healthCount += thisPart.GetMaxHealth();
        }
        if (bulkCount > 1000)
        {
            bulkCount = 1000;
        }
        bulkValue = bulkCount;
        maxHealth = healthCount;
        currentHealth = healthCount;

    }

    public void CraftFromParts(List<GamePart> partsToAdd)
    {
        
        currentParts = partsToAdd;
        RefreshItem();
    }

    public void RefreshItem()
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
                if (thisPart.GetCurrentHealth() <= 0)
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
        foreach (GamePart thisPart in defaultParts)
        {
            healthCount += thisPart.GetMaxHealth();
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

    //only call this if there are parts actually being removed
    //it breaks the item traits by design
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
        //refresh complete
        //run cleanup on item manager

    }

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
    public int GetBulk()
    {
        RefreshItem();
        return bulkValue;
    }
    public bool GetRemovalFlag()
    {
        return removalFlag;
    }

    //items lose their item-level traits if they are missing any parts
    public List<PartTrait> GetTraits()
    {
        if (brokenItem)
        {
            return new List<PartTrait>();
        }
        else
        {
            return itemTraits;
        }
    }
    public string TellTraits()
    {
        if (brokenItem)
        {
            return displayName + " has no traits";
        }
        else
        {
            string traitList = "";
            foreach (PartTrait thisTrait in itemTraits)
            {
                traitList = traitList + thisTrait.GetName() + " ";
            }
            return traitList;
        }
        
    }

    public List<GamePart> GetCurrentParts()
    {
        return currentParts;
    }
    public string TellParts()
    {

        if (currentParts.Count > 0)
        {
            string partList = "";
            foreach (GamePart thisPart in currentParts)
            {
                partList = partList + thisPart.GetName() + " ";
            }
            return partList;
        }
        else
        {
            return displayName + " has no parts";
        }
    }
    public string TellHealth()
    {
        string healthText = currentHealth.ToString() + " / " + maxHealth.ToString();
        return healthText;
    }

    public string TellDescription()
    {
        string description = "Item Traits: " + TellTraits() + "\nParts: " + TellParts() + "\nItem Health: " + TellHealth();
        return description;
    }

    //when an item recieves damage, that's just a conduit for applying that damage to all of it's parts.
    //mainly Parts are the things that break
    public void DamageThis(int damageValue)
    {
        if (currentParts.Count > 0)
        {
            foreach (GamePart thisPart in currentParts)
            {
                thisPart.DamageThis(damageValue);
            }
            RefreshItem();
        }
    }

    public bool HasPartWithTrait(PartTrait trait)
    {
        Debug.Log(displayName + " is being searched for parts with the trait: " + trait.GetName());
        bool hasPartWithTrait = false;
        foreach (GamePart part in currentParts)
        {
            if(part.HasTrait(trait))
            {
                hasPartWithTrait = true;
            }
        }
        return hasPartWithTrait;
    }

    public List<GamePart> GetPartsWithTrait(PartTrait trait)
    {
        List<GamePart> getPartsWithTrait = new List<GamePart>();
        foreach (GamePart part in currentParts)
        {
            if (part.HasTrait(trait))
            {
                getPartsWithTrait.Add(part);
            }
        }
        return getPartsWithTrait;
    }

}
