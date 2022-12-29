using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionBehavior : MonoBehaviour
{
    public Image positionHolder;
    private bool positionDisplayed = false;
    public Animator positionAnimator;
    public Sprite standingSprite;
    public Sprite crouchingSprite;
    public Sprite crawlingSprite;
    public Image arrowHolder;
    private bool arrowDisplayed = false;
    public Animator arrowAnimator;
    public Sprite arrowUp;
    public Sprite arrowDown;
    public Sprite standingStow;
    public Sprite standingUnStow;
    public Sprite crouchingStow;
    public Sprite crouchingUnStow;
    public Sprite crawlingStow;
    public Sprite crawlingUnStow;

    public Image frontHeldHolder;
    public Image backHeldHolder;
    public bool heldDisplayed = false;
    public Animator frontAnimator;
    public Animator backAnimator;
    public Sprite dropCube;
    public Sprite unknownCube;
    public GameObject standingFrontHolder;
    public GameObject standingBackHolder;
    public GameObject crouchingFrontHolder;
    public GameObject crouchingBackHolder;
    public GameObject crawlingFrontHolder;
    public GameObject crawlingBackHolder;
    public Interactable hoistedObject;
    private bool hasHoistedObject;
    public Interactable stowedObject;
    private bool hasStowedObject;
    public StarterAssets.FirstPersonController controller;

    private bool frontDropNotification = false;
    private bool frontDropComplete = true;
    private bool backDropNotification = false;
    private bool backDropComplete = true;
    

    void Start()
    {
        if (!controller)
        {
            GameObject player = GameObject.Find("PlayerCapsule");
            controller = player.GetComponent<StarterAssets.FirstPersonController>();
        }
    }

    public void StateHideTimerSet()
    {
        StopCoroutine("StateChangeCheck");
        StartCoroutine("StateChangeCheck");
    }
    public void StateFadeIn()
    {
        if (!positionDisplayed)
        {
            positionDisplayed = true;
            positionAnimator.Play("HudFadeIn");
        }
    }

    public void StateFadeOut()
    {
        if (positionDisplayed)
        {
            positionAnimator.Play("HudFadeOut");
            positionDisplayed = false;
        }
    }

    public void HolderUpdateStanding()
    {
        HeldFadeOut();
        frontHeldHolder = standingFrontHolder.GetComponent<Image>();
        backHeldHolder = standingBackHolder.GetComponent<Image>();
        frontAnimator = standingFrontHolder.GetComponent<Animator>();
        backAnimator = standingBackHolder.GetComponent<Animator>();
        DisplayHoist();
    }

    public void HolderUpdateCrouching()
    {
        HeldFadeOut();
        frontHeldHolder = crouchingFrontHolder.GetComponent<Image>();
        backHeldHolder = crouchingBackHolder.GetComponent<Image>();
        frontAnimator = crouchingFrontHolder.GetComponent<Animator>();
        backAnimator = crouchingBackHolder.GetComponent<Animator>();
        DisplayHoist();
    }

    public void HolderUpdateCrawling()
    {
        HeldFadeOut();
        frontHeldHolder = crawlingFrontHolder.GetComponent<Image>();
        backHeldHolder = crawlingBackHolder.GetComponent<Image>();
        frontAnimator = crawlingFrontHolder.GetComponent<Animator>();
        backAnimator = crawlingBackHolder.GetComponent<Animator>();
        DisplayHoist();
    }

    public void ArrowHideTimerSet()
    {
        StopCoroutine("ArrowStateCheck");
        StartCoroutine("ArrowStateCheck");

    }

    public void ArrowFadeIn()
    {
        if (!arrowDisplayed)
        {
            arrowDisplayed = true;
            arrowAnimator.Play("HudFadeIn");
        }
    }

    public void ArrowFadeOut()
    {
        if (arrowDisplayed)
        {
            arrowAnimator.Play("HudFadeOut");
            arrowDisplayed = false;
        }
    }

    public void HeldHideTimerSet()
    {
        StopCoroutine("HeldChangeCheck");
        StartCoroutine("HeldChangeCheck");
    }
    public void HeldFadeIn()
    {
        if (!heldDisplayed)
        {
            heldDisplayed = true;
            if (hasHoistedObject || frontDropNotification)
            {
                frontAnimator.Play("HudFadeIn");
            }
            if (hasStowedObject || backDropNotification)
            {
                backAnimator.Play("HudFadeIn");
            }
            
        }
    }

    public void RegisterDropFront()
    {
        frontDropNotification = true;
        frontDropComplete = false;
    }

    public void RegisterDropBack()
    {
        backDropNotification = true;
        backDropComplete = false;
    }

    //since front and back are set via script, gotta check to make sure they aren't null before trying to animate.
    public void HeldFadeOut()
    {
        if (heldDisplayed)
        {
            if ((frontAnimator != null && hasHoistedObject == true) || frontDropNotification)
            {
                frontAnimator.Play("HudFadeOut");
                
            }
            if ((backAnimator != null && hasStowedObject == true) || backDropNotification)
            {
                backAnimator.Play("HudFadeOut");
                
            }
            /*if (frontDropComplete && frontDropNotification)
            {
                frontDropNotification = false;
            }
            if (backDropComplete && backDropNotification)
            {
                backDropNotification = false;
            }*/
            
            heldDisplayed = false;
        }
        if (frontDropComplete && frontDropNotification)
        {
            frontDropNotification = false;
        }
        if (backDropComplete && backDropNotification)
        {
            backDropNotification = false;
        }
    }
    public void RegisterCubes()
    {
        if (controller.hoistedObject)
        {
            frontHeldHolder.enabled = true;
            hasHoistedObject = true;
            hoistedObject = controller.hoistedObject.GetComponent<Interactable>();
        }
        else if (frontDropNotification)
        {
            hasHoistedObject = false;
            hoistedObject = null;
            frontHeldHolder.enabled = true;
        }
        else
        {
            hasHoistedObject = false;
            hoistedObject = null;
            frontHeldHolder.enabled = false;
        }
        if (controller.stowedObject)
        {
            backHeldHolder.enabled = true;
            hasStowedObject = true;
            stowedObject = controller.stowedObject.GetComponent<Interactable>();
        }
        else if (backDropNotification)
        {
            hasStowedObject = false;
            stowedObject = null;
            backHeldHolder.enabled = true;
        }
        else
        {
            hasStowedObject = false;
            stowedObject = null;
            backHeldHolder.enabled = false;
        }
    }
    public void DisplayHoist()
    {
        RegisterCubes();
        
        if (hoistedObject)
        {
            if (hoistedObject.objectIcon)
            {
                frontHeldHolder.sprite = hoistedObject.objectIcon;
            }
            else
            {
                frontHeldHolder.sprite = unknownCube;
            }
        }
        else if (frontDropNotification)
        {
            frontHeldHolder.sprite = dropCube;
            frontDropComplete = true;
        }
        else
        {
            frontHeldHolder.sprite = null;
        }
        if (stowedObject)
        {
            if (stowedObject.objectIcon)
            {
                backHeldHolder.sprite = stowedObject.objectIcon;

            }
            else
            {
                backHeldHolder.sprite = unknownCube;
            }
        }
        else if (backDropNotification)
        {
            backHeldHolder.sprite = dropCube;
            backDropComplete = true;
        }
        else
        {
            backHeldHolder.sprite = null;
        }
        //ArrowFadeOut();
        HeldHideTimerSet();
        HeldFadeIn();
    }
        
    public void DisplayStand()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = standingSprite;
        StateFadeIn();
        HolderUpdateStanding();
    }

    public void DisplayCrouch()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crouchingSprite;
        StateFadeIn();
       
        HolderUpdateCrouching();
    }

    public void DisplayCrawl()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crawlingSprite;
        StateFadeIn();
        
        HolderUpdateCrawling();
    }

    public void DisplayUpToStand()
    {
        StateHideTimerSet();
        positionHolder.sprite = standingSprite;
        StateFadeIn();
        ArrowHideTimerSet();
        arrowHolder.sprite = arrowUp;
        ArrowFadeIn();
        HolderUpdateStanding();
        
    }

    

    public void DisplayDownToCrouch()
    {
        StateHideTimerSet();
        positionHolder.sprite = crouchingSprite;
        StateFadeIn();
        ArrowHideTimerSet();
        arrowHolder.sprite = arrowDown;
        ArrowFadeIn();
        HolderUpdateCrouching();
    }

    public void DisplayUpToCrouch()
    {
        StateHideTimerSet();
        positionHolder.sprite = crouchingSprite;
        StateFadeIn();
        ArrowHideTimerSet();
        arrowHolder.sprite = arrowUp;
        ArrowFadeIn();
        HolderUpdateCrouching();
    }

    public void DisplayDownToCrawl()
    {
        StateHideTimerSet();
        positionHolder.sprite = crawlingSprite;
        StateFadeIn();
        ArrowHideTimerSet();
        arrowHolder.sprite = arrowDown;
        ArrowFadeIn();
        HolderUpdateCrawling();

    }

    public void DisplayStandingDropFront()
    {
        RegisterDropFront();
        DisplayStand();
    }

    public void DisplayStandingDropBack()
    {
        RegisterDropBack();
        DisplayStand();
    }

    public void DisplayCrouchingDropFront()
    {
        RegisterDropFront();
        DisplayCrouch();
    }

    public void DisplayCrouchingDropBack()
    {
        RegisterDropBack();
        DisplayCrouch();
    }

    public void DisplayCrawlingDropFront()
    {
        RegisterDropFront();
        DisplayCrawl();
    }

    public void DisplayCrawlingDropBack()
    {
        RegisterDropBack();
        DisplayCrawl();
    }

    public void DisplayStandingStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = standingStow;
        StateFadeIn();
        HolderUpdateStanding();
    }

    public void DisplayStandingUnStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = standingUnStow;
        StateFadeIn();
        HolderUpdateStanding();
    }

    public void DisplayCrouchingStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crouchingStow;
        StateFadeIn();
        HolderUpdateCrouching();
    }

    public void DisplayCrouchingUnStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crouchingUnStow;
        StateFadeIn();
        HolderUpdateCrouching();
    }

    public void DisplayCrawlingStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crawlingStow;
        StateFadeIn();
        HolderUpdateCrawling();
    }

    public void DisplayCrawlingUnStow()
    {
        ArrowFadeOut();
        StateHideTimerSet();
        positionHolder.sprite = crawlingUnStow;
        StateFadeIn();
        HolderUpdateCrawling();
    }

    IEnumerator StateChangeCheck()
    {
        yield return new WaitForSeconds(5);
        StateFadeOut();
    }

    IEnumerator ArrowStateCheck()
    {
        yield return new WaitForSeconds(5);
        ArrowFadeOut();
    }

    IEnumerator HeldChangeCheck()
    {
        yield return new WaitForSeconds(5);
        HeldFadeOut();
    }
}
