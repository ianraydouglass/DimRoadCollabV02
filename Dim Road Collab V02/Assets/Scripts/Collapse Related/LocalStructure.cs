using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStructure : MonoBehaviour
{
    public float maxLocalIntegrity = 0f;
    public float currentLocalIntegrity = 0f;

    public DangerManager dManager;
    public RegionStructure region;
    public SegmentStability stability;
    public bool freezeDamage;
    [Range(0, 3)]
    public int damageLevel = 0;
    private Coroutine collapseTimer;
    private bool timerRunning;
    private int delayTimer = 10;

    void Start()
    {
        if (!stability)
        {
            stability = this.transform.parent.gameObject.GetComponent<SegmentStability>();
        }
    }

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
        if (damageLevel >= 3)
        {
            DamageRegion(damageValue);
            return;
        }
        if (freezeDamage)
        {
            DamageRegion(damageValue);
            return;
        }
        if (maxLocalIntegrity == 0f)
        {
            DamageRegion(damageValue);
            return;
        }
        if (currentLocalIntegrity <= 0f)
        {
            DamageRegion(damageValue);
            currentLocalIntegrity = 0f;
            return;
        }
        currentLocalIntegrity -= damageValue;
        if (currentLocalIntegrity <= 0f)
        {
            currentLocalIntegrity = 0f;
            damageLevel = 2;
            stability.SetDamageLevel(2);
            DamageRegion(damageValue);
            return;
        }
        float h = 10f;
        if(maxLocalIntegrity  > 0 && currentLocalIntegrity > 0)
        {
            h = maxLocalIntegrity / 2;
        }
        if (currentLocalIntegrity <= h)
        {
            damageLevel = 1;
            stability.SetDamageLevel(1);
            DamageRegion(damageValue);
            return;
        }
    }

    public void StressSegment(float damageValue)
    {
        if (damageLevel >= 3)
        {
            return;
        }
        if (freezeDamage)
        {
            return;
        }
        if (maxLocalIntegrity == 0f)
        {
            return;
        }
        if (currentLocalIntegrity <= 0f)
        {
            currentLocalIntegrity = 0f;
            return;
        }
        currentLocalIntegrity -= damageValue;
        if (currentLocalIntegrity <= 0f)
        {
            currentLocalIntegrity = 0f;
            damageLevel = 2;
            stability.SetDamageLevel(2);
            return;
        }
        float h = 10f;
        if (maxLocalIntegrity > 0 && currentLocalIntegrity > 0)
        {
            h = maxLocalIntegrity / 2;
        }
        if (currentLocalIntegrity <= h)
        {
            damageLevel = 1;
            stability.SetDamageLevel(1);
            return;
        }
    }

    public void DoomSegment()
    {
        if(freezeDamage)
        {
            return;
        }
        if (damageLevel < 2)
        {
            damageLevel = 2;
            stability.SetDamageLevel(2);

        }
    }

    public void CollapseSegment()
    {
        damageLevel = 3;
        stability.SetDamageLevel(3);

    }

    public void PlayerEnter()
    {
        if(damageLevel >= 2)
        {
            if(timerRunning)
            {
                return;
            }
            collapseTimer = StartCoroutine("DelayCollapseTimer", delayTimer);
            timerRunning = true;
        }
    }
    public void PlayerExit()
    {
        if(damageLevel < 2)
        {
            return;
        }
        if(timerRunning)
        {
            timerRunning = false;
            StopCoroutine(collapseTimer);

        }
        CollapseSegment();
    }

    public void DamageRegion(float damageValue)
    {
        if (region)
        {
            region.ReceiveRegionDamage(damageValue);
        }
    }

    IEnumerator DelayCollapseTimer(int cTimer)
    {
        yield return new WaitForSeconds(cTimer);
        timerRunning = false;
        CollapseSegment();
    }
}
