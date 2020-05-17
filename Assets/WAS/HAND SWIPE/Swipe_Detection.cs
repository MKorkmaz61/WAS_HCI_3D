using UnityEngine;
using System.Collections;
using Leap;

public class Swipe_Detection : MonoBehaviour
{
    public Controller   leap_controller      ;
    public Vector       direction            ;
    public GameObject   viewer               ;

    public string horizontal             = "";
    public string vertical               = "";

    // Use this for initialization
    void Start()
    {
        leap_controller = new Controller();
        leap_controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
    }

    // Update is called once per frame
    void Update()
    {
        Frame current_frame = leap_controller.Frame();
        GestureList gestures = current_frame.Gestures();

        foreach (var gesture in gestures)
        {
            if (gesture.Type == Gesture.GestureType.TYPESWIPE)
            {
                SwipeGesture swipe_gesture = new SwipeGesture(gesture);

                direction = swipe_gesture.Direction;

                // X axis
                if (direction.x < 0)
                {
                    viewer.transform.position = new Vector3(viewer.transform.position.x - .1f , viewer.transform.position.y, viewer.transform.position.z);
                }
                else
                {
                    viewer.transform.position = new Vector3(viewer.transform.position.x + .1f, viewer.transform.position.y, viewer.transform.position.z);
                }

                // Y Axis
                if (direction.y < 0)
                {
                    viewer.transform.position = new Vector3(viewer.transform.position.x, viewer.transform.position.y - .1f, viewer.transform.position.z);
                }
                else
                {
                    viewer.transform.position = new Vector3(viewer.transform.position.x, viewer.transform.position.y + .1f, viewer.transform.position.z);
                }
            }
        }

    }
}
