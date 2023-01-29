using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ToolType", menuName = "Equipment System/Tool Type")]
public class ToolType : ScriptableObject
{
    [SerializeField]
    private string displayName;

    public string GetName()
    {
        if (displayName == "")
        {
            return ToString();
        }
        return displayName;
    }
}
