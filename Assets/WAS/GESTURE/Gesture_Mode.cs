using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Gesture_Modes
{
    HAND_SWIPE_LEFT,
    HAND_SWIPE_RIGHT,
    HAND_SWIPE_UP,
    HAND_SWIPE_DOWN,
    HAND_CIRCLE,
    HAND_KEY_TAP,
    HAND_SCREEN_KEY_TAP,
    INVALID_TYPE
}

public enum Swipe_Modes
{
    SWIPE_LEFT,
    SWIPE_RIGHT,
    SWIPE_UP,
    SWIPE_DOWN,
    SWIPE_INVALID
}

public enum Circle_Direction_Mode
{
    CLOCKWISE,
    COUNTERCLOCKWISE
}

public class Gesture_Mode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
