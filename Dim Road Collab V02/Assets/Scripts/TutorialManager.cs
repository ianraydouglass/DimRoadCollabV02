using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    
    public GameObject tutorialPanel;

    public void DisplayNotification(GameObject tutorialStep)
    {
        GameObject n = Instantiate(tutorialStep, tutorialPanel.transform);
       
    }
}
