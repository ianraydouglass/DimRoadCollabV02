using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTrigger : MonoBehaviour
{
    public LocalStructure structure;

    void Start()
    {
        if (structure == null)
        {
            structure = gameObject.GetComponent<LocalStructure>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            structure.dManager.EnterStructure(structure);
        }
    }
}
