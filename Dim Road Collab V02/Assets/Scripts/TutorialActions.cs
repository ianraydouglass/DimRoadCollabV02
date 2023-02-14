using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActions : MonoBehaviour
{
    public GameEvent crouchEvent;
    public GameEvent moveEvent;

    public void OnGoDown()
    {
        crouchEvent.Raise();
    }

    public void OnMove()
    {
        moveEvent.Raise();
    }
}
