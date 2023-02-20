using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DangerManager : MonoBehaviour
{
    public TextMeshProUGUI regionText;
    public TextMeshProUGUI localText;
    public string rText = "Regional Structural Integrity: ";
    public string lText = "Local Structural Integrity: ";
    public float moveCost = 1f;
    public float carryCost = 1f;
    public float dropCost = 0.5f;
    public float breakCost = 0.5f;
    public float kickCost = 2f;
    public float delayCost = 0.5f;
    public float toolCost = 0.5f;
    public int delayTimer = 20;
    public float defaultLocalIntegrity = 20f;
    public float defaultRegionIntegrity = 100f;
    public bool cubeCarried = false;
    public bool cubeStowed = false;
    public LocalStructure currentStructure;
    public EndingManager eManager;
    public GameObject dangerIndicator;
    private Coroutine damageTimer;

    public List<LocalStructure> recentStructures = new List<LocalStructure>();
    public List<RegionStructure> activeRegions = new List<RegionStructure>();

    void Start()
    {
        if (activeRegions.Count > 0)
        {
            foreach(RegionStructure region in activeRegions)
            {
                region.dManager = this;
                region.SetLocals(this);
                region.SetUpIntegrity();
            }
        }
        TimerStart();
    }

    public void EnterStructure(LocalStructure structure)
    {
        currentStructure = structure;
        if(recentStructures.Count < 2 && !recentStructures.Contains(structure))
        {
            recentStructures.Add(structure);
            DisplayInfo(structure);
            return;
        }
        if(recentStructures.Contains(structure))
        {
            recentStructures.Remove(structure);
            recentStructures.Add(structure);
            DisplayInfo(structure);
            return;
        }
        MovingDamage(recentStructures[0]);
        recentStructures.Remove(recentStructures[0]);
        recentStructures.Add(structure);
        DisplayInfo(structure);
    }

    public void MovingDamage(LocalStructure structure)
    {
        float mDamage = moveCost;
        if (cubeCarried)
        {
            mDamage += carryCost;
        }
        if (cubeStowed)
        {
            mDamage += carryCost;
        }
        DamageStructure(structure, mDamage);
    }

    public void TimerStart()
    {
        damageTimer = StartCoroutine("DelayDamageTimer", delayTimer);
    }

    public void TimerReset()
    {
        StopCoroutine(damageTimer);
        damageTimer = StartCoroutine("DelayDamageTimer", delayTimer);
    }

    public void DamageStructure(LocalStructure structure, float damageValue)
    {
        structure.RecieveLocalDamage(damageValue);
        TimerReset();
    }

    IEnumerator DelayDamageTimer(int damTimer)
    {
        yield return new WaitForSeconds(damTimer);
        DelayHere();
    }

    public void AddRegion(RegionStructure region)
    {
        activeRegions.Add(region);
        region.dManager = this;
        region.SetLocals(this);
        region.SetUpIntegrity();
    }

    void DisplayInfo(LocalStructure structure)
    {
        string l = lText;
        l += structure.currentLocalIntegrity + " / " + structure.maxLocalIntegrity;
        string r = rText;
        r += structure.region.currentRegionIntegrity + " / " + structure.region.maxRegionIntegrity;
        regionText.text = r;
        localText.text = l;
    }

    public void DropHere()
    {
        DamageStructure(currentStructure, dropCost);
        DisplayInfo(currentStructure);
    }

    public void BreakHere()
    {
        DamageStructure(currentStructure, breakCost);
        DisplayInfo(currentStructure);
    }

    public void KickHere()
    {
        DamageStructure(currentStructure, kickCost);
        DisplayInfo(currentStructure);
    }

    public void DelayHere()
    {
        DamageStructure(currentStructure, delayCost);
        DisplayInfo(currentStructure);
    }

    public void ToolHere()
    {
        DamageStructure(currentStructure, toolCost);
        DisplayInfo(currentStructure);
    }

    public void CollapseEnd()
    {
        eManager.endingGroup.alpha = 1f;
        eManager.SetEnding(0);
        eManager.TrueEnding();
        DisplayInfo(currentStructure);
    }

    public void SetDanger(bool danger)
    {
        dangerIndicator.SetActive(danger);
    }
}
