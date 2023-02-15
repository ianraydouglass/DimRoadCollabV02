using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDHandler : MonoBehaviour
{
    public TextMeshProUGUI rightText;
    public Image rightImage;
    public GameObject rightBlock;

    public TextMeshProUGUI leftText;
    public Image leftImage;
    public GameObject leftBlock;

    public Sprite rightClick;
    public Sprite leftClick;
    public Sprite rightHold;
    public Sprite leftHold;
    
    public void TargetObject(InteractionType inType, string targetName)
    {
        if (inType == InteractionType.Basic)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
            InteractPrompt();
            leftText.text += targetName;
        }
        if (inType == InteractionType.Cube)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            HoistPrompt();
            rightText.text += targetName;
        }
        if (inType == InteractionType.DebrisCube)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(true);
            HoistPrompt();
            BreakPrompt();
            rightText.text += targetName;
            leftText.text += targetName;
        }
        if (inType == InteractionType.LooseDebris)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
        }
        if (inType == InteractionType.Item)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
        }
        if (inType == InteractionType.Tool)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
        }
        if (inType == InteractionType.Machine)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
        }
        if (inType == InteractionType.UtilityCube)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(true);
        }
        if (inType == InteractionType.Button)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
        }
    }

    public void NoTarget()
    {
        rightBlock.SetActive(false);
        leftBlock.SetActive(false);
    }

    public void HoistPrompt()
    {
        rightImage.sprite = leftHold;
        rightText.text = "Lift ";
    }

    public void PickUpPrompt()
    {
        leftImage.sprite = leftClick;
        leftText.text = "Pick up ";
    }

    public void InteractPrompt()
    {
        leftImage.sprite = leftClick;
        leftText.text = "Interact with ";
    }

    public void BreakPrompt()
    {
        leftImage.sprite = rightHold;
        leftText.text = "Break ";
    }

    public void PackPrompt()
    {
        rightImage.sprite = rightHold;
        rightText.text = "Pack ";
    }

    public void UseToolPrompt()
    {
        rightImage.sprite = rightHold;
        rightText.text = "use tool on ";
    }
}
