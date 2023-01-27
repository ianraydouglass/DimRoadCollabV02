using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftConfirmation : MonoBehaviour
{
    //public TextMeshProUGUI textBox;
    public GameObject confirmationText;

    void Start()
    {
        
    }

    public void ShowConfirmation()
    {
        confirmationText.SetActive(true);
        StartCoroutine("HideTimer");
    }

    public void HideConfirmation()
    {
        confirmationText.SetActive(false);
    }

    IEnumerator HideTimer()
    {
        yield return new WaitForSeconds(3);
        HideConfirmation();
    }
}
