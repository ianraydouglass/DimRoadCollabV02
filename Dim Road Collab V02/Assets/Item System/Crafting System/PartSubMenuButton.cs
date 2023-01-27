using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script for the sub-menu icon buttons in the crafting interface
public class PartSubMenuButton : MonoBehaviour
{
    public Image partImage;
    public GamePart part;
    public CraftMenuManager2 craftMenu;
    public GameObject partHome;
    public int listIndex;
    private Color occupiedColor = new Color32(253, 140, 87, 255);
    private Color startingColor = new Color32(255, 255, 255, 255);
    public ItemCardHolder partCard;
    // Start is called before the first frame update


    public void SlotThis()
    {
        if (part.IsOccupied())
        {
            return;
        }
        part.SetLock(true);
        craftMenu.AttachPartToSlot(partHome, part, listIndex);
    }

    public void CheckColor()
    {
        if (part.IsOccupied())
        {
            GetComponent<Image>().color = occupiedColor;
        }
        else
        {
            GetComponent<Image>().color = startingColor;
        }
    }

    void OnMouseEnter()
    {
        
        WriteToCard();
    }
    public void WriteToCard()
    {
        if (!part)
        {
            return;
        }
        partCard.DisplayPartInfo(part);
    }
    
}
