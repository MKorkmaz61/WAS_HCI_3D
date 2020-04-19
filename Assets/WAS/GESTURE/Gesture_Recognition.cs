using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using System;

public class Gesture_Recognition : MonoBehaviour
{
    // Accessing class' member thread safe, we use singleton pattern.

    private static object sync_root_object = new object();

    private static Gesture_Recognition instance;

    public static Gesture_Recognition Instance
    {
        get
        {
            lock (sync_root_object)
            {

                if (instance == null)
                {
                    instance = new Gesture_Recognition();
                }
                return instance;
            }
        }
    }

    //Hand Controller
    private Controller leap_motion_controller = new Controller();

    //Leap motion frame
    private Frame leap_motion_frame;

    // Current gesture type from enum
    public Gesture_Modes current_gesture_mode = Gesture_Modes.INVALID_TYPE;

    #region TYPE SWIPE VARIABLES
    public bool TYPE_SWIPE_ACTIVE = false;

    #endregion

    #region TYPE CIRCLE VARIABLES
    public bool                  TYPE_CIRCLE_ACTIVE = false ;
    public Circle_Direction_Mode CIRCLE_DIR_MODE            ;
    public double                RADIUS                     ;
    public Vector                CENTER_POINT               ;
    #endregion

    #region TYPE SCREEN TAP VARIABLES
    public bool                  TYPE_SCREEN_TAP_ACTIVE = false;
    #endregion

    #region TYPE TAP KEY VARIABLES
    public bool                  TYPE_KEY_TAP_ACTIVE    = false;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // Enable all gesture activity.
        leap_motion_controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
        leap_motion_controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
        leap_motion_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
        leap_motion_controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        leap_motion_controller.EnableGesture(Gesture.GestureType.TYPEINVALID);

        // Note : after the enabling all gesture types we have to check these on update loop.
    }

    // Update is called once per frame
    private void Update()
    {
        // Getting current frame
        leap_motion_frame = leap_motion_controller.Frame();

        // Getting gesture
        GestureList gesture_list = leap_motion_frame.Gestures();

        // Determine the reached gesture types

        foreach (var gesture in gesture_list)
        {
            switch (gesture.Type)
            {
                case Gesture.GestureType.TYPESWIPE:
                    {
                        // IF gesture type is swipe

                        SwipeGesture swipe_gesture    = new SwipeGesture(gesture);
                        Vector       swipe_direction  = swipe_gesture.Direction;

                        // Controlling x axis
                        if (swipe_direction.x > 0)
                        {
                            // Swipe right
                            current_gesture_mode = Gesture_Modes.HAND_SWIPE_RIGHT;
                        }
                        else if (swipe_direction.x < 0)
                        {
                            // Swipe left
                            current_gesture_mode = Gesture_Modes.HAND_SWIPE_LEFT;
                        }
                        else
                        {
                            // Does not matter
                        }

                        // Controlling y axis
                        if (swipe_direction.y > 0)
                        {
                            // Swipe up
                            current_gesture_mode = Gesture_Modes.HAND_SWIPE_UP;
                        }
                        else if (swipe_direction.y < 0)
                        {
                            // Swipe down
                            current_gesture_mode = Gesture_Modes.HAND_SWIPE_DOWN;
                        }
                        else
                        {
                            // Does not matter
                        }

                        // type swipe is gonna be actve
                        TYPE_SWIPE_ACTIVE = true;

                        break;
                    }

                case Gesture.GestureType.TYPE_INVALID:
                    {
                        // Assigning invalid
                        current_gesture_mode = Gesture_Modes.INVALID_TYPE;

                        break;
                    }
                case Gesture.GestureType.TYPE_CIRCLE:
                    {
                        CircleGesture circle_gesture = new CircleGesture(gesture);

                        // Crucial parameters such as radius, speed, turn type
                        RADIUS       = circle_gesture.Radius;
                        CENTER_POINT = circle_gesture.Center;

                        // Circle dir.
                        if (circle_gesture.Pointable.Direction.AngleTo(circle_gesture.Normal) <= Math.PI / 2)
                        {
                            CIRCLE_DIR_MODE = Circle_Direction_Mode.CLOCKWISE;
                        }
                        else
                        {
                            CIRCLE_DIR_MODE = Circle_Direction_Mode.COUNTERCLOCKWISE;
                        }

                        // Type circle flag is gonna be active
                        TYPE_CIRCLE_ACTIVE = true;

                        // current catched gesture is gonna be circle
                        current_gesture_mode = Gesture_Modes.HAND_CIRCLE;

                        break;
                    }
                case Gesture.GestureType.TYPE_SCREEN_TAP:
                    {
                        ScreenTapGesture screen_tap_gesture = new ScreenTapGesture(gesture);

                        // current catched gesture is gonna be screen tap
                        current_gesture_mode = Gesture_Modes.HAND_SCREEN_KEY_TAP;

                        // Tyoe screen tap flag is gonna be active
                        TYPE_SCREEN_TAP_ACTIVE = true;

                        break;
                    }
                case Gesture.GestureType.TYPE_KEY_TAP:
                    {
                        KeyTapGesture key_tap_gesture = new KeyTapGesture();

                        // current catched gesture is gonna be key tap
                        current_gesture_mode = Gesture_Modes.HAND_KEY_TAP;

                        // Tyoe key tap flag is gonna be active
                        TYPE_KEY_TAP_ACTIVE = true;

                        break;
                    }
            }


        }

    }
}
