using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    // Ensures that health can only be get from this script from anywhere but not allowed to be modified
    public float currentHealth { get; private set; }
    private Animator anima;
    private bool isDead;

    [SerializeField] private float PhysicalDefense = 0;
    [SerializeField] private float MagicDefense = 0;
    [SerializeField] private float minDmg;
    [SerializeField] private GameObject prefab;
    private void Awake()
    {
        currentHealth = startingHealth;
        anima = GetComponent<Animator>();
    }

    public void TakePhysicalDamage(float damage) 
    {
        float netDamage = damage - PhysicalDefense;
        if (netDamage < 0)
        {
            netDamage = minDmg;
        }
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
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
                OnDeath();
                isDead = true;             
            }
            
        }   
    }

    public void TakeMagicDamage(float damage)
    {
        float netDamage = damage - MagicDefense;
        if (netDamage < 0)
        {
            netDamage = minDmg;
        }
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().color = Color.blue;
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
                OnDeath();
                isDead = true;
            }
        }
    }

    public void TakeTrueDamage(float damage)
    {
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();

        // Ensure the current health of player stays within 0 and the starting health
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

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
                OnDeath();
                isDead = true;
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

    public void AddHealth(float amt)
    {
        startingHealth += amt;
        currentHealth = Mathf.Clamp(currentHealth + amt, 0, startingHealth);
    }

    public void AddPhysicalDefense(float amt)
    {
        PhysicalDefense += amt;
    }

    public void AddMagicDefense(float amt)
    {
        MagicDefense += amt;
    }

    public void AddDefense(float amt)
    {
        PhysicalDefense += amt;
        MagicDefense += amt;
    }

    private void OnDeath()
    {
        anima.SetTrigger("die");

        // For enemy
        if (GetComponent<MeleeEnemy>() != null)
        {
            GetComponent<MeleeEnemy>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();
            
        }

        if (GetComponent<RangeEnemy>() != null)
        {
            GetComponent<RangeEnemy>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();
           
        }

        if (GetComponent<MeleeAndRanged>() != null)
        {
            GetComponent<MeleeAndRanged>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();
            
        }
        if (GetComponent<Aggro>() != null)
        {
            GetComponentInParent<Aggro>().enabled = false;
        }

        // For player
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerDeath>().Respawn();
        }
    }
}
