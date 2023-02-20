using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStructure : MonoBehaviour
{
    public float maxLocalIntegrity = 0f;
    public float currentLocalIntegrity = 0f;

    public DangerManager dManager;
    public RegionStructure region;

    public void SetUpIntegrity()
    {
        if (maxLocalIntegrity < 1f)
        {
            maxLocalIntegrity = dManager.defaultLocalIntegrity;
            currentLocalIntegrity = dManager.defaultLocalIntegrity;
        }
    }

    public void RecieveLocalDamage(float damageValue)
    {
        currentLocalIntegrity -= damageValue;
        if (currentLocalIntegrity <= 0f)
        {
            currentLocalIntegrity = 0f;
        }
        region.ReceiveRegionDamage(damageValue);
    }
}
