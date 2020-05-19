using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Touch_Process : MonoBehaviour
{
    // move camera to make surveillance
    public Camera  main_camera                          ;
    public Vector3 first_click_mouse_position           ;
    public Vector3 current_mouse_position               ;
    public bool    first_click_scene            = false ;

    // Inital camera pos.
    public Vector3 initial_camera_position              ;
    public float   initial_camera_zoom_scale            ;

    // Reset camera
    public bool    reset_is_active              = false ;


    // Start is called before the first frame update
    private void Start()
    {
        main_camera               = Camera.main;
        initial_camera_position   = main_camera.transform.position;
        initial_camera_zoom_scale = main_camera.orthographicSize;
    }

    // Update is called once per frame
    private void Update()
    {
        // Drag, move detector.
        Drag_Move_Detector_Process();

        //Zoom in,out detector process
        Zoom_Detector_Process();

        if (reset_is_active == true)
        {
            main_camera.transform.position = Vector3.Lerp(main_camera.transform.position, initial_camera_position, 0.1f);

            if (main_camera.transform.position == initial_camera_position)
            {
                reset_is_active = false;
            }
        }
    }

    private void Drag_Move_Detector_Process()
    {
        // Detect touch and click
        if (Input.GetMouseButton(0) == true)
        {
            if (first_click_scene == true)
            {
                // move camera
                current_mouse_position = Input.mousePosition;

                // subtraction two variables, getting x axis
                float drag_to_move_x_axis = (first_click_mouse_position - current_mouse_position).x / 50f;
                float drag_to_move_y_axis = (first_click_mouse_position - current_mouse_position).y / 50f;

                main_camera.transform.position = new Vector3(main_camera.transform.position.x + drag_to_move_x_axis,
                                                             main_camera.transform.position.y + drag_to_move_y_axis,
                                                             main_camera.transform.position.z);

                first_click_mouse_position = current_mouse_position;

            }
            else
            {
                first_click_scene = true;
                first_click_mouse_position = Input.mousePosition;
            }
        }

        // checlk mouse or touch released
        if (Input.GetMouseButtonUp(0))
        {
            first_click_scene = false;
        }

        // right click detector
        if (Input.GetMouseButton(1) == true)
        {
            reset_is_active = true;
        }

        // middle click detector
        if (Input.GetMouseButton(2))
        {
            main_camera.orthographicSize = initial_camera_zoom_scale;
        }
    }

    private void Zoom_Detector_Process()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward wheel
        {
            main_camera.orthographicSize += 0.5f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backward wheel
        {
            main_camera.orthographicSize -= 0.5f;
        }
    }
}
