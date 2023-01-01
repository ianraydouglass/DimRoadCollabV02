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
    public bool isAccepted = false;


    
    void Start()
    {
        image.color = new Color(255f, 255f, 255f);
        PartColorCheck();
    }

    public void PartColorCheck()
    {
        if (!thisPart)
        {
            return;
        }
        if (isAccepted)
        {
            image.color = new Color(87f, 234f, 253f);
        }
        else if (thisPart.IsOccupied())
        {
            image.color = new Color(253f, 140f, 87f);
        }
        else
        {
            image.color = new Color(255f, 255f, 255f);
        }
    }

    public void AcceptPart()
    {
        thisPart.SetLock(true);
        isAccepted = true;
        PartColorCheck();
    }
    public void UnAcceptPart()
    {
        if(isAccepted)
        {
            thisPart.SetLock(false);
            isAccepted = false;
            PartColorCheck();
        }
        
    }

    public void ViewItemCard()
    {
        if (isEndCap)
        {
            Debug.Log("DisablingItemPreview");
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
