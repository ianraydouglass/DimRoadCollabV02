using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacementPuzzle : MonoBehaviour
{
    public bool puzzleSatesfied;
    public CubeTrait requiredTrait;
    public List<GameObject> triggerObjects = new List<GameObject>();
    public GameObject puzzleMaster;


    void Start()
    {
        if (!puzzleMaster)
        {
            puzzleMaster = this.gameObject;
        }
    }
    public void DetectionList(List<GameObject> newList)
    {
        triggerObjects = newList;
        if (newList.Count > 0)
        {
            CheckRequirements();
        }
    }

    public void CheckRequirements()
    {
        foreach(GameObject o in triggerObjects)
        {
            CubeProperties p = o.GetComponent<CubeProperties>();
            if (p != null)
            {
                if (p.cubeTraits.Contains(requiredTrait))
                {
                    PuzzleComplete();
                    return;
                }
            }
        }
        PuzzleIncomplete();
    }

    public void PuzzleIncomplete()
    {
        if(puzzleSatesfied)
        {
            puzzleSatesfied = false;
            puzzleMaster.BroadcastMessage("CheckPuzzleConditions");
        }
        puzzleSatesfied = false;
    }

    public void PuzzleComplete()
    {
        if(!puzzleSatesfied)
        {
            puzzleSatesfied = true;
            puzzleMaster.BroadcastMessage("CheckPuzzleConditions");
        }
        puzzleSatesfied = true;
    }

    public bool IsSatesfied()
    {
        return puzzleSatesfied;
    }
}
