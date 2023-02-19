using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkBox : MonoBehaviour
{
    public List<GameObject> bonkList = new List<GameObject>();
    
    //disregard the ignore raycast, player, carried objects, phased objects, and trigger zones
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 2 || collider.gameObject.layer == 6 || collider.gameObject.layer == 10 || collider.gameObject.layer == 12 || collider.gameObject.layer == 13)
        {
            
        }
        else if (!bonkList.Contains(collider.gameObject))
        {
            bonkList.Add(collider.gameObject);
            Debug.Log("Added " + gameObject.name);
            
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (bonkList.Contains(collider.gameObject))
        {
            bonkList.Remove(collider.gameObject);
            Debug.Log("Removed " + gameObject.name);

        }
    }

    public bool IsClear()
    {
        if (bonkList.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }    
    }
}
