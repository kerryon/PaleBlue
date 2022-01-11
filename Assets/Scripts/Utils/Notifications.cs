using UnityEngine;
using Unity.Notifications.iOS;
using System.Collections;
using System;

public class Notifications : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RequestAuthorization());

        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(12, 0, 0),
            Repeats = false
        };

        var calendarTriggerMid = new iOSNotificationCalendarTrigger()
        {
            Day = 4,
            Hour = 18,
            Minute = 0,
            Repeats = false
        };

        var calendarTriggerEnd = new iOSNotificationCalendarTrigger()
        {
            Day = 7,
            Hour = 23,
            Minute = 59,
            Repeats = false
        };


        var notificationInterval = new iOSNotification()
        {
            Identifier = "_notification_01",
            Title = "Es tut sich was ...",
            Body = "Sieh nach, wie es deinem Planeten geht.",
            //Subtitle = "Es passiert etwas ...",
            ShowInForeground = false,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        var notificationMid = new iOSNotification()
        {
            Identifier = "_notification_02",
            Title = "Es tut sich was ...",
            Body = "Die hälfte der Spielzeit ist um, jede deiner Entscheidungen zählt.",
            //Subtitle = "Es passiert etwas ...",
            ShowInForeground = false,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = calendarTriggerMid,
        };

        var notificationEnd = new iOSNotification()
        {
            Identifier = "_notification_03",
            Title = "Die Zeit ist um.",
            Body = "Du hast es geschafft. Sieh dir dein Ergebnis an.",
            //Subtitle = "Es passiert etwas ...",
            ShowInForeground = false,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = calendarTriggerEnd,
        };

        iOSNotificationCenter.ScheduleNotification(notificationInterval);
        iOSNotificationCenter.ScheduleNotification(notificationMid);
        iOSNotificationCenter.ScheduleNotification(notificationEnd);
    }

    IEnumerator RequestAuthorization()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
    }
}
