using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{


    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("IsRunning", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsDrunk", true);
        anim.SetBool("IsAttacking", false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsJumping", true);
                anim.SetBool("IsDrunk", false);
                anim.SetBool("IsAttacking", false);
            }
        }

        if (Input.GetButtonDown("Move"))
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsDrunk", false);
            anim.SetBool("IsAttacking", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsDrunk", false);
            anim.SetBool("IsAttacking", true);
        }

    }
}
