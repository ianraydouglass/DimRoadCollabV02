using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUseManager : MonoBehaviour
{
    public ToolItem heldTool;
    public ToolAction toolAction;
    public ToolHUDManager hudManager;
    public List<ToolItem> currentTools = new List<ToolItem>();

    public void PickupTool(ToolItem incomingTool)
    {
        
        ToolItem newTool = Instantiate(incomingTool) as ToolItem;
        currentTools.Add(newTool);
        //newItem.SetupItem();
        //NotifyAdd(newItem);
    }

    public bool CanUseTool(GameObject targetObject)
    {
        if (!heldTool)
        {
            return false;
        }
        
        return toolAction.ToolCheck(targetObject);
    }

    public int CurrentToolTime()
    {
        if(!heldTool)
        {
            return 1;
        }
        return heldTool.GetUseTime();
    }
    public void FinishToolUse(GameObject targetObject)
    {
        if (!heldTool)
        {
            return;
        }
        toolAction.ToolActionComplete(targetObject);

    }

    public void CancelToolUse()
    {
        Debug.Log("Display Tool Cancel Message");
    }

    public void DropTool(ToolItem outgoingTool)
    {
        if (currentTools.Contains(outgoingTool))
        {
            //instantiate thing
            currentTools.Remove(outgoingTool);
        }
    }
    public void DropHeldTool()
    {
        if (!heldTool)
        {
            return;
        }
        if (currentTools.Contains(heldTool))
        {
            //instantiate thing
            currentTools.Remove(heldTool);
        }
    }
    public void CycleToolPositive()
    {
        if (currentTools.Count == 0)
        {
            return;
        }
        if (!heldTool)
        {
            HoldTool(currentTools[0]);
            RefreshToolHud();
            return;
        }
        if (currentTools.Contains(heldTool))
        {
            int currentIndex = currentTools.IndexOf(heldTool);
            if (currentIndex == LastTool())
            {
                heldTool = null;
            }
            else
            {
                int newIndex = currentIndex + 1;
                HoldTool(currentTools[newIndex]);
            }
            
        }

        RefreshToolHud();
    }
    public void CycleToolNegative()
    {
        if (currentTools.Count == 0)
        {
            return;
        }
        if (!heldTool)
        {
            HoldTool(currentTools[LastTool()]);
            RefreshToolHud();
            return;
        }
        if (currentTools.Contains(heldTool))
        {            
            if (currentTools[0] == heldTool)
            {
                heldTool = null;
            }
            else
            {
                int currentIndex = currentTools.IndexOf(heldTool);
                int newIndex = currentIndex - 1;
                HoldTool(currentTools[newIndex]);
            }
        }
        RefreshToolHud();
    }

    public void RefreshToolHud()
    {
        if(HasTool())
        {
            hudManager.EquipTool(heldTool);
        }
        else
        {
            hudManager.UnEquipTool();
        }
    }

    public void HoldTool(ToolItem tool)
    {
        heldTool = tool;
        toolAction.type = tool.GetToolType();
    }

    public bool HasTool()
    {
        if (!heldTool)
        {
            return false;
        }
        return true;
    }

    public int LastTool()
    {
        int toolCount = currentTools.Count;
        return toolCount - 1;
    }

}
