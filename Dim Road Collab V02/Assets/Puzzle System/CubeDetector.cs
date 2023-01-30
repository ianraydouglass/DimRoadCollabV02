using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDetector : MonoBehaviour
{
    public List<GameObject> triggerObjects = new List<GameObject>();
    public GameObject puzzleObject;
    void OnTriggerEnter(Collider other)
    {
        triggerObjects.Add(other.gameObject);
        ListUpdated();
    }
    void OnTriggerExit(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if(triggerObjects.Contains(otherObject))
        {
            triggerObjects.Remove(otherObject);
            ListUpdated();
        }
    }
    void ListUpdated()
    {
        puzzleObject.BroadcastMessage("DetectionList", triggerObjects);
    }
}
