using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_ac : MonoBehaviour
{
    public GameObject obj_Animated;
    public GameObject obj_ParticleSystem;
    Animator comp_Animator;
    ParticleSystem comp_ParticleSystem;
    public GameObject[] array_obj_ToToggle;
    public float f_ToggleOnDelay;
    public float f_ToggleOffDelay;

    public bool bool_Toggle;
bool bool_ToggleCheck;

    void Start()
    {
        comp_Animator = obj_Animated.GetComponent<Animator>();
        comp_ParticleSystem = obj_ParticleSystem.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (bool_Toggle != bool_ToggleCheck)
        {
            bool_ToggleCheck = bool_Toggle;
            StartCoroutine(function_ToggleObjects());
        }
    }

    IEnumerator function_ToggleObjects()
    {
        if (bool_Toggle == true)
        {
            comp_Animator.SetBool("bool_Toggle", true);
            yield return new WaitForSeconds(f_ToggleOnDelay);
            for (int i = 0; i < array_obj_ToToggle.Length; i++)
            {
                array_obj_ToToggle[i].SetActive(true);
                comp_ParticleSystem.Play();
            }
        }
        else
        {
            comp_Animator.SetBool("bool_Toggle", false);
            yield return new WaitForSeconds(f_ToggleOffDelay);
            for (int i = 0; i < array_obj_ToToggle.Length; i++)
            {
                array_obj_ToToggle[i].SetActive(false);
            }
        }
    }
}