using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CubeTrait", menuName = "Puzzle System/Cube Trait")]
public class CubeTrait : ScriptableObject
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
