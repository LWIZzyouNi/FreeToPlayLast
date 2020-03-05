using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;


namespace Assets.SimpleAndroidNotifications
{
    public class MobileNotificationsManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CreateTemplateNotif();
            SendNotif();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void SendNotif()
        {
            var notification = new AndroidNotification();
            notification.Title = "SomeTitle";
            notification.Text = "SomeText";
            notification.FireTime = System.DateTime.Now.AddSeconds(3f);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
            Debug.Log("la notif est ok");
        }

        public void CreateTemplateNotif()
        {
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);
            Debug.Log("la gen est ok");
        }
    }
}

