using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSignal : MonoBehaviour
{
    public EndingManager eManager;
    public bool endingSignalled;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !endingSignalled)
        {
            endingSignalled = true;
            eManager.TrueEnding();
        }
    }
}
