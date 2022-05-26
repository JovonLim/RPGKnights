using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    // Ensures that health can only be get from this script from anywhere but not allowed to be modified
    public float currentHealth { get; private set; }
    private Animator anima;
    private bool isDead;

    private float Defense = 0;

    public GameObject damageIndicator;
    private void Awake()
    {
        currentHealth = startingHealth;
        anima = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) 
    {
        float netDamage = damage - Defense;
        GameObject damageInd = Instantiate(damageIndicator, transform.position, Quaternion.identity) as GameObject;
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().text = netDamage.ToString();

        // Ensure the current health of player stays within 0 and the starting health
        currentHealth = Mathf.Clamp(currentHealth - netDamage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player is hurt
            anima.SetTrigger("hurt");
        }
        else
        {
            // Player will die once it has no health
            if (!isDead)
            {
                anima.SetTrigger("die");

                // For enemy
                if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<CoinSpawn>().Spawn();
                    isDead = true;
                }

                if (GetComponent<RangeEnemy>() != null)
                {
                    GetComponent<RangeEnemy>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<CoinSpawn>().Spawn();
                    isDead = true;
                }


                if (GetComponentInParent<Patrol>() != null)
                {
                    GetComponentInParent<Patrol>().enabled = false;
                    isDead = true;
                }
                    
                // For player
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<PlayerInteraction>().Respawn();
                }
                    

                
            }
            
        }
              
    }

    public void GainHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth + damage, 0, startingHealth);
    }

    public bool IsDefeated()
    {
        return isDead;
    }

    public void AddHealth()
    {
        startingHealth += 1;
        currentHealth = Mathf.Clamp(currentHealth + 1, 0, startingHealth);
    }

    public void AddDefense()
    {
        Defense += 0.5f;
    }
}
