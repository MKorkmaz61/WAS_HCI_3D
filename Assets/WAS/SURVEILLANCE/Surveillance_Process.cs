using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surveillance_Process : MonoBehaviour
{
    public  GameObject [] target_objects                        ;
    public  int           current_target_index                  ;
    private bool          target_transition_is_active  = false  ;
    private Camera        main_camera                           ;
    private Vector3       next_target_position                  ;
    private UI_Process    UI_process_handle                     ;
 
    // Start is called before the first frame update
    void Start()
    {
        // getting camera
        main_camera = Camera.main;

        // assign -1 to current index at initial time due to pass 0.
        current_target_index = -1;

        // get all target objects.
        target_objects = GameObject.FindGameObjectsWithTag("target");

        // get ui handle
        UI_process_handle = gameObject.GetComponent<UI_Process>();

        // set target counter
        UI_process_handle.Set_Target_Counter(target_objects.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (target_transition_is_active == true)
        {
            main_camera.transform.position = Vector3.Lerp(main_camera.transform.position, next_target_position, 0.1f);
        }
    }

    private IEnumerator Reset_Coroutine()
    {
        yield return new WaitForSeconds(1);

        target_transition_is_active = false;
    }

    public void Set_Transition_Target(Target_Discovery_Type target_discovery_type)
    {
        switch (target_discovery_type)
        {
            case Target_Discovery_Type.FORWARD:
                {
                    if (current_target_index < target_objects.Length - 1 && target_transition_is_active == false)
                    {
                        current_target_index++;

                        // x and y axis will come from target objects, bu z axis is only belong to cameras' z axis.
                        next_target_position = new Vector3(target_objects[current_target_index].transform.position.x,
                                                           target_objects[current_target_index].transform.position.y,
                                                           main_camera.transform.position.z);

                        target_transition_is_active = true;

                    }
                    break;
                }
            case Target_Discovery_Type.BACKWARD:
                {
                    if (current_target_index > 0 && target_transition_is_active == false)
                    {
                        current_target_index --;

                        // x and y axis will come from target objects, bu z axis is only belong to cameras' z axis.
                        next_target_position = new Vector3(target_objects[current_target_index].transform.position.x,
                                                           target_objects[current_target_index].transform.position.y,
                                                           main_camera.transform.position.z);

                        target_transition_is_active = true;

                    }

                    break;
                }
            default:
                {
                    break;
                }

        }

        StartCoroutine(Reset_Coroutine());
    }
}
