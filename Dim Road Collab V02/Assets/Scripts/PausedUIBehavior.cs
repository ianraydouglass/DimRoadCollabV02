using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedUIBehavior : MonoBehaviour
{
    public GameEvent pauseCancel;
    public void OnCancelMenu()
    {
        pauseCancel.Raise();
    }
}
