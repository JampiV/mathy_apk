using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
using UnityEngine.ios;
#endif

public class IosNotificationController : MonoBehaviour
{
#if UNITY_IOS
    public IEnumerator RequestAuthorization()
    {
        using var req = new AuthorizationRequest (
            (AuthorizationOption.Alert | AuthorizationOption.Badge,
            true);
        while (!req.IsFinished)
        {
            yield return null;
        }
            
    }

    public void SendNotification(string title, string body, string subtitle, int fireTimeInSeconds)
    {
        var TimeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new System.TimeSpan(0, 0, fireTimeInSeconds),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            Identifier = "Hola_mundo_notification",
            Title = title,
            Body = body,
            Subtitle = subtitle,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "default_category",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);
        }
#endif
}
