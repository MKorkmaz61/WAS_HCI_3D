using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Selection : MonoBehaviour
{

    public bool                object_selection_mode = false ;
    public GameObject          selected_object       = null  ;
    public Vector3             inital_position               ;
    public Gesture_Recognition current_gesture_recognition   ;

    //!< Distance calculations from object
    public GameObject red_object;

    public float red_index_finger_distance;

    // Start is called before the first frame update
    void Start()
    {
        current_gesture_recognition = gameObject.GetComponent<Gesture_Recognition>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        red_index_finger_distance    = Vector3.Distance(red_object.transform.position, current_gesture_recognition.INDEX_FINGER_POS);

        if (red_index_finger_distance <= 2f)
        {
            object_selection_mode = true;
            selected_object       = red_object;
        }

        if (object_selection_mode == true && selected_object != null)
        {
            selected_object.transform.position = current_gesture_recognition.INDEX_FINGER_POS;
        }
    }

    public void Draw_Sphere_From_Selected_Object()
    {
        object_selection_mode              = false;
        selected_object.transform.position = inital_position;



    }
}
