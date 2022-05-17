using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    // Ensures that health can only be get from this script from anywhere but not allowed to be modified
    public float currentHealth { get; private set; }
    private Animator anima;
    private bool isDead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anima = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) 
    {
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
                anima.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
            
        }
              
    }

}
