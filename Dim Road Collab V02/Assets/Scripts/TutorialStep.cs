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

    //method for playing fade-out animation via canvas group

    public void Complete()
    {
        animator.Play("MessageOut");
    }

    public void RemoveThis()
    {
        Destroy(gameObject);
    }

}
