using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 startPos;

    void Start()
    {
        if (target != null)
            startPos = transform.position - target.transform.position;
        else
            Debug.LogWarning("Please set " + name + "'s CameraBehaviour.target.");
    }

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
