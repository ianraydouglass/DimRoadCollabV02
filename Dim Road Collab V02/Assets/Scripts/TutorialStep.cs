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
