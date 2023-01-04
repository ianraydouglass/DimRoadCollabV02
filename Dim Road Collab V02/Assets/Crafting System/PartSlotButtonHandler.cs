using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PartSlotButtonHandler : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler
{
    public int slotNumber;
    public CraftMenuManager craftMenu;
    public bool pointerOn = false;
    public bool selectorOn = false;
    [SerializeField]
    private bool isFirstSelected = false;
    public EventSystem eventSystem;
    public Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
        if (isFirstSelected)
        {
            /*
            GameObject eventObject = GameObject.Find("UI_EventSystem");
            eventSystem = eventObject.GetComponent<EventSystem>();
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            */
            
            thisButton.Select();
        }
    }

    /*
    void OnEnable()
    {
        thisButton = GetComponent<Button>();
        if (isFirstSelected)
        {
            

            thisButton.Select();
        }
    }
    */

    
    //when moused over
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        pointerOn = true;
        Debug.Log("Slot " + slotNumber + " was triggered by pointer enter");
        SelectRow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOn = false;
    }
    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
        
        selectorOn = true;
        Debug.Log("Slot " + slotNumber + " was triggered by selection");
        SelectRow();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectorOn = false;
    }

    public void SelectRow()
    {
        
        craftMenu.ShiftFromRow();
        craftMenu.ShiftToRow(slotNumber);
    }

    
}
