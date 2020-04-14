using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Drawing_Object : MonoBehaviour {

    private List<Vector3> drawing_positions = new List<Vector3>();
    public GameObject Line_Renderer;

    public float index_middle_distance = 0f;
    public float index_ring_distance = 0f;
    public float index_pinky_distance = 0f;

    public LineRenderer drawn_line_renderer;


	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        GameObject index_object = GameObject.Find("index/bone3");
        GameObject middle_object = GameObject.Find("middle/bone3");
        GameObject pinky_object = GameObject.Find("pinky/bone3");
        GameObject ring_object = GameObject.Find("ring/bone3");

        if(index_object != null)
        {
            if (Drawing_Mode_Active_b == true)
            {
                LineRenderer line = Line_Renderer.GetComponent<LineRenderer>();
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, index_object.transform.position);
            }

            index_middle_distance = Vector3.Distance(index_object.transform.position, middle_object.transform.position);
            index_ring_distance = Vector3.Distance(index_object.transform.position, ring_object.transform.position);
            index_pinky_distance = Vector3.Distance(index_object.transform.position, pinky_object.transform.position);

        }
    }

    private bool Drawing_Mode_Active_b 
    {
        get 
        {
            if(index_middle_distance >=1.1f && index_ring_distance >= 1.1f && index_pinky_distance >= 1.1f )
            {

                return true;
            }
            else 
            {
                return false;
            }

        }
    }

    private void Target_Drawing_Completed()
    {

    }
}
