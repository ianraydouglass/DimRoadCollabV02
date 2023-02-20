using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    
    public DangerManager dManager;

    void Start()
    {
        if(!dManager)
        {
            dManager = GameObject.Find("Danger Panel").GetComponent<DangerManager>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckCollapseEnd();
        }
    }

    public void CheckCollapseEnd()
    {
        if (dManager.currentStructure.damageLevel > 2)
        {
            dManager.CollapseEnd();
        }
    }
}
