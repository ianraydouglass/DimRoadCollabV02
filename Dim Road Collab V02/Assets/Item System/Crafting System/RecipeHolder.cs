using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeHolder : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI titleText;
    public GameObject buttonLeft;
    public GameObject buttonRight;

    public void NoButtons()
    {
        buttonLeft.GetComponent<Button>().interactable = false;
        buttonRight.GetComponent<Button>().interactable = false;
    }

    public void DefaultSetup()
    {
        buttonRight.GetComponent<Button>().interactable = true;
        buttonLeft.GetComponent<Button>().interactable = false;
    }

    public void MaxLeft()
    {
        buttonRight.GetComponent<Button>().interactable = true;
        buttonLeft.GetComponent<Button>().interactable = false;
    }

    public void MaxRight()
    {
        buttonRight.GetComponent<Button>().interactable = false;
        buttonLeft.GetComponent<Button>().interactable = true;
    }

    public void MidRange()
    {
        buttonRight.GetComponent<Button>().interactable = true;
        buttonLeft.GetComponent<Button>().interactable = true;
    }
}
