using UnityEngine;
using System.Collections;

public class MeleeEnemy : CombatManager
{
    [SerializeField] private float attackRange;
    [SerializeField] public float damage;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    protected float attackCooldownTimer = float.MaxValue;
    protected float collisionTimer = float.MaxValue;

    // Reference variables
    protected Animator anima;
    private Health playerHealth;
    public float attackSpeed;


    private void Awake()
    {
        anima = GetComponent<Animator>();
       
    }

    private void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        collisionTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (attackCooldownTimer >= attackSpeed)
            {
                attackCooldownTimer = 0;
                anima.SetTrigger("meleeAttack");
            }
        }

       

    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<PlayerHealth>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    protected void DamagePlayer()
    {
        if (PlayerInSight())
        {
            ApplyDamage();   
        }
    }

    public override void ScaleDifficulty(float Modifier)
    {
        damage *= Modifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collisionTimer >= 2.0f)
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            ApplyDamage();
            collisionTimer = 0;
        }
    }

    void ApplyDamage()
    {
        if (damageType == Dmg.physical)
        {
            playerHealth.TakePhysicalDamage(damage);
        }
        else if (damageType == Dmg.magic)
        {
            playerHealth.TakeMagicDamage(damage);
        }
        else
        {
            playerHealth.TakeTrueDamage(damage);
        }
    }
}
