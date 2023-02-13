using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEntryTrigger : MonoBehaviour
{
    public bool distributed;
    public GameObject tutorialStep;
    public TutorialManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !distributed)
        {
            distributed = true;
            manager.DisplayNotification(tutorialStep);
        }
    }
}
