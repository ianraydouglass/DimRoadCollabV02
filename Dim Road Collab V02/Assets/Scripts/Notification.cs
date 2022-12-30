using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public Image image;

    void Start()
    {
        StartCoroutine("DestroyTimer");
    }

    public void DestroyNotification()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5);
        DestroyNotification();
    }
}
