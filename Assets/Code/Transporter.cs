using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    CameraBehaviour mainCamera;
    public Transform[] rooms;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.transform.position.x > transform.position.x)
                mainCamera.ChangeRoom(rooms[1]);
            else if (other.bounds.max.x < transform.position.x)
                mainCamera.ChangeRoom(rooms[0]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.transform.position.x > transform.position.x)
                mainCamera.ChangeRoom(rooms[0]);
            else if (other.bounds.max.x < transform.position.x)
                mainCamera.ChangeRoom(rooms[1]);
        }
    }
}
