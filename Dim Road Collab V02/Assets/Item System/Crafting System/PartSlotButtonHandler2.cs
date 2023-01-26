using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        }
        craftMenu.SetPartSubMenu(this.gameObject, slotTrait, listIndex);
    }
}
