using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPuzzleActive : MonoBehaviour
{
    public string stationTitle;
    public PartTrait requiredTrait;
    public bool consumesItem;
    public PuzzleMenuManager puzzleMenu;
    public GameEvent pauseEvent;
    public string puzzleKey;


    void Start()
    {
        if (!puzzleMenu)
        {
            GameObject menus = GameObject.Find("Non-Paused UI Canvas");
            puzzleMenu = menus.GetComponent<PuzzleMenuManager>();
        }
    }

    public void StationActive()
    {
        pauseEvent.Raise();
        puzzleMenu.SetPuzzleObject(this.gameObject);
        puzzleMenu.OpenPuzzleMenu(stationTitle, requiredTrait, consumesItem);
    }

    public void PuzzleActive()
    {
        BroadcastMessage("PerformResponse", puzzleKey);
        Debug.Log("puzzle solved with key: " + puzzleKey);
    }
}
