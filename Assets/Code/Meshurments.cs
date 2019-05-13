using UnityEngine;

public class Meshurments : MonoBehaviour
{
    [SerializeField] bool log = true;
    [SerializeField] int roundTo = -1;
    Vector3 sizes = Vector3.zero;

    void Start()
    {
        if (GetComponent<MeshCollider>() != null) sizes = GetComponent<MeshCollider>().bounds.size;
        else if (GetComponent<MeshRenderer>() != null) sizes = GetComponent<MeshRenderer>().bounds.size;
        else if (GetComponent<Collider>() != null) sizes = GetComponent<Collider>().bounds.size;

        if (log)
        {
            if (roundTo >= 0)
                print(name + "'s measured sizes are " + sizes.x.ToString("F" + roundTo) + " by " + sizes.y.ToString("F" + roundTo) + " by " + sizes.z.ToString("F" + roundTo));
            else
                print(name + "'s measured sizes are " + sizes.x + " by " + sizes.y + " by " + sizes.z);
        }
    }

    public Vector3 GetSizes()
    {
    	return sizes;
    }

    public Vector3 GetRoundedSizes()
    {
        if (roundTo >= 0)
        {
            Vector3 answer = Vector3.zero;

            answer.x = float.Parse(sizes.x.ToString("F" + roundTo));
            answer.y = float.Parse(sizes.y.ToString("F" + roundTo));
            answer.z = float.Parse(sizes.z.ToString("F" + roundTo));

            return answer;
        }
        else
            return sizes;
    }
}
