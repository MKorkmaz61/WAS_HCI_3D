using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Gesture_Process : MonoBehaviour
{

    // Get object from gesture recongition class
    private Gesture_Recognition       gesture_recognition;
    private List<Survaillence_Target> survaillence_targets_list = new List<Survaillence_Target>();
    private const Int32               UPDATE_MACHINE_LOOP_HZ    = 1;
    public  Sprite                    KEY_TARGET_POINT_SPRITE;
    public  Object_Selection          object_selection;

    // UI process
    private UI_Process current_UI_process;

    // Start is called before the first frame update
    private void Start()
    {
        // Get object from singleton class
        gesture_recognition = gameObject.GetComponent<Gesture_Recognition>();
        object_selection    = gameObject.GetComponent<Object_Selection>();
        StartCoroutine(Update_1Hz_Machine());

        // Gest UI process
        current_UI_process = gameObject.GetComponent<UI_Process>();
    }


    private IEnumerator Update_1Hz_Machine()
    {
        while (true)
        {
            Determine_Process_Machine();

            yield return new WaitForSeconds(UPDATE_MACHINE_LOOP_HZ);
        }

    }

    private void Determine_Process_Machine()
    {


        gesture_recognition = gameObject.GetComponent<Gesture_Recognition>();

        #region CIRCLE PROCESS

        // if type circle is active
        if (gesture_recognition.TYPE_CIRCLE_ACTIVE == true && gesture_recognition.CIRCLE_PROGRESS >= 1)

        { 
            // Add target circle into scene
            float     circle_segments     = 50f;
            double    circle_radius       = gesture_recognition.RADIUS;
            Vector3   circle_center_point = new Vector3(gesture_recognition.CENTER_POINT.x,
                                                        gesture_recognition.CENTER_POINT.y,
                                                        gesture_recognition.CENTER_POINT.z);

            // First create empty gameobject
            GameObject circle_root_object         = new GameObject();
            circle_root_object.transform.position = gesture_recognition.CENTER_POINT;

            LineRenderer circle_line_renderer     = circle_root_object.AddComponent<LineRenderer>();

            // Circle points
            float point_x                         ;
            float point_y                         ;
            float point_z = circle_center_point.z ; // getting from hand center point' z

            float angle = 20f;
            circle_line_renderer.positionCount = Convert.ToInt32(circle_segments + 1);

            for (int i = 0; i < (circle_segments + 1); i++)
            {
                point_x = (float)(Mathf.Sin(Mathf.Deg2Rad * angle) * circle_radius);
                point_y = (float)(Mathf.Cos(Mathf.Deg2Rad * angle) * circle_radius);

                circle_line_renderer.SetPosition(i, new Vector3(point_x, point_y, point_z));

                angle += (360f / circle_segments);
            }

            // We don't use world space, use local space
            circle_line_renderer.useWorldSpace   = false;
            circle_line_renderer.widthMultiplier = .1f;

            //store the target
            Survaillence_Target survaillence_target = new Survaillence_Target()
            {
                target_ID                = survaillence_targets_list.Count,
                target_points            = circle_root_object.transform.position,
                target_root_game_objects = circle_root_object,
                target_type              = Target_Type.TARGET_CIRCLE
            };


            survaillence_targets_list.Add(survaillence_target);

            // Push database
            Database_Process.Add_Target_Into_Database(survaillence_target);

            // Release the circle mode after finish.
            gesture_recognition.TYPE_CIRCLE_ACTIVE = false;


        }
        #endregion

        #region KEY TAP PROCESS
        if (gesture_recognition.TYPE_KEY_TAP_ACTIVE == true)
        {
            // KEY TAP PROCESS
            //GameObject      key_target_object       = new GameObject();
            //SpriteRenderer  sprite_renderer         = key_target_object.AddComponent<SpriteRenderer>();
            //sprite_renderer.sprite                  = KEY_TARGET_POINT_SPRITE;
            //key_target_object.transform.localScale  = new Vector3(.3f, .3f, .3f);
            //key_target_object.transform.position    = gesture_recognition.KEY_TARGET_POINT;

            object_selection.Draw_Sphere_From_Selected_Object();


            gesture_recognition.TYPE_KEY_TAP_ACTIVE = false;
        }

        // Detecting thumb to finish tap target process.

        if (gesture_recognition.THUMBS_UP_ACTIVE == true)
        {
            //if (gesture_recognition.TARGET_TAP_LIST.Count > 2)
            //{
            //    GameObject tap_target_object  = new GameObject();
            //    LineRenderer line_renderer    = tap_target_object.AddComponent<LineRenderer>();
            //    line_renderer.useWorldSpace   = false;
            //    line_renderer.positionCount   = gesture_recognition.TARGET_TAP_LIST.Count + 1;
            //    line_renderer.widthMultiplier = .1f;

            //    // Draw line using positions
            //    for (int i = 0; i < line_renderer.positionCount; i++)
            //    {
            //        if (i == line_renderer.positionCount - 1)
            //        {
            //            line_renderer.SetPosition(i, gesture_recognition.TARGET_TAP_LIST[0]);
            //        }
            //        else
            //        {
            //            line_renderer.SetPosition(i, gesture_recognition.TARGET_TAP_LIST[i]);
            //        }
            //    }

            //    // Clear list
            //    gesture_recognition.TARGET_TAP_LIST.Clear();
            //}


            gesture_recognition.THUMBS_UP_ACTIVE = false;
        }

        #endregion

        #region SWIPE PROCESS
        if (gesture_recognition.TYPE_SWIPE_ACTIVE == true)
        {
            // Determine direction of swipe
            switch (gesture_recognition.CURRENT_SWIPE_MODES)
            {
                case Swipe_Modes.SWIPE_LEFT:
                    {
                        current_UI_process.Start_Gesture_Notification_Panel(Notifications.HAND_SWIPE_LEFT);
                        break;
                    }
                case Swipe_Modes.SWIPE_RIGHT:
                    {
                        current_UI_process.Start_Gesture_Notification_Panel(Notifications.HAND_SWIPE_RIGHT);

                        break;
                    }
                case Swipe_Modes.SWIPE_UP:
                    {
                        // TO DO

                        break;
                    }
                case Swipe_Modes.SWIPE_DOWN:
                    {
                        // TO DO

                        break;
                    }
                case Swipe_Modes.SWIPE_INVALID:
                    {
                        // TO DO

                        break;
                    }
                default:
                    {
                        // TO DO

                        break;
                    }
            }

            gesture_recognition.TYPE_SWIPE_ACTIVE = false;
        }

        #endregion



    }
}