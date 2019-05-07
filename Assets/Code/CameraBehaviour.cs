using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
            startPos = transform.position - target.transform.position;
        else
            Debug.LogWarning("Please set " + name + "'s CameraBehaviour.target.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom(Transform newRoom)
    {
        transform.position = newRoom.position + startPos;
        target = newRoom;
    }

    public Transform CurrentRoom()
    {
        return target;
    }
}
