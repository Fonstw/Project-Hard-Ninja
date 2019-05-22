using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Enemy_Seeer : MonoBehaviour
{
    public float f_patrolDistance = 1f;
    public float f_baseSpeed = .2f;
    public float f_runSpeedMultiplier = 1.5f;
    public bool b_moving = true;
    public bool b_movingRight = true;
    public bool b_running = false;
    public bool b_seesPlayer = false;

    Vector3 v3_startPosition;
    Vector3 v3_endPosition;

    GameObject obj_visionLight;

    Rigidbody comp_rigidbody;

    private void Awake()
    {
        v3_startPosition = transform.position;
        v3_endPosition = transform.position + new Vector3(f_patrolDistance,0,0);
        comp_rigidbody = GetComponent<Rigidbody>();
    }

    //visualize patrol route in editor
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(f_patrolDistance, 0, 0));
            Gizmos.DrawWireSphere(transform.position + new Vector3(f_patrolDistance, 0, 0), .1f);
        }
    }

    void Update()
    {
        function_CheckForPlayer();
        function_UpdateMovement();
    }

    //movement
    void function_UpdateMovement()
    {
        //if enemy spots a player the enemy starts running
        if (b_seesPlayer)
            b_running = true;

        float f_currentSpeed;
        if (b_moving)
            if (b_running)
                f_currentSpeed = f_baseSpeed * f_runSpeedMultiplier;
            else
                f_currentSpeed = f_baseSpeed;
        else f_currentSpeed = 0f;

        if (b_movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            comp_rigidbody.MovePosition(transform.position + Vector3.right * f_currentSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            comp_rigidbody.MovePosition(transform.position + Vector3.left * f_currentSpeed * Time.deltaTime);
        }

        //turn the enemy around when he goes beyond his patrol distance.
        //if the enemy was chasing the player the enemy will keep staring at the player.
        if (transform.position.x > v3_endPosition.x)
        {
            if (b_seesPlayer)
                b_moving = false;
            else
                b_movingRight = false;
        }
        else if (transform.position.x < v3_startPosition.x)
        {
            if (b_seesPlayer)
                b_moving = false;
            else
                b_movingRight = true;
        }
    }

    void function_CheckForPlayer()
    {
            return;
    }
}
