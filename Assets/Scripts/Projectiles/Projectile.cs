using UnityEngine;
using System.Collections;

public class Projectile : Damage
{
    [SerializeField] private float projectileSpeed;
    private bool hit;
    [SerializeField] private float projectileResetTime;
    private float projectileLifetime;
    public float projectileDamage;
    private float direction;

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
        if (hit)
        {
            return;
        }

        // Move the projectile every frame if it does not hit anything
        float movementSpeed = projectileSpeed * Time.deltaTime;
        transform.Translate(movementSpeed * transform.localScale.x, 0, 0);

        projectileLifetime += Time.deltaTime;
        // If projectile does not hit anything for a duration, the projectile will be destroyed
        if (projectileLifetime > projectileResetTime)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.layer == 10)
        {
            if (GameObject.Find("Arcane Archer") == null || collison.gameObject != GameObject.Find("Arcane Archer"))
            {
                hit = true;
                boxCollider.enabled = false;
                if (damageType == Dmg.physical)
                {
                    collison.GetComponent<EnemyHealth>().TakePhysicalDamage(projectileDamage);
                }
                else if (damageType == Dmg.magic)
                {
                    collison.GetComponent<EnemyHealth>().TakeMagicDamage(projectileDamage);
                }
                else
                {
                    collison.GetComponent<EnemyHealth>().TakeTrueDamage(projectileDamage);
                }          
                anima.SetTrigger("explode");
                StartCoroutine(Impact());
            }
        }
        
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        hit = false;
        projectileLifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;

       float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
      
      
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Impact()
    {
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }
}
