using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressHandler : MonoBehaviour
{
    public GameObject longPress;
    public Image longPressBar;
    public Animator toolAnimator;
    public GameObject toolUse;
    public Image toolUseBar;

    public void ToolProgress(int tTime)
    {
        toolUse.SetActive(true);
        float tSpeed = 1f;
        if (tTime > 1)
        {
            tSpeed = 1 / tTime;
        }
        //the calc here doesnt seem to work
        //opted to fudge it
        toolAnimator.Play("ToolProgress");
        toolAnimator.speed = 0.33f;
    }

    public void CancelTool()
    {
        toolUse.SetActive(false);
    }
}
