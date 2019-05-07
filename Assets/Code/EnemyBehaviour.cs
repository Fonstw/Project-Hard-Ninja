using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float runSpeed = 8;
    [SerializeField] Transform myVisionLight;
    float speed;
    bool right = true;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        if (!StillFloor())
        {
            right = !right;
            myVisionLight.Rotate(0, 180, 0, Space.World);
        }
    }

    void Patrol()
    {
        if (right)
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        else
            rb.MovePosition(transform.position - Vector3.right * speed * Time.deltaTime);
    }

    bool StillFloor()
    {
        if (right)
        {
            Debug.DrawRay(transform.position, new Vector3(2, -2));

            return Physics.Raycast(transform.position, new Vector3(1, -1), 2);
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(-2, -2));

            return Physics.Raycast(transform.position, new Vector3(-1, -1), 2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 from = other.transform.position - transform.position;
            Vector3 to = Vector3.right;
            if (!right)
                to = -to;

            if (Vector3.Angle(from, to) <= 45 && Physics.Raycast(transform.position, from))
                speed = runSpeed;
            else
                speed = walkSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            speed = walkSpeed;
    }
}
