using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GamePart", menuName = "Item System/Game Part")]
public class GamePart : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    [Range(0, 1000)]
    private int bulkValue = 1;
    private bool removalFlag = false;
    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth = 10;
    [SerializeField]
    private List<PartTrait> partTraits = new List<PartTrait>();


    public string GetName()
    {
        return ToString();
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

}
