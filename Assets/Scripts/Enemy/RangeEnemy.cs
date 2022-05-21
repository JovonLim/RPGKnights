using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject[] projectileStorage;


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

    private bool PlayerInSight()
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
        projectileStorage[FindProjectile()].transform.position = projectileLaunchPoint.position;
        projectileStorage[FindProjectile()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindProjectile()
    {
        for (int i = 0; i < projectileStorage.Length; i++)
        {
            if (projectileStorage[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
