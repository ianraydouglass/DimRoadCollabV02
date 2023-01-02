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
    private Color acceptedColor = new Color32(87, 234, 253, 255);
    private Color occupiedColor = new Color32(253, 140, 87, 255);
    private Color startingColor = new Color32(255, 255, 255, 255);



    void Start()
    {
        //image.color = new Color(255f, 255f, 255f);
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
            Debug.Log("setting color to accept");
            GetComponent<Image>().color = acceptedColor;
        }
        else if (thisPart.IsOccupied())
        {
            Debug.Log("setting color to occupied");
            GetComponent<Image>().color = occupiedColor;
        }
        else
        {
            GetComponent<Image>().color = startingColor;
        }
    }

    public void AcceptPart()
    {
        if (thisPart)
        {
            thisPart.SetLock(true);
        }
        
        isAccepted = true;
        PartColorCheck();
    }
    public void UnAcceptPart()
    {
        if (thisPart)
        {
            thisPart.SetLock(false);
        }
        isAccepted = false;
        
        PartColorCheck();

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
