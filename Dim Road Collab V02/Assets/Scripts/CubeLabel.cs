using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLabel : MonoBehaviour
{
    public GameObject labelPivot;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        

    }

    

    void LateUpdate()
    {
        if (labelPivot)
        {
            labelPivot.transform.LookAt(mainCamera.transform);
        }
    }
}
