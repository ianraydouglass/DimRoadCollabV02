using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//created by Ian D. on 021923
public class MaterialShifter : MonoBehaviour
{
    public GameObject toShift;
    public List<Material> materials = new List<Material>();

    

    

    public void ShiftToMaterial(int mIndex)
    {
        if (materials.Count > mIndex && mIndex >= 0)
        {
            toShift.GetComponent<MeshRenderer>().material = materials[mIndex];
        }
    }

}
