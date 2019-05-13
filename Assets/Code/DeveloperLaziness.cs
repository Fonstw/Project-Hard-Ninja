using UnityEngine;

public class DeveloperLaziness : MonoBehaviour
{
    [SerializeField] float gameSpeed, gravityForce;
    Vector3 initialGravity;

    // Start is called before the first frame update
    void Start()
    {
        initialGravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameSpeed < 0 || gameSpeed > 100)
            gameSpeed = Mathf.Clamp(gameSpeed, 0, 100);

        if (Time.timeScale != gameSpeed)
            Time.timeScale = gameSpeed;

        if (gravityForce < -9999999 || gravityForce > 9999999)
            gravityForce = Mathf.Clamp(gravityForce, -9999999, 9999999);

        if (Physics.gravity != initialGravity * gravityForce)
            Physics.gravity = initialGravity * gravityForce;
    }
}
