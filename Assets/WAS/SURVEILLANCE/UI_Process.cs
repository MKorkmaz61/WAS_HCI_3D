﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Process : MonoBehaviour
{
    public  Image                       left_hand_detected_image;
    public  Image                       right_hand_detected_image;
    public  Text                        clock_type_text;
    public  Gesture_Recognition         gesture_recognition;
    private bool                        right_hand_detected = false;
    private bool                        left_hand_detected = false;

    // Gesture notification panel
    public  GameObject                  gesture_notification_panel_root_object;
    public  Image                       gesture_notification_image;
    public  Text                        gesture_notification_text;
    private bool                        gesture_recognition_panel_is_active = false;
    private IEnumerator                 gesture_recognition_panel_coroutine;

    // Notification Queue
    private Queue<Gesture_Notification> gesture_notification_panel_queue;

    // Start is called before the first frame update
    private void Start()
    {
        // Get component for hand detection.
        gesture_recognition = gameObject.GetComponent<Gesture_Recognition>();

        // memory alloc for queue
        gesture_notification_panel_queue = new Queue<Gesture_Notification>();

        // set active is going to be false
        gesture_notification_panel_root_object.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        try
        {
            // Update clock
            clock_type_text.text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

            // to find detected hands, all boolean vars are going to be false.
            right_hand_detected = false;
            left_hand_detected  = false;

            // Update Hand detection icon
            // firstly, get all detected hands
            var hands = gesture_recognition.leap_motion_frame.Hands;
            foreach (var hand in hands)
            {
                if (hand.IsLeft == true)
                {
                    left_hand_detected = true;
                }
                else if (hand.IsRight)
                {
                    right_hand_detected = true;
                }
            }

            // Update images' mesh.
            Set_Image_Visibility_For_Hands(right_hand_detected_image, right_hand_detected);
            Set_Image_Visibility_For_Hands(left_hand_detected_image, left_hand_detected);

            // Coroutine process
            if (gesture_notification_panel_queue.Count > 0 && gesture_recognition_panel_is_active == false)
            {
                Gesture_Notification target_notification = gesture_notification_panel_queue.Dequeue();
                gesture_recognition_panel_coroutine      = Notification_Panel_Coroutine(target_notification);

                StartCoroutine(gesture_recognition_panel_coroutine);
            }

        }
        catch (Exception ex)
        {
            Debug.Log("Exception occured. " + ex.Message);
        }
    }

    private void Set_Image_Visibility_For_Hands(Image hand_image, bool visibility)
    {
        hand_image.enabled = visibility;
    }

    public void Start_Gesture_Notification_Panel(Notifications notification_item)
    {
        Gesture_Notification gesture_notification = new Gesture_Notification();

        switch (notification_item)
        {
            case Notifications.HAND_SWIPE_LEFT:
                {
                    gesture_notification.notification_image_sprite = Resources.Load<Sprite>("IMAGES/ICONS/swipe_left");
                    gesture_notification.notification_text         = "Swipe Left";

                    break;
                }
            case Notifications.HAND_SWIPE_RIGHT:
                {
                    gesture_notification.notification_image_sprite = Resources.Load<Sprite>("IMAGES/ICONS/swipe_right");
                    gesture_notification.notification_text         = "Swipe Right";

                    break;
                }
            default:
                {
                    break;
                }
        }

        // check if notification panel is active
        if (gesture_recognition_panel_is_active == false && gesture_notification_panel_queue.Count == 0)
        {
            gesture_recognition_panel_coroutine = Notification_Panel_Coroutine(gesture_notification);

            StartCoroutine(gesture_recognition_panel_coroutine);
        }
        else
        {
            gesture_notification_panel_queue.Enqueue(gesture_notification);
        }
    }

    private IEnumerator Notification_Panel_Coroutine(Gesture_Notification gesture_notification)
    {
        gesture_recognition_panel_is_active     = true;
        gesture_notification_image.sprite       = gesture_notification.notification_image_sprite;
        gesture_notification_text.text          = gesture_notification.notification_text;

        gesture_notification_panel_root_object.SetActive(true);

        // wait a second
        yield return new WaitForSeconds(1f);

        gesture_recognition_panel_is_active = false;
        gesture_notification_panel_root_object.SetActive(false);
    }
}