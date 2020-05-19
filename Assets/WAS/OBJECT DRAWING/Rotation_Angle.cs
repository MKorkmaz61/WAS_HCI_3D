using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
public class Rotation_Angle : MonoBehaviour {

    public GameObject Color_Selection_Object;
    public Object_Selection object_selection;

    public  float [] rotation;

    Vector3 pinkyVector,menuVector;
    public static bool flag;
	// Use this for initialization
	void Start ()
    {
        menuVector       = Color_Selection_Object.transform.position;
        object_selection = gameObject.GetComponent<Object_Selection>();
    }


	
	// Update is called once per frame
	void Update () {


        if (GameObject.Find("Bip01 R Finger057") != null)
        {
            pinkyVector = GameObject.Find("Bip01 R Finger057").transform.position;
        }
        else
        {
            Color_Selection_Object.transform.position = menuVector;
        }

        Frame frame  = Gesture_Recognition.Instance.leap_motion_controller.Frame();
        Hand  left   = frame.Hands.Leftmost;

        if (left.IsLeft)
        {
            rotation = left.Basis.xBasis.ToFloatArray();

            if (rotation[1] <= -0.65f)
            {

                flag = true;
				pinkyVector.x += 1f;
                pinkyVector.y += 1.4f;
                Color_Selection_Object.transform.position = pinkyVector;
            }

            else
            {
                flag = false;
                Color_Selection_Object.transform.position = menuVector;
            }

        }
        
        else
        {
            flag = false;

        }
    }
}
