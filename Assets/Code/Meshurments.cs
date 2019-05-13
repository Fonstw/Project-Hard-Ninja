using UnityEngine;

public class Meshurments : MonoBehaviour
{
    [SerializeField] float roundTo;
    
    void Start()
    {
        Vector3 sizes = Vector3.zero;

        if (GetComponent<Mesh>() != null) sizes = GetComponent<Mesh>().bounds.size;
        else if (GetComponent<MeshCollider>() != null) sizes = GetComponent<MeshCollider>().bounds.size;
        else if (GetComponent<MeshRenderer>() != null) sizes = GetComponent<MeshRenderer>().bounds.size;
        else if (GetComponent<Collider>() != null) sizes = GetComponent<Collider>().bounds.size;

        print(name + "'s measured sizes are " + sizes.x.ToString("F" + roundTo) + " by " + sizes.y.ToString("F" + roundTo) + " by " + sizes.z.ToString("F" + roundTo));
    }
}
