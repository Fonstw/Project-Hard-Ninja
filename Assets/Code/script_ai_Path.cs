using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ai_Path : MonoBehaviour
{

    public Color color_Main = Color.white;
    public Color color_WaypointPause = Color.blue;
    public List<Transform> obj_Waypoints = new List<Transform>();
    Transform[] array_Transforms;

    void OnDrawGizmos()
    {
        Gizmos.color = color_Main;
        array_Transforms = GetComponentsInChildren<Transform>();
        obj_Waypoints.Clear();

        foreach (Transform obj_Waypoint in array_Transforms)
        {
            if (obj_Waypoint != this.transform)
            {
                obj_Waypoints.Add(obj_Waypoint);
            }
        }

        for (int i = 0; i < obj_Waypoints.Count; i++)
        {
            Vector3 v3_Position = obj_Waypoints[i].position;
            if(i > 0)
            {
                Vector3 v3_PreviousPosition = obj_Waypoints[i - 1].position;
                if (obj_Waypoints[i].tag == "tag_Waypoint_Pause")
                {
                    Gizmos.color = color_WaypointPause;
                    Gizmos.DrawWireSphere(v3_Position, .5f);
                    Gizmos.color = color_Main;
                }
                else Gizmos.DrawWireSphere(v3_Position, .3f);

                Gizmos.DrawLine(v3_PreviousPosition, v3_Position);
            }
        }
    }
}
