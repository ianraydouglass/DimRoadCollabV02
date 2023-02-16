using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//created by Ian D. on 2-16-2023
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
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
            HoistPrompt();
            leftText.text += targetName;
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
            PackPrompt();
            rightText.text += targetName;
        }
        if (inType == InteractionType.Item)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            PickUpPrompt();
            rightText.text += targetName;
        }
        if (inType == InteractionType.Tool)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            PickUpPrompt();
            rightText.text += targetName;
        }
        if (inType == InteractionType.Machine)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
            InteractPrompt();
            leftText.text += targetName;
        }
        if (inType == InteractionType.UtilityCube)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(true);
            HoistPrompt();
            InteractPrompt();
            rightText.text += targetName;
            leftText.text += targetName;
        }
        if (inType == InteractionType.Button)
        {
            rightBlock.SetActive(false);
            leftBlock.SetActive(true);
            InteractPrompt();
            leftText.text += targetName;
        }
        if (inType == InteractionType.OnlyTool)
        {
            //not sure what to do here
            //maybe nothing at all, and trigger a new method
            //TargetWithTool might be triggered by the tool handling script then
        }
    }

    public void TargetWithTool()
    {

    }

    public void NoTarget()
    {
        rightBlock.SetActive(false);
        leftBlock.SetActive(false);
    }

    public void HoistPrompt()
    {
        leftImage.sprite = leftHold;
        leftText.text = "Lift ";
    }

    public void PickUpPrompt()
    {
        rightImage.sprite = rightClick;
        rightText.text = "Pick up ";
    }

    public void InteractPrompt()
    {
        leftImage.sprite = leftClick;
        leftText.text = "Interact with ";
    }

    public void BreakPrompt()
    {
        rightImage.sprite = rightHold;
        rightText.text = "Break ";
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
