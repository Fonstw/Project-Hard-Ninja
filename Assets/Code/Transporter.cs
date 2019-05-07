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
            Transform cur = mainCamera.CurrentRoom();

            if (cur == rooms[0])
                mainCamera.ChangeRoom(rooms[1]);
            else
                mainCamera.ChangeRoom(rooms[0]);
        }
    }
}
