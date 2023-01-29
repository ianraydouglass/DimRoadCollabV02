using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : MonoBehaviour
{
    public ToolType type;

    //used to initiate the action
    public bool ToolCheck(GameObject targetObject)
    {
        ToolResponse[] otherResponse = targetObject.GetComponents<ToolResponse>();
        if (otherResponse.Length == 0)
        {
            return false;
        }
        foreach(ToolResponse response in otherResponse)
        {
            if (response.type == type)
            {
                return true;
            }
        }
        return false;
    }

    public void ToolActionComplete(GameObject targetObject)
    {
        ToolResponse[] otherResponse = targetObject.GetComponents<ToolResponse>();
        if (otherResponse.Length == 0)
        {
            return;
        }
        foreach (ToolResponse response in otherResponse)
        {
            response.RespondToToolAction(type);
        }
    }


}
