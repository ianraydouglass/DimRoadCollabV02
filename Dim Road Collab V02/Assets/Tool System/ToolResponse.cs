using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolResponse : MonoBehaviour
{
    public ToolType type;
    public string keyPhrase;

    public void RespondToToolAction(ToolType otherType)
    {
        if (otherType != type)
        {
            return;
        }
        BroadcastMessage("PerformResponse", keyPhrase);
        Debug.Log("Tool use to get response from " + keyPhrase);
    }
}
