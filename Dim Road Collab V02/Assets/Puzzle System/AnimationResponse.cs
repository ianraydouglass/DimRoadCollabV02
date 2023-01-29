using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResponse : MonoBehaviour
{
    public GameObject objectToAnimate;
    private Animator anim;
    public string animToPlay;
    public string keyPhrase;

    void Start()
    {
        anim = objectToAnimate.GetComponent<Animator>();
    }
    public void PerformResponse(string key)
    {
        if (key != keyPhrase)
        {
            return;
        }
        anim.Play(animToPlay);
    }

}
