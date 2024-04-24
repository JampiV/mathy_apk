using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeNotificationsController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotificationsController androidNotificationsController;
    [SerializeField]
    private IosNotificationController iosNotificationsController; // Corregido aqu�

    private void Start()
    {
#if UNITY_ANDROID
            androidNotificationsController.RequestAuthorization();
            androidNotificationsController.RegisterNotificationChannel();
            androidNotificationsController.SendNotification("�Hora de practicar!",
            "Resuelve la evaluaci�n matem�tica y mejora tus habilidades con Mathy.", 3);
#elif UNITY_IOS
            StartCoroutine(iosNotificationsController.RequestAuthorization());
            iosNotificationsController.SendNotification("�Hora de practicar!",
            "Resuelve la evaluaci�n matem�tica y mejora tus habilidades con Mathy.", 3);
#endif
    }
}