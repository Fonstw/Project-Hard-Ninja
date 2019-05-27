using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerMovement : MonoBehaviour
{
    //gravity without rigidbody
    //public bool b_EnableGravity = false;
    //public float f_Gravity = -9.81f;

    //movement variables
    public float f_DoubleTapTime = .5f;
    public float f_MovementSpeed = 5f;
    public bool b_SidewaysCollision;
    Vector3 v3_MovementDirection;

    //jumping variables
    public float f_JumpForce;
    public float f_GroundedDistance = 1f;
    public bool b_Jumping;
    public float f_BaseJumpTimer = 1f;
    public float f_JumpTimer;

    Rigidbody comp_Rigidbody;
    Collider comp_BoxCollider;

    void Start()
    {
        f_JumpTimer = f_BaseJumpTimer;

        comp_Rigidbody = GetComponent<Rigidbody>();
        comp_BoxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        function_GetInputs();
        function_Jump();
        function_Movement();
    }

    void FixedUpdate()
    {
    }

    bool bool_CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, f_GroundedDistance))
            return true;
        else
            return false;
    }

    void function_GetInputs()
    {
        //input for movement
        v3_MovementDirection = new Vector3(Input.GetAxis("Move"), 0);

        //input for jumping
        if (Input.GetButtonDown("Jump") && bool_CheckIfGrounded())
            b_Jumping = true;
        else if (Input.GetButtonUp("Jump"))
            b_Jumping = false;
    }

    void function_Jump()
    {
        if (b_Jumping)
            if(f_JumpTimer == f_BaseJumpTimer)
            {
                comp_Rigidbody.velocity = new Vector3(comp_Rigidbody.velocity.x, 0);
                comp_Rigidbody.AddForce(Vector3.up * f_JumpForce);
                f_JumpTimer -= Time.deltaTime;
            }
            else if (f_JumpTimer > 0)
            {
                comp_Rigidbody.AddForce(Vector3.up * f_JumpForce * f_JumpTimer * .15f);
                f_JumpTimer -= Time.deltaTime;
            }
            else
            {
                b_Jumping = false;
            }
        else
            f_JumpTimer = f_BaseJumpTimer;
    }

    void function_Movement()
    {
        RaycastHit ray_hitInfo;
        if (!Physics.SphereCast(transform.position, .15f, v3_MovementDirection,out ray_hitInfo, .01f))
        {
            comp_Rigidbody.velocity = new Vector3(v3_MovementDirection.x * f_MovementSpeed, comp_Rigidbody.velocity.y);
        }
    }
}
