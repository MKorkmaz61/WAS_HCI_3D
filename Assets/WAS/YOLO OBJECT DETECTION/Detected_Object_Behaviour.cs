using UnityEngine;
using System.Collections;

public class Detected_Object_Behaviour : MonoBehaviour
{
    private IEnumerator             guiedeness_coroutine              ;
    private bool                    is_user_detect            = false ;
    private Vector3                 initial_point                     ;
    private float                   object_height_sink        = 0.1f  ;
    private Vector3                 target_object_point               ;
    private bool                    transition_completed_flag = false ;
    public  float                   mouse_object_distance     = 0f    ;

    // detect double click 
    private int                     click_count               = 0     ;
    private float                   neccessary_clickable_time = 2f    ;
    private Surveillance_Process    surveillance_process_handle       ;

    // Use this for initialization
    private void Start()
    {
        initial_point               = gameObject.transform.position;
        target_object_point         = new Vector3(initial_point.x, initial_point.y - 3f, initial_point.z);
        surveillance_process_handle = GameObject.Find("Script_Manager").GetComponent<Surveillance_Process>();
    }

    // Update is called once per frame
    private void Update()
    {
        // alert user
        Take_Attention_User();

        // Target detector
        Target_Detector_Process();

    }

    private void Target_Detector_Process()
    {
        Vector3 mouse_world_position  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_object_distance         = Vector3.Distance(mouse_world_position, gameObject.transform.position);

        if (mouse_object_distance < 3f       && Input.GetMouseButtonDown(0) == true)
        {
        	click_count++;
        }
        else 
        {
            // start reset timer
            neccessary_clickable_time -= Time.deltaTime;

            if (neccessary_clickable_time <= 0)
            {
                click_count = 0;
                neccessary_clickable_time = 2f;
            }
        }

       	// check double click
        if(click_count >= 2)
        {
        	Open_Target_Determination_Panel();
        	click_count = 0;
        }
    }

    private void Take_Attention_User()
    {
        if (transition_completed_flag == false)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target_object_point, 0.1f);

            if (Vector3.Distance(gameObject.transform.position, target_object_point) <= 0.2f)
            {
                transition_completed_flag = true;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, initial_point, 0.1f);

            if (Vector3.Distance(gameObject.transform.position, initial_point) <= 0.2f)
            {
                transition_completed_flag = false;
            }
        }

    }

    private void Open_Target_Determination_Panel()
    {
        surveillance_process_handle.dynamic_AI_object = gameObject;
        surveillance_process_handle.UI_process_handle.AI_support_panel.SetActive(true);
    }
}
