using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public float zoneSpan = 1f;
    public List<GameObject> playerObjects = new List<GameObject>();
    public GameObject endObjectPoint;
    public EndingManager eManager;
    public int thisEnding = 1;

    void Start()
    {
        if (!endObjectPoint)
        {
            endObjectPoint = this.gameObject;
        }
    }

    void Update()
    {
        if(isPlayerWithin())
        {
            eManager.endingGroup.alpha = FadeValue();
        }

    }

    public float FadeValue()
    {
        if (playerObjects.Count == 0)
        {
            return 0f;
        }
        float distance = Vector3.Distance(playerObjects[0].transform.position, endObjectPoint.transform.position);
        if (distance > zoneSpan)
        {
            return 0f;
        }
        return ((zoneSpan - distance)/zoneSpan);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !playerObjects.Contains(other.gameObject))
        {
            playerObjects.Add(other.gameObject);
            eManager.SetEnding(thisEnding);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerObjects.Contains(other.gameObject))
        {
            playerObjects.Remove(other.gameObject);
        }
        
    }

    public bool isPlayerWithin()
    {
        if(playerObjects.Count > 0)
        {
            return true;
        }
        return false;
    }
}
