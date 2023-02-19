using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialStep;
    public TutorialManager tManager;
    private bool hasGiven;

    void Start()
    {
        if (!tManager)
        {
            GameObject tPanel = GameObject.Find("Tutorialization Panel");
            tManager = tPanel.GetComponent<TutorialManager>();
        }
    }

    public void AddReminder()
    {
        tManager.DisplayNotification(tutorialStep);
    }

    public void AddStep()
    {
        if (!hasGiven)
        {
            hasGiven = true;
            tManager.DisplayNotification(tutorialStep);
        }
    }
}
