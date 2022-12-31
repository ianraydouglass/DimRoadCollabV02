using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHolder : MonoBehaviour
{
    public GameEvent gameEvent;

    public void RaiseEvent()
    {
        gameEvent.Raise();
    }
}
