using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for the empty slot buttons in the crafting interface
public class PartSlotButtonHandler2 : MonoBehaviour
{
    public PartTrait slotTrait;
    public GamePart slotPart;
    public CraftMenuManager2 craftMenu;
    public int listIndex;
    public GameObject slottedPart;

    public void OpenPartSubMenu()
    {
        if (slotPart)
        {
            craftMenu.ReleasePart(slotPart);
            slottedPart.SetActive(false);

        }
        craftMenu.SetPartSubMenu(this.gameObject, slotTrait, listIndex);
    }
}
