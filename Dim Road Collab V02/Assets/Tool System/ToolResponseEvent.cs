using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolResponseEvent : MonoBehaviour
{
    public string keyPhrase;
    public GameEvent responseEvent;

    //why pass a key phrase?
    //so that an interactable can have more than one tool response with more than one tool type
    //and provide different responses to different types
    public void PerformResponse(string key)
    {
        if (key != keyPhrase)
        {
            return;
        }
        responseEvent.Raise();
        Debug.Log("Raised event from keyphrase " + key);
    }

}
