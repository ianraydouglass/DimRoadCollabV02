using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPartHolder : MonoBehaviour
{
    public GameItem hostItem;
    public GamePart thisPart;
    public Image image;
    public GameObject itemCard;
    public ItemCardHolder cardHolder;
    public bool isEndCap = false;
    
    void Start()
    {

    }

    public void SlotThis()
    {
        if (isEndCap)
        {
            cardHolder.cardContents.SetActive(false);
        }
        else
        { 
            cardHolder.cardContents.SetActive(true);
            cardHolder.itemImage.sprite = hostItem.GetSprite();
            cardHolder.titleText.text = hostItem.GetName();
            cardHolder.descriptionText.text = hostItem.TellDescription();
        }
    }
}
