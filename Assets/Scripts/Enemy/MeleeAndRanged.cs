using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAndRanged : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float meleeAttackRange;
    [SerializeField] private float rangedAttackRange;
    [SerializeField] private float damage;

    [SerializeField] private float meleeColliderDistance;
    [SerializeField] private float rangedColliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected GameObject prefab;

    protected float attackCooldownTimer = float.MaxValue;
    protected Animator anima;
    private Health playerHealth;
    private GameObject player;


    public virtual void Awake()
    {
        anima = GetComponent<Animator>();
        
    }

    public virtual void Update()
    {
        attackCooldownTimer += Time.deltaTime;

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

    public bool PlayerInMeleeSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * meleeAttackRange * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
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
            playerHealth.TakeDamage(damage);
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
        spell.GetComponent<BringerOfDeathSpell>().ActivateProjectile();
    }
}
