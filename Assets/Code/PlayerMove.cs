using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float jump = 100;
    Rigidbody rb;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }

    void Walk()
    {
        Vector3 move = new Vector3(Input.GetAxis("Move"), 0);
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;

            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            grounded = true;
    }
}
