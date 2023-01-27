using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpaceTest : MonoBehaviour
{
    public BoxCollider thisCollider;
    public bool colliderClear = true;
    

    void Start()
    {
        thisCollider = GetComponent<BoxCollider>();
        thisCollider.enabled = false;
    }

    public bool IsClear()
    {
        return colliderClear;
    }

    
}
