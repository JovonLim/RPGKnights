using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcher : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform projectileLaunchPoint;

    [SerializeField] GameObject prefab = null;

    [SerializeField]
    private GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    private float attackCooldownTimer = float.MaxValue;

    // Reference variables
    private Animator anima;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        attackCooldownTimer += Time.deltaTime;

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
        GameObject arrow = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        arrow.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        arrow.GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            anima.SetTrigger("roll");
        }
    }
}
