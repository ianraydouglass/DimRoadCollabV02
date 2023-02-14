using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialListenTrigger : MonoBehaviour
{
    public bool distributed;
    public GameObject tutorialStep;
    public TutorialManager manager; 

    void Start()
    {
        if (!manager)
        {
            GameObject tutorialPanel = GameObject.Find("Tutorialization Panel");
            manager = tutorialPanel.GetComponent<TutorialManager>();
        }
    }

    public void TutorialTrigger()
    {
        if (!distributed)
        {
            distributed = true;
            manager.DisplayNotification(tutorialStep);
        }
    }
}
