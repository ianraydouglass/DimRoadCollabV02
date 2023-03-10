using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GamePart", menuName = "Item System/Game Part")]
public class GamePart : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string displayName;

    [SerializeField]
    [Range(0, 1000)]
    private int bulkValue = 1;
    private bool removalFlag = false;
    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth = 10;
    [SerializeField]
    private List<PartTrait> partTraits = new List<PartTrait>();
    [SerializeField]
    private bool occupied = false;


    public string GetName()
    {
        if (displayName == "")
        {
            return ToString();
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
        return bulkValue;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public List<PartTrait> GetTraits()
    {
        return partTraits;
    }
    public bool HasTrait(PartTrait trait)
    {
        bool hasTrait = false;
        Debug.Log(ToString() + " is being searched for the trait: " + trait.GetName());
        foreach (PartTrait thisTrait in partTraits)
        {
            if (thisTrait == trait)
            {
                hasTrait = true;
            }
        }
        return hasTrait;
    }
        
    public string TellTraits()
    {
        string traitList = "";
        foreach (PartTrait thisTrait in partTraits)
        {
            traitList = traitList + thisTrait.GetName() + " ";
        }
        return traitList;
    }
    public void DamageThis(int damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            removalFlag = true;
        }
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetLock(bool lockState)
    {
        occupied = lockState;
    }

    public bool GetRemovalFlag()
    {
        return removalFlag;
    }

    public void UseInCraft()
    {
        removalFlag = true;
        occupied = false;
    }

    public string TellDescription()
    {
        string t = "Traits: " + TellTraits() + "\n";
        t += "Health: " + currentHealth.ToString() + " / " + maxHealth.ToString();
        return t;

    }
}
