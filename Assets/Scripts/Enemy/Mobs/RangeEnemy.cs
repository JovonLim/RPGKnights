using UnityEngine;

public class RangeEnemy : CombatManager
{
    
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform projectileLaunchPoint;

    [SerializeField] GameObject prefab = null;

    [SerializeField] private GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    private float attackCooldownTimer = float.MaxValue;
    protected float collisionTimer = float.MaxValue;
    public float attackSpeed;
    private PlayerHealth playerHealth;

    // Reference variables
    protected Animator anima;

    public virtual void Awake()
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
                anima.SetTrigger("attack");
            }
        }

    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

   
    private void LaunchProjectile()
    {
        attackCooldownTimer = 0;

        // Launch the projectile
        GameObject fireball = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        fireball.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        fireball.GetComponent<EnemyProjectile>().enemyDamage = damage;
        fireball.GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    public override void ScaleDifficulty(float Modifier)
    {
        damage *= Modifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collisionTimer >= 2.0f)
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
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
