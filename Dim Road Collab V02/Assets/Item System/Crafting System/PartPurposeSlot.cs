
using UnityEngine;

[System.Serializable]

//meant to replace TraitSlot in items and recepies
public class PartPurposeSlot
{
    private GamePart heldPart;
    public GamePart defaultPart;
    public PartTrait requiredTrait;
    public string purpose = "default";

    public void SetForCreation()
    {
        //fill-in whatever fields need to be filled
        SetUpPurpose();

    }

    public void SetUpPurpose()
    {
        //get a purpose from the required trait if it's still default
        if (purpose != "default" && purpose != "")
        {
            return;
        }
        
        if (requiredTrait)
        {
            purpose = requiredTrait.GetPurpose();
        }
        
        else
        {
            purpose = "Purpose Unknown";
        }
    }

    public void SetPart(GamePart part)
    {
        heldPart = part;
    }
    public GamePart GetPart()
    {
        return heldPart;
    }

    
        
}
