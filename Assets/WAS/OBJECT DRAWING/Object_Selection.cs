using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Object_Color
{
    GREEN,
    RED,
    YELLOW
}

public class Object_Selection : MonoBehaviour
{

    public float         object_index_finger_distance = 0f;
    public Object_Color  curren_object_color          = Object_Color.RED;
    public LineRenderer  target_line_renderer         = null;

    // Update is called once per frame
    void Update()
    {
        GameObject index_finger_object = GameObject.Find("Bip01 R Finger12");

        if (index_finger_object != null)
        {
            object_index_finger_distance = Vector3.Distance(index_finger_object.transform.position, gameObject.transform.position);
        }
        else
        {
            object_index_finger_distance = 100;
        }

        if (object_index_finger_distance <= 1.5f && target_line_renderer != null)
        {
            switch (curren_object_color)
            {
                case Object_Color.GREEN:
                    {
                        target_line_renderer.material.color = Color.green;
                        
                        break;
                    }
                case Object_Color.RED:
                    {
                        target_line_renderer.material.color = Color.red;

                        break;
                    }
                case Object_Color.YELLOW:
                    {
                        target_line_renderer.material.color = Color.yellow;

                        break;
                    }
            }

        }
    }
}
