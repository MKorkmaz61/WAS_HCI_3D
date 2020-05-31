using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigation_Algorithm : MonoBehaviour
{
    public bool          nav_path_algorithm_is_active = false;
    public UI_Process    UI_process_handle;
    public GameObject    root_prefab;
    public List<Vector3> path_position_list           = new List<Vector3>();
    public Camera        main_camera;
    public List<Vector3> wandering_path_list;
    public int           current_wandering_station    = 0;
    public bool          wandering_mode_is_active     = false;
    public bool          transition_is_continuing     = true;
    public Vector3       station_position;

    // Start is called before the first frame update
    private void Start()
    {
        // get UI process
        UI_process_handle = gameObject.GetComponent<UI_Process>();

        // set the cam
        main_camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        // check keyboard A pressed
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            if (nav_path_algorithm_is_active == false)
            {
                nav_path_algorithm_is_active = true;
                UI_process_handle.Start_External_Notification_Panel("Dyn. Nav. Algorithm Active");
            }
            else
            {
                nav_path_algorithm_is_active = false;
                UI_process_handle.Start_External_Notification_Panel("Dyn. Nav. Algorithm Passive");
            }
        }

        // check dynamic wandering is active
        if (Input.GetKeyDown(KeyCode.W) && nav_path_algorithm_is_active == true)
        {
            if (wandering_mode_is_active == false)
            {
                wandering_mode_is_active = true;
                station_position = wandering_path_list[0];
                UI_process_handle.Start_External_Notification_Panel("Dynamic Wandering Started");
            }
            else
            {
                wandering_mode_is_active = false;
                UI_process_handle.Start_External_Notification_Panel("Dynamic Wandering Ended");
            }
        }

        if (nav_path_algorithm_is_active == true && Input.GetMouseButtonDown(0) == true)
        {
            Vector3 path_position  = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject path_object = Instantiate(root_prefab, new Vector3(path_position.x, path_position.y, 5f), Quaternion.identity);

            path_object.name       = "Dynamic path object";

            path_position_list.Add(path_object.transform.position);
        }

        if (path_position_list.Count == 4)
        {
            GameObject   path_line_object = new GameObject();
            LineRenderer line             = path_line_object.AddComponent<LineRenderer>();
            line.material.color           = Color.cyan;
            line.positionCount            = path_position_list.Count + 1;
            line.widthMultiplier          = 0.3f;
            line.SetPositions(path_position_list.ToArray());
            line.SetPosition(path_position_list.Count, path_position_list[0]);
            line.generateLightingData     = true;

            // clear list but before assing to cam path list
            wandering_path_list = new List<Vector3>();
 
            foreach (var item in path_position_list)
            {
                wandering_path_list.Add(new Vector3(item.x, item.y, main_camera.transform.position.z));
            }

            path_position_list.Clear();
        }

        // dynamic wandering
        Dynamic_Wander_Process();
    }

    private void Dynamic_Wander_Process()
    {
        if (wandering_mode_is_active == true)
        {
            main_camera.transform.position = Vector3.Lerp(main_camera.transform.position, station_position, 0.01f);
            main_camera.orthographicSize   = 16;
            if (Vector3.Distance(main_camera.transform.position, station_position) < 1f)
            {
                current_wandering_station++;

                if (current_wandering_station < wandering_path_list.Count)
                {
                    station_position = wandering_path_list[current_wandering_station];
                }
                else
                {
                    wandering_mode_is_active = false;
                    UI_process_handle.Start_External_Notification_Panel("Dynamic Wandering Ended");
                }
            }
        }

    }

}
