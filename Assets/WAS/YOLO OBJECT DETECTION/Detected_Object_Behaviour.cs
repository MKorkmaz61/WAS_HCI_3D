﻿using UnityEngine;
using System.Collections;

public class Detected_Object_Behaviour : MonoBehaviour
{
    private IEnumerator guiedeness_coroutine              ;
    private bool        is_user_detect            = false ;
    private Vector3     initial_point                     ;
    private float       object_height_sink        = 0.1f  ;
    private Vector3     target_object_point               ;
    private bool        transition_completed_flag = false ;

    // Use this for initialization
    private void Start()
    {
        initial_point       = gameObject.transform.position;
        target_object_point = new Vector3(initial_point.x, initial_point.y - 3f, initial_point.z);
    }

    // Update is called once per frame
    private void Update()
    {
        // alert user
        Take_Attention_User();

    }

    private void Target_Detector_Process()
    {


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
}