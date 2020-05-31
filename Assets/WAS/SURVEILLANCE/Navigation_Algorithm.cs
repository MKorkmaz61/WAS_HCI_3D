using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_Algorithm : MonoBehaviour
{
    public bool          nav_path_algorithm_is_active = false;
    public UI_Process    UI_process_handle;
    public GameObject    root_prefab;
    public List<Vector3> path_position_list           = new List<Vector3>();

    // Start is called before the first frame update
    private void Start()
    {
        // get UI process
        UI_process_handle = gameObject.GetComponent<UI_Process>();
    }

    // Update is called once per frame
    private void Update()
    {
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

            // clear list
            path_position_list.Clear();
        }       
    }

}
