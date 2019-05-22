using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float runSpeed = 8;
    [SerializeField] Transform myVisionLight;
    [SerializeField] float groundDistance = 2f;
    float speed;
    bool right = true;
    Rigidbody rb;
    
    void Start()
    {
        speed = walkSpeed;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Patrol();
        
        if (WhatISee() != "Floor")
            Turn();
    }

    void Patrol()
    {
        if (right)
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        else
            rb.MovePosition(transform.position - Vector3.right * speed * Time.deltaTime);
    }

    string WhatISee()
    {
        if (right)
        {
            Debug.DrawRay(transform.position, new Vector3(groundDistance, -groundDistance));

            if (Physics.Raycast(transform.position, new Vector3(1, -1), out RaycastHit hit, groundDistance))
                return hit.collider.tag;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(-groundDistance, -groundDistance));

            if (Physics.Raycast(transform.position, new Vector3(-1, -1), out RaycastHit hit, groundDistance))
                return hit.collider.tag;
        }

        return "";
    }

    void Turn()
    {
        right = !right;
        myVisionLight.Rotate(0, 180, 0, Space.World);
        transform.Rotate(0,180,0, Space.World);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Turn();
    }
}
