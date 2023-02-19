using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleUnityResponse : MonoBehaviour
{
    public UnityEvent Response;
    public string keyPhrase;

    public void PerformResponse(string key)
    {
        if (key == keyPhrase)
        {
            Response.Invoke();
        }
    }
}
