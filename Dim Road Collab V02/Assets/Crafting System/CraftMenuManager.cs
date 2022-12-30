using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenuManager : MonoBehaviour
{
    public GameObject itemPartPrefab;
    public GameObject itemCard;
    public ItemCardHolder cardHolder;
    public GameObject partSlot1;
    public GameObject partSlot2;
    public GameObject partSlot3;
    public GameObject partSlot4;
    public GameObject partSlot5;
    public GameObject partRow1;
    public GameObject partRow2;
    public GameObject partRow3;
    public GameObject partRow4;
    public GameObject partRow5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPart(GameObject partRow, GameItem hostItem, GamePart thisPart)
    {
        GameObject p = Instantiate(itemPartPrefab);
        p.transform.parent = partRow.transform;
        ItemPartHolder ip = p.GetComponent<ItemPartHolder>();
        ip.hostItem = hostItem;
        ip.thisPart = thisPart;
        ip.image.sprite = thisPart.GetSprite();
        ip.itemCard = itemCard;
        ip.cardHolder = cardHolder;
    }
}
