using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Enemy_Seeer : MonoBehaviour
{
    float f_monsterWidth;
    public float f_patrolDistance = 1f;
    public float f_baseSpeed = .2f;
    public float f_runSpeedMultiplier = 1.5f;
    public float f_rotateSpeed = 5f;
    public float f_baseStareTimer = 5f; //how long to keep staring at the player when seeing player but not moving.
    float f_stareTimer;
    public bool b_moving = true;
    public bool b_turning = false;
    public bool b_movingRight = true;
    public bool b_seesPlayer = false;
    public bool b_chasingPlayer = false;
    public bool b_playedSound = false;

    Vector3 v3_startPosition;
    Vector3 v3_endPosition;

    GameObject obj_player;

    Rigidbody comp_rigidbody;
    AudioSource comp_audioSource;

    private void Awake()
    {
        f_monsterWidth = GetComponent<CapsuleCollider>().radius;
        f_stareTimer = f_baseStareTimer;
        v3_startPosition = transform.position;
        v3_endPosition = transform.position + new Vector3(f_patrolDistance,0,0);
        obj_player = GameObject.FindGameObjectWithTag("Player");
        comp_rigidbody = GetComponent<Rigidbody>();
        comp_audioSource = GetComponent<AudioSource>();
    }

    //visualize patrol route in editor
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(f_patrolDistance, 0, 0));
            Gizmos.DrawWireSphere(transform.position + new Vector3(f_patrolDistance, 0, 0), .1f);
        }
        else
        {
            Gizmos.DrawLine(v3_startPosition, v3_endPosition);
            Gizmos.DrawWireSphere(v3_endPosition, .1f);
        }
    }

    void Update()
    {
        function_checkForPlayer();
        function_playSound();
        function_updateMovement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            obj_player = other.gameObject;
            b_seesPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            b_seesPlayer = false;
            b_playedSound = false;
        }
    }

    //if sees player and player is within patrol range, chase player.
    //if sees player and staring at player, start counting down staretimer.
    void function_checkForPlayer()
    {
        if (b_seesPlayer)
        {
            if (obj_player.transform.position.x + f_monsterWidth > v3_startPosition.x && obj_player.transform.position.x - f_monsterWidth < v3_endPosition.x) b_chasingPlayer = true;
            else b_chasingPlayer = false;

            if (!b_moving)
                f_stareTimer -= Time.deltaTime;
        }
        else b_chasingPlayer = false;
    }

    //everything sound related
    void function_playSound()
    {
        if (b_chasingPlayer && !b_playedSound)
        {
            b_playedSound = true;
            comp_audioSource.time = Random.Range(.65f, .75f);
            comp_audioSource.pitch = Random.Range(.7f, 1f);
            comp_audioSource.Play();
        }
    }

    //movement
    void function_updateMovement()
    {
        float f_currentSpeed;
        if (b_moving)
        {
            if (b_chasingPlayer) f_currentSpeed = f_baseSpeed * f_runSpeedMultiplier;
            else f_currentSpeed = f_baseSpeed;

            if (b_movingRight) comp_rigidbody.MovePosition(transform.position + Vector3.right * f_currentSpeed * Time.deltaTime);
            else comp_rigidbody.MovePosition(transform.position + Vector3.left * f_currentSpeed * Time.deltaTime);
        }

        //turn the enemy around when he goes beyond his patrol distance.
        //if the enemy was chasing the player the enemy will keep staring at the player.
        if (transform.position.x < v3_startPosition.x && !b_movingRight)
            if (b_seesPlayer && f_stareTimer > 0f) b_moving = false;
            else
            {
                b_turning = true;
                b_movingRight = true;
                f_stareTimer = f_baseStareTimer;
            }
        else if (transform.position.x > v3_endPosition.x && b_movingRight)
            if (b_seesPlayer && f_stareTimer > 0f) b_moving = false;
            else
            {
                b_turning = true;
                b_movingRight = false;
                f_stareTimer = f_baseStareTimer;
            }

        //everything for turning around
        if (b_turning)
        {
            b_moving = false;

            //turn from left to right
            if (b_movingRight)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), f_rotateSpeed);
                if (transform.rotation.y == Quaternion.Euler(0,0,0).y)
                {
                    b_turning = false;
                    b_moving = true;
                }
            }
            //turn from right to left
            else if (!b_movingRight)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -180, 0), f_rotateSpeed);
                if (transform.rotation.y <= Quaternion.Euler(0, -180, 0).y)
                {
                    b_turning = false;
                    b_moving = true;
                }
            }
        }
    }
}
