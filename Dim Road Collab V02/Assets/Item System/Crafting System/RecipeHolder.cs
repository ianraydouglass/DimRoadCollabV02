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
        buttonLeft.SetActive(false);
        buttonRight.SetActive(false);
    }

    public void DefaultSetup()
    {
        buttonRight.SetActive(true);
        buttonLeft.SetActive(false);
    }

    public void MaxLeft()
    {
        buttonRight.SetActive(true);
        buttonLeft.SetActive(false);
    }

    public void MaxRight()
    {
        buttonRight.SetActive(false);
        buttonLeft.SetActive(true);
    }

    public void MidRange()
    {
        buttonRight.SetActive(true);
        buttonLeft.SetActive(true);
    }
}
