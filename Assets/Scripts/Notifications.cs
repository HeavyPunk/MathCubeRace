using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private void Awake()
    {
        AndroidNotificationChannel channelRecords = new AndroidNotificationChannel()
        {
            Name = "Рекорды",
            Description = "Уведомления о новых рекордах",
            Id = "records",
            Importance = Importance.High
        };
        AndroidNotificationChannel channelNews = new AndroidNotificationChannel()
        {
            Name = "Новости",
            Description = "Уведомления о долгом отсутствии в игре",
            Id = "news",
            Importance = Importance.High
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(channelRecords);
        AndroidNotificationCenter.RegisterNotificationChannel(channelNews);
    }

    public static void SendNotificationRecord()
    {
        string text;

        if (PlayerPrefs.GetInt("isNotificationOn") == 1)
        {
            if (RateTableData.playerPosition >= 0)
            {
                text = "Вы на " + (RateTableData.playerPosition + 1) + " месте, зайдите в игру и займите самые лучшие места в таблице рекордов!";
                AndroidNotification notificationRecord = new AndroidNotification()
                {
                    Title = "Ваш рекорд побит",
                    Text = text,
                    FireTime = DateTime.Parse("18:00:00"),
                    Style = NotificationStyle.BigTextStyle,
                };

                AndroidNotificationCenter.SendNotification(notificationRecord, "records");
            }
        }
    }
    
    public static void SendNotificationNews()
    {
        if (PlayerPrefs.GetInt("isNotificationOn") == 1)
        {
            AndroidNotification notificationNews = new AndroidNotification()
            {
                Title = "Вы долго не заходили в игру",
                Text = "Заходите и улучшайте свои математические навыки прямо сейчас!",
                FireTime = DateTime.Parse("17:00:00"),
                Style = NotificationStyle.BigTextStyle,
            };

            AndroidNotificationCenter.SendNotification(notificationNews, "news");
        }
    }
}
