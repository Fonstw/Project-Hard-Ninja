using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] bool givesPickaxe, givesShoes;
    [SerializeField] int setsBeardTo = -1;

    bool used = false;

    private void OnTriggerStay(Collider other)
    {
        if (!used && Input.GetButton("Interact") && other.tag == "Player")
        {
            GetComponentInChildren<script_ac>().bool_Toggle = true;

            if (givesPickaxe) other.GetComponent<PlayerInteraction>().GainPickaxe();
            if (givesShoes) other.GetComponent<PlayerInteraction>().GainShoes();
            if (setsBeardTo >= 0) other.GetComponent<PlayerInteraction>().SetBeard(setsBeardTo);

            used = true;
        }
    }
}
