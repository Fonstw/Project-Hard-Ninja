using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] bool demoMode;

    public enum BeardTier { none = 0, stubble = 1, hipster = 2, viking = 3 };

    [SerializeField] float maxLives, maxStamina, fallDepht;
    [SerializeField] bool pickaxe, shoes, invincibility;
    [SerializeField] BeardTier beardTier;
    [SerializeField] private int chests = 0;
    [SerializeField] private Text scoreText;

    float curLives, curStamina;

    void Start()
    {
        scoreText = FindObjectOfType<Text>();
        // set playerprefs
        if (demoMode || PlayerPrefs.GetInt("pickaxe", -1) == -1)
            PlayerPrefs.SetInt("pickaxe", 0);
        if (demoMode || PlayerPrefs.GetInt("shoes", -1) == -1)
            PlayerPrefs.SetInt("shoes", 0);
        if (demoMode || PlayerPrefs.GetInt("beardTier", -1) == -1)
            PlayerPrefs.SetInt("beardTier", 0);

        // set abilities
        pickaxe = PlayerPrefs.GetInt("pickaxe", 0) == 1;
        shoes = PlayerPrefs.GetInt("shoes", 0) == 1;
        invincibility = false;

        // set param max
        switch (PlayerPrefs.GetInt("beardTier", 0))
        {
            case 1:
                beardTier = BeardTier.stubble;
                maxLives = 2;
                break;
            case 2:
                beardTier = BeardTier.hipster;
                maxLives = 3;
                break;
            case 3:
                beardTier = BeardTier.viking;
                maxLives = 1;
                invincibility = true;
                break;
            default:
                beardTier = BeardTier.none;
                maxLives = 1;
                break;
        }
        maxStamina = 1;

        // set param current
        curLives = maxLives;
        curStamina = maxStamina;

        // debug the fall depht
        if (fallDepht <= 0)
            fallDepht = 150;
    }

    void Update()
    {
        if (transform.position.y < -fallDepht)
            Die();
        scoreText.text = "Chests opened: " + chests;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
            TakeDamage(1);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(float dmg)
    {
        curLives = Mathf.Clamp(curLives-dmg, 0, maxLives);

        if (curLives <= 0)
            Die();
        else if (curLives > maxLives)
            curLives = maxLives;
    }

    public bool IsDigging()
    {
        return pickaxe && Input.GetButton("Interact");
    }

    public void GainPickaxe()
    {
        pickaxe = true;
        PlayerPrefs.SetInt("pickaxe", 1);
    }

    public void GainShoes()
    {
        shoes = true;
        PlayerPrefs.SetInt("shoes", 1);
    }

    public bool SetBeard(int beardLevel)
    {
        switch (beardLevel)
        {
            case 0:
                beardTier = BeardTier.none;
                PlayerPrefs.SetInt("beardTier", beardLevel);
                return true;
            case 1:
                beardTier = BeardTier.stubble;
                PlayerPrefs.SetInt("beardTier", beardLevel);
                return true;
            case 2:
                beardTier = BeardTier.hipster;
                PlayerPrefs.SetInt("beardTier", beardLevel);
                return true;
            case 3:
                beardTier = BeardTier.viking;
                PlayerPrefs.SetInt("beardTier", beardLevel);
                return true;
            default:
                return false;
        }
    }

    public void addChest()
    {
        chests++;
    }
}
