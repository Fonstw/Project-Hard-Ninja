using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerMovement : MonoBehaviour
{
    //gravity without rigidbody
    //public bool b_EnableGravity = false;
    //public float f_Gravity = -9.81f;

    //Movement variables
    public float f_DoubleTapTime = .5f;
    public float f_MovementSpeed = 5f;
    public bool b_SidewaysCollision;
    Vector3 v3_MovementDirection;

    //Jumping variables
    public float f_JumpForce;
    public float f_GroundedDistance = 1f;
    public bool b_Jumping;
    public float f_BaseJumpTimer = 1f;
    public float f_JumpTimer;

    Rigidbody comp_Rigidbody;
    Collider comp_BoxCollider;

    //Animation variables
    public GameObject obj_AnimatorHolder;
    int intSwitch_Animation;
    List<float> fList_LastMoveInputValue = new List<float>();

    //References
    Animator comp_Animator;

    void Start()
    {
        f_JumpTimer = f_BaseJumpTimer;

        comp_Rigidbody = GetComponent<Rigidbody>();
        comp_BoxCollider = GetComponent<BoxCollider>();
        comp_Animator = obj_AnimatorHolder.GetComponent<Animator>();
    }

    void Update()
    {
        function_GetInputs();
    }

    void FixedUpdate()
    {
        function_Jump();
        function_Movement();
    }

    bool bool_CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, f_GroundedDistance);
    }

    void function_GetInputs()
    {
        //interact animation
        if (Input.GetButtonDown("Interact")) function_PlayAnimation("Attack");

        //input for movement
        v3_MovementDirection = new Vector3(Input.GetAxis("Move"), 0);

        //input for jumping
        if (Input.GetButtonDown("Jump") && bool_CheckIfGrounded()) b_Jumping = true;
        else if (Input.GetButtonUp("Jump")) b_Jumping = false;
    }

    // prevent player mistakes (eg. don't stop jumping if let go of button very short time)
    void function_Jump()
    {
        if (b_Jumping)
        {
            function_PlayAnimation("Jump");
            if (f_JumpTimer == f_BaseJumpTimer)
            {
                comp_Rigidbody.velocity = new Vector3(comp_Rigidbody.velocity.x, 0);
                comp_Rigidbody.AddForce(Vector3.up * f_JumpForce);
                f_JumpTimer -= Time.deltaTime;
            }
            else if (f_JumpTimer > 0)
            {
                comp_Rigidbody.AddForce(Vector3.up * f_JumpForce * f_JumpTimer * .1f);
                f_JumpTimer -= Time.deltaTime;
            }
            else
            {
                b_Jumping = false;
            }
        }
        else
            f_JumpTimer = f_BaseJumpTimer;
    }

    void function_Movement()
    {
        //Animation
        if (bool_CheckIfGrounded())
        {
            bool b_Moved = false;
            if(fList_LastMoveInputValue != null)
                for (int i = 0; i < fList_LastMoveInputValue.Count; i++)
                {
                    if (fList_LastMoveInputValue[i] != 0) b_Moved = true;
                }

            if (Input.GetAxis("Move") != 0 || b_Moved || v3_MovementDirection != Vector3.zero)
            {
                function_PlayAnimation("Run");
            }
            else
            {
                function_PlayAnimation("Idle");
            }
        }
        else if (!b_Jumping)
        {
            if (Input.GetAxis("Move") != 0)
            {
                function_PlayAnimation("Run");
            }
            else
            {
                function_PlayAnimation("Idle");
            }
        }

        //flip player model to direction
        if (Input.GetAxisRaw("Move") != 0)
        {
            if (Input.GetAxisRaw("Move") == -1) transform.localScale = new Vector3(-1, 1, 1);
            else if (Input.GetAxisRaw("Move") == 1) transform.localScale = new Vector3(1, 1, 1);
        }

        if (!Physics.SphereCast(transform.position, .15f, v3_MovementDirection,out RaycastHit ray_hitInfo, .01f))
        {
            comp_Rigidbody.velocity = new Vector3(v3_MovementDirection.x * f_MovementSpeed, comp_Rigidbody.velocity.y);
        }

        fList_LastMoveInputValue.Add(Input.GetAxisRaw("Move"));
        if(fList_LastMoveInputValue.Count > 5)
            fList_LastMoveInputValue.RemoveAt(0);
    }

    void function_PlayAnimation(string string_AnimationToPlay)
    {
        if (string_AnimationToPlay == "Idle") intSwitch_Animation = 1;
        if (string_AnimationToPlay == "Jump") intSwitch_Animation = 2;
        if (string_AnimationToPlay == "Attack") intSwitch_Animation = 3;
        if (string_AnimationToPlay == "Run") intSwitch_Animation = 4;

        switch (intSwitch_Animation)
        {
            case 1:
                //Idle
                comp_Animator.SetBool("IsDrunk", true);
                comp_Animator.SetBool("IsJumping", false);
                comp_Animator.SetBool("IsAttacking", false);
                comp_Animator.SetBool("IsRunning", false);
                return;
            case 2:
                //Jump
                comp_Animator.SetBool("IsDrunk", false);
                comp_Animator.SetBool("IsJumping", true);
                comp_Animator.SetBool("IsAttacking", false);
                comp_Animator.SetBool("IsRunning", false);
                return;
            case 3:
                //Attack
                comp_Animator.SetBool("IsDrunk", false);
                comp_Animator.SetBool("IsJumping", false);
                comp_Animator.SetBool("IsAttacking", true);
                comp_Animator.SetBool("IsRunning", false);
                return;
            case 4:
                //Run
                comp_Animator.SetBool("IsDrunk", false);
                comp_Animator.SetBool("IsJumping", false);
                comp_Animator.SetBool("IsAttacking", false);
                comp_Animator.SetBool("IsRunning", true);
                return;
        }

    }
}
