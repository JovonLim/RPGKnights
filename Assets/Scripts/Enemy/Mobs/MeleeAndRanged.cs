using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAndRanged : Damage
{
    
    [SerializeField] private float meleeAttackRange;
    [SerializeField] private float rangedAttackRange;
    [SerializeField] public float damage;
    [SerializeField] protected float rangedDamage;

    [SerializeField] private float meleeColliderDistance;
    [SerializeField] private float rangedColliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected GameObject prefab;

    protected float attackCooldownTimer = float.MaxValue;
    protected float collisionTimer = float.MaxValue;
    protected Animator anima;
    protected Health playerHealth;
    private GameObject player;

    public float attackSpeed;

    protected virtual void Awake()
    {
        anima = GetComponent<Animator>();
        
    }

    protected virtual void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        collisionTimer += Time.deltaTime;
        if (PlayerInMeleeSight())
        {
            if (attackCooldownTimer >= attackSpeed)
            {
                attackCooldownTimer = 0;
                anima.SetTrigger("meleeAttack");
            }
        }
        else if (PlayerInRangedSight())
        {
            if (attackCooldownTimer >= attackSpeed)
            {
                attackCooldownTimer = 0;
                anima.SetTrigger("rangedAttack");
                
            }
        }

       

    }

    protected bool PlayerInMeleeSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * meleeAttackRange * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * meleeAttackRange * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangedAttackRange * transform.localScale.x * rangedColliderDistance,
            new Vector3(boxCollider.bounds.size.x * rangedAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInMeleeSight())
        {
            ApplyDamage();        
        }
    }

    public bool PlayerInRangedSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangedAttackRange * transform.localScale.x * rangedColliderDistance,
            new Vector3(boxCollider.bounds.size.x * rangedAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.gameObject;
        }

        return hit.collider != null;
    }

    public virtual void LaunchProjectile()
    {
        attackCooldownTimer = 0;

        // Launch the projectile
        GameObject spell = Instantiate(this.prefab, new Vector2(player.transform.position.x, player.transform.position.y + 1.2f), Quaternion.identity);
        spell.GetComponent<BringerOfDeathSpell>().enemyDamage = rangedDamage;
        spell.GetComponent<BringerOfDeathSpell>().ActivateProjectile();
    }

    public override void ScaleDifficulty(float Modifier)
    {
        damage *= Modifier;
        rangedDamage *= Modifier;
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
