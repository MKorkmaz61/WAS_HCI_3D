using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Unity;

public class Survaillence_Target
{
    public int         target_ID                { get; set; }
    public Target_Type target_type              { get; set; }
    public Vector3     target_points            { get; set; }
    public GameObject  target_root_game_objects { get; set; } 
}

public enum Target_Type
{
    TARGET_CIRCLE,
    TARGET_RECTANGULAR
}
