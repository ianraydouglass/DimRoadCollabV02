using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PartTrait", menuName = "Item System/Trait")]
public class PartTrait : ScriptableObject
{
    [SerializeField]
    private string traitNotes = "No trait notes available.";
    [SerializeField]
    private string defaultPurpose = "Purpose Unknown";
    [SerializeField]
    private bool itemLevelTrait = false;

    public string GetName()
    {
        return ToString();
    }

    public string GetNotes()
    {
        return traitNotes;
    }

    public bool IsItemTrait()
    {
        return itemLevelTrait;
    }

    public string GetPurpose()
    {
        return defaultPurpose;
    }

}
