using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRow : MonoBehaviour
{
    public RectTransform thisTransform;
    public Vector2 originalPosition;
    // Start is called before the first frame update
    void Awake()
    {
        RectTransform thisTransform = GetComponent<RectTransform>();
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    public void RevertPosition()
    {
        thisTransform.anchoredPosition = originalPosition;
    }

}
