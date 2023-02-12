using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachPartResponse : MonoBehaviour
{
    public GameObject[] partsVisual;
    public GameItem[] partItems;
    public InventoryManager inventory;
    public int currentPartIndex = 0;
    public bool spent;
    public string detachKeyphrase;
    public string bustKeyphrase = "spent";

    void Start()
    {
        if (!inventory)
        {
            GameObject holder = GameObject.Find("Inventory Holder Temporary");
            inventory = holder.GetComponent<InventoryManager>();
        }
    }

    public void PerformResponse(string keyPhrase)
    {
        if (keyPhrase != detachKeyphrase)
        {
            return;
        }
        //PopAtIndex();
        //AdvanceIndex();
        TightAction();
        if (spent)
        {
            BroadcastMessage("PerformResponse", bustKeyphrase);
            Debug.Log("Busting " + gameObject);
        }
    }

    private void AdvanceIndex()
    {
        if (spent)
        {
            return;
        }
        currentPartIndex += 1;
        int indexToLength = currentPartIndex + 1;
        if (indexToLength > partItems.Length)
        {
            spent = true;
        }
    }

    private void PopAtIndex()
    {
        if (spent)
        {
            return;
        }
        partsVisual[currentPartIndex].SetActive(false);
        inventory.AddToInventory(partItems[currentPartIndex]);
    }

    private void TightAction()
    {
        if (spent)
        {
            return;
        }
        partsVisual[currentPartIndex].SetActive(false);
        inventory.AddToInventory(partItems[currentPartIndex]);
        currentPartIndex += 1;
        int indexToLength = currentPartIndex + 1;
        if (indexToLength > partItems.Length)
        {
            spent = true;
        }
    }
}
