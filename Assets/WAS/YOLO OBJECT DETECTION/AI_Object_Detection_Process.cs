using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Object_Detection_Process : MonoBehaviour
{
    public GameObject[] detected_objects;

    // Start is called before the first frame update
    void Start()
    {
        detected_objects = GameObject.FindGameObjectsWithTag("AI_Object");

        // draw a line
        foreach (var detected_object in detected_objects)
        {
            GameObject line_object = new GameObject();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
