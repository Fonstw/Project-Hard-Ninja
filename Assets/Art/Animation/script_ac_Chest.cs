using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ac_Chest : MonoBehaviour
{
    GameObject obj_Animated;
    Animator comp_Animator;
    public bool bool_ChestOpen;

    void Start()
    {
        comp_Animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        //if (bool_ChestOpen == true) comp_Animator.SetBool("bool_ChestOpen", true);
        //else comp_Animator.SetBool("bool_ChestOpen", false);

        comp_Animator.SetBool("bool_ChestOpen", bool_ChestOpen);
    }
}