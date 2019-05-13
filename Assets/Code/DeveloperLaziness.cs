using UnityEngine;

public class DeveloperLaziness : MonoBehaviour
{
    [SerializeField] float gameSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != gameSpeed)
            Time.timeScale = gameSpeed;
    }
}
