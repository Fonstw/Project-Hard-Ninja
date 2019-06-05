using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerInteraction player = collision.transform.GetComponent<PlayerInteraction>();

            if (player.IsDigging())
            {
                // this at least 'works', it's stand-in
                Destroy(gameObject);

                /*if holding direction
                      go into that direction
                  else
                      move past thing

                  if player in wall
                      player.Die();
                */
            }
        }
    }
}
