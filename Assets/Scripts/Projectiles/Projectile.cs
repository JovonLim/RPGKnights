using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private bool hit;
    [SerializeField] private float projectileResetTime;
    private float projectileLifetime;
    [SerializeField] private float projectileDamage;

    // References
    private BoxCollider2D boxCollider;
    private Animator anima;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anima = GetComponent<Animator>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        projectileLifetime = 0;
        gameObject.SetActive(true);

    }

    private void Update()
    {
        if (hit) return;

        // Move the projectile every frame if it does not hit anything
        float movementSpeed = projectileSpeed * Time.deltaTime;
        transform.Translate(movementSpeed * transform.localScale.x, 0, 0);

        projectileLifetime += Time.deltaTime;
        // If projectile does not hit anything for a duration, the projectile will be destroyed
        if (projectileLifetime > projectileResetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "Enemy")
        {
            hit = true;
            boxCollider.enabled = false;
            collison.GetComponent<Health>().TakeDamage(projectileDamage);
            anima.SetTrigger("explode");
        }
        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
