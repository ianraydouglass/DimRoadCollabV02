using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionStructure : MonoBehaviour
{
    public float maxRegionIntegrity = 0f;
    public float currentRegionIntegrity = 0f;
    public float regionDangerValue;

    public DangerManager dManager;

    public List <LocalStructure> localStructures = new List<LocalStructure>();

    public void SetUpIntegrity()
    {
        if(maxRegionIntegrity < 1f)
        {
            maxRegionIntegrity = dManager.defaultRegionIntegrity;
            currentRegionIntegrity = dManager.defaultRegionIntegrity;
        }
        regionDangerValue = maxRegionIntegrity * 0.1f;
    }

    public void SetLocals(DangerManager dangerManager)
    {
        if (localStructures.Count > 0)
        {
            foreach(LocalStructure structure in localStructures)
            {
                structure.dManager = dangerManager;
                structure.region = this;
                structure.SetUpIntegrity();
            }
        }
    }

    public void ReceiveRegionDamage(float damageValue)
    {
        currentRegionIntegrity -= damageValue;
        if (currentRegionIntegrity <= 0f)
        {
            dManager.CollapseEnd();
        }
        if (currentRegionIntegrity <= regionDangerValue)
        {
            dManager.SetDanger(true);
        }
        else
        {
            dManager.SetDanger(false);
        }

    }
}
