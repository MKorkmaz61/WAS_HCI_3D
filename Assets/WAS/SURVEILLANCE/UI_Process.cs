using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Process : MonoBehaviour
{
    public  Image               left_hand_detected_image;
    public  Image               right_hand_detected_image;
    public  Text                clock_type_text;
    public  Gesture_Recognition gesture_recognition;
    private bool                right_hand_detected = false;
    private bool                left_hand_detected = false;


    // Start is called before the first frame update
    private void Start()
    {
        // Get component for hand detection.
        gesture_recognition = gameObject.GetComponent<Gesture_Recognition>();
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
            left_hand_detected = false;

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
}
