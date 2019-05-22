using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ai_FollowPath : MonoBehaviour
{

    public script_ai_Path comp_PathToFollow;

    public int i_CurrentWaypointID = 0;
    public float f_BaseSpeed;
    public float f_ReachDistance = 1f;
    public float f_RotationSpeed = 5f;
    public string string_PathName;

    public bool b_ReversePath = false;
    public bool b_Patrol = false;
    public bool b_DeleteOnEnd = false;
    public bool b_Paused = false;

    float f_Distance = 0f;
    Vector3 v3_LastPosition;
    Vector3 v3_CurrentPosition;

    void Start()
    {
        v3_LastPosition = transform.position;
        if(b_ReversePath) i_CurrentWaypointID = comp_PathToFollow.obj_Waypoints.Count - 1;
    }

    void Update()
    {
        if (i_CurrentWaypointID >= 0 && i_CurrentWaypointID <= comp_PathToFollow.obj_Waypoints.Count - 1)
        {
            if (b_Paused)
            {
                if (b_ReversePath) function_MoveToPosition(i_CurrentWaypointID + 1);
                else function_MoveToPosition(i_CurrentWaypointID - 1);
            }
            else
            {
                function_MoveToPosition(i_CurrentWaypointID);

                //Continue to next waypoint if reached current waypoint
                if (f_Distance <= f_ReachDistance)
                {
                    //toggle pause when waypoint has pause tag
                    if (comp_PathToFollow.obj_Waypoints[i_CurrentWaypointID].tag == "tag_Waypoint_Pause")
                    {
                        b_Paused = true;
                    }

                    if (b_ReversePath) i_CurrentWaypointID--;
                    else i_CurrentWaypointID++;
                }
            }
        }
        else
        {
            //if patrolling is turned on reverse path when finished. Else if delete is turned on delete object.
            if (b_Patrol)
            {
                b_ReversePath = !b_ReversePath;
                if (b_ReversePath) i_CurrentWaypointID -= 2;
                else i_CurrentWaypointID += 2;
            }
            else if (b_DeleteOnEnd) Destroy(gameObject);
        }

        void function_MoveToPosition(int i_WaypointID)
        {
            if(b_Paused) f_Distance = Vector3.Distance(comp_PathToFollow.obj_Waypoints[i_CurrentWaypointID].position, transform.position);
            else f_Distance = Vector3.Distance(comp_PathToFollow.obj_Waypoints[i_WaypointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, comp_PathToFollow.obj_Waypoints[i_WaypointID].position, Time.deltaTime * (f_BaseSpeed));
        }
    }
}
