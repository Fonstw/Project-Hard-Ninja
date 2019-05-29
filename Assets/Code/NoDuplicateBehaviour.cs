using UnityEngine;

public class NoDuplicateBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length == 1)   // if there is no-one like me...
            DontDestroyOnLoad(gameObject);   // make sure I don't ever go away!
        else   // else there is someone like me...
            Destroy(gameObject);   // so I shouldn't interfere, time to kill myself bye!
    }

    // Update is called once per frame
    void Update()
    {

    }
}
