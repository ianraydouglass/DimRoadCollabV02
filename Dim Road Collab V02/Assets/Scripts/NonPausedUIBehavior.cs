using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NonPausedUIBehavior : MonoBehaviour
{
    public GameEvent menuRight;
    public GameEvent menuLeft;
    public GameEvent menuAccept;
    public GameEvent menuCancel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAcceptMenu()
    {
        menuAccept.Raise();
    }
    public void OnCencelMenu()
    {
        menuCancel.Raise();
    }
    public void OnNavigateMenu(InputValue value)
    {
        Vector2 navigationVector = value.Get<Vector2>();
        float navX = navigationVector.x;
        if (navX > 0.01f)
        {
            NavigateRight();
        }
        if (navX < -0.01f)
        {
            NavigateLeft();
            //navigate left
        }
    }

    public void NavigateRight()
    {
        menuRight.Raise();
    }

    public void NavigateLeft()
    {
        menuLeft.Raise();
    }
}
