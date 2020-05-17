using System;
using UnityEngine;
using UnityEngine.UI;

public class Gesture_Notification
{
    public Notifications notification_item;
    public Sprite        notification_image_sprite;
    public String        notification_text;

}

public enum Notifications
{
    HAND_SWIPE_LEFT,
    HAND_SWIPE_RIGHT
}
