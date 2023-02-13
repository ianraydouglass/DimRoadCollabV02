using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActions : MonoBehaviour
{
    public GameEvent crouchEvent;
    public GameEvent crawlEvent;

    public void OnGoDown()
    {
        crouchEvent.Raise();
    }
}
