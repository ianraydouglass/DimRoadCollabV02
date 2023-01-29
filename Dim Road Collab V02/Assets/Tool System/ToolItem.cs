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
}
