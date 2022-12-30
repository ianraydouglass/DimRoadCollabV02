
using UnityEngine;

[System.Serializable]

public class PartSocket
{
    public GamePart part;
    public PartTrait requiredTrait;

    public bool FitCheck()
    {
        if (part.HasTrait(requiredTrait))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
