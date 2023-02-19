using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public GameObject buttonGroup;

    public void EnableButtons()
    {
        buttonGroup.SetActive(true);
    }
}
