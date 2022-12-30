using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public GameObject notification;
    public GameObject notificationPanel;

    public void DisplayNotification(Sprite sprite, string text)
    {
        GameObject n = Instantiate(notification);
        n.transform.parent = notificationPanel.transform;
        Notification notif = n.GetComponent<Notification>();
        notif.textBox.text = text;
        notif.image.sprite = sprite;
    }

}
