using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptedPartHolder : MonoBehaviour
{
    public GameObject itemPartObject;
    public GamePart acceptedPart;
    [SerializeField]
    private int rowValue;

    public void TogglePartVisibility(bool isVisible)
    {
        if (!itemPartObject)
        {
            return;
        }
        if (isVisible)
        {
            itemPartObject.GetComponent<ItemPartHolder>().PartColorCheck();
        }
        itemPartObject.SetActive(isVisible);
        
    
    }

    public void SetPartAccept(GameObject partToSet)
    {
        itemPartObject = partToSet;
        acceptedPart = partToSet.GetComponent<ItemPartHolder>().thisPart;
        partToSet.GetComponent<ItemPartHolder>().PartColorCheck();
        TogglePartVisibility(false);
    }

    public void UnacceptPart()
    {
        if (itemPartObject)
        {
            acceptedPart = null;
            Destroy(itemPartObject);
            itemPartObject = null;
        }
        
    }

    public int GetRowValue()
    {
        return rowValue;
    }



}
