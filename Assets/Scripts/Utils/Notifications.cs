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

        var notification = new iOSNotification()
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

        iOSNotificationCenter.ScheduleNotification(notification);
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
