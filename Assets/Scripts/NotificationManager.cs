using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public bool isNotificationOn = true;
    public GameObject notificationButtonOff;
    public GameObject notificationButtonOn;
    public Sprite spriteGray, spriteGreen, spriteRed;

    private void Start()
    {
        isNotificationOn = PlayerPrefs.GetInt("isNotificationOn") == 1;
        ChangeColor();
    }

    public void ChangeColor()
    {
        if (isNotificationOn)
        {
            notificationButtonOff.GetComponent<Image>().sprite = spriteGray;
            notificationButtonOn.GetComponent<Image>().sprite = spriteGreen;
        }
        else
        {
            notificationButtonOff.GetComponent<Image>().sprite = spriteRed;
            notificationButtonOn.GetComponent<Image>().sprite = spriteGray;
        }
    }

    public void OnNotificationButtonOff()
    {
        isNotificationOn = false;
        PlayerPrefs.SetInt("isNotificationOn", isNotificationOn ? 1 : 0);
        ChangeColor();
    }

    public void OnNotificationButtonOn()
    {
        isNotificationOn = true;
        PlayerPrefs.SetInt("isNotificationOn", isNotificationOn ? 1 : 0);
        ChangeColor();
    }
}