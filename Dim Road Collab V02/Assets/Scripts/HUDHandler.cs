using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//created by Ian D. on 2-16-2023
public class HUDHandler : MonoBehaviour
{
    public TutorialManager tManager;
    public TextMeshProUGUI rightText;
    public Image rightImage;
    public GameObject rightBlock;

    public TextMeshProUGUI leftText;
    public Image leftImage;
    public GameObject leftBlock;

    public TextMeshProUGUI bottomText;
    public TextMeshProUGUI bottomKeyText;
    public Image bottomImage;
    public GameObject bottomBlock;

    public Sprite rightClick;
    public Sprite leftClick;
    public Sprite rightHold;
    public Sprite leftHold;
    public Sprite bottomKey;

    public bool itemLookDistributed;
    public GameObject tutorialItem;
    public bool toolLookDistributed;
    public GameObject tutorialTool;
    public bool debrisLookDistributed;
    public GameObject tutorialDebris;

    
    public void TargetObject(InteractionType inType, string targetName)
    {
        if (inType == InteractionType.Basic)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            InteractPrompt();
            rightText.text += targetName;
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
            if (!debrisLookDistributed)
            {
                debrisLookDistributed = true;
                tManager.DisplayNotification(tutorialDebris);
            }
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
            if (!itemLookDistributed)
            {
                itemLookDistributed = true;
                tManager.DisplayNotification(tutorialItem);
            }
        }
        if (inType == InteractionType.Tool)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            PickUpPrompt();
            rightText.text += targetName;
            if(!toolLookDistributed)
            {
                toolLookDistributed = true;
                tManager.DisplayNotification(tutorialTool);
            }
        }
        if (inType == InteractionType.Machine)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            InteractPrompt();
            rightText.text += targetName;
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
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            InteractPrompt();
            rightText.text += targetName;
        }
        if (inType == InteractionType.OnlyTool)
        {
            rightBlock.SetActive(true);
            leftBlock.SetActive(false);
            NeedToolPrompt();
            rightText.text += targetName;
            //not sure what to do here
            //maybe nothing at all, and trigger a new method
            //TargetWithTool might be triggered by the tool handling script then
        }
    }

    public void TargetWithTool(string targetName)
    {
        rightBlock.SetActive(true);
        leftBlock.SetActive(false);
        UseToolPrompt();
        rightText.text += targetName;
    }

    public void NoTarget()
    {
        rightBlock.SetActive(false);
        leftBlock.SetActive(false);
    }

    public void CubePrompt(string state)
    {
        
        if (state == "held")
        {
            bottomBlock.SetActive(true);
            bottomText.text = "store cube";
            bottomKeyText.text = "E";
            bottomImage.sprite = bottomKey;
            return;
        }

        if (state == "stowed")
        {
            bottomBlock.SetActive(true);
            bottomText.text = "retrieve cube";
            bottomKeyText.text = "E";
            bottomImage.sprite = bottomKey;
            return;
        }
        

        bottomBlock.SetActive(false);
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
        rightImage.sprite = rightClick;
        rightText.text = "Interact with ";
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

    public void NeedToolPrompt()
    {
        rightImage.sprite = rightHold;
        rightText.text = "requires a tool";
    }
}
