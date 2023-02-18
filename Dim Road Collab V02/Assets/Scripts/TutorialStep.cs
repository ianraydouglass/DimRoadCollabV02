using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialStep : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public Image image;
    public Animator animator;
    public bool isReminder;
    public float awakeTime = 3f;
    public GameObject stepToAddOnComplete;
    [Space(10)]
    public TutorialManager tManager;


    //method for playing fade-out animation via canvas group

    void Start()
    {
        if(isReminder)
        {
            StartCoroutine("DestroyTimer");
        }
    }

    public void Complete()
    {
        if(stepToAddOnComplete != null && tManager != null)
        {
            tManager.DisplayNotification(stepToAddOnComplete);
        }
        animator.Play("MessageOut");
    }

    public void RemoveThis()
    {
        Destroy(gameObject);
    }


    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(awakeTime);
        Complete();
    }
}
