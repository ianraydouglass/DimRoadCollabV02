using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionStructure : MonoBehaviour
{
    public float maxRegionIntegrity = 0f;
    public float currentRegionIntegrity = 0f;
    public float regionDangerValue;
    public int regionDangerLevel = 0;

    public DangerManager dManager;

    public List <LocalStructure> localStructures = new List<LocalStructure>();

    public void SetUpIntegrity()
    {
        if(maxRegionIntegrity < 1f)
        {
            maxRegionIntegrity = dManager.defaultRegionIntegrity;
            currentRegionIntegrity = dManager.defaultRegionIntegrity;
        }
        regionDangerValue = maxRegionIntegrity * 0.5f;
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
        if (maxRegionIntegrity <= 0f)
        {
            return;
        }
        currentRegionIntegrity -= damageValue;
        if (currentRegionIntegrity <= 0f)
        {
            regionDangerLevel = 2;
            dManager.SetDanger(true);
            DoomAll();
            return;
            //dManager.CollapseEnd();
        }
        if (currentRegionIntegrity <= regionDangerValue)
        {
            regionDangerLevel = 1;
            dManager.SetDanger(true);
            return;
        }
        else
        {
            dManager.SetDanger(false);
        }

    }

    public void DoomAll()
    {
        if (localStructures.Count == 0)
        {
            return;
        }
        foreach (LocalStructure structure in localStructures)
        {
            structure.DoomSegment();
        }
    }
}
