using UnityEngine;

public class GroundManipulation : MonoBehaviour
{
    // References
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject dissappearingGround;
    private Animator anima;

    // Cast Spell
    [SerializeField] private float castCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private float spellDuration;
    private float spellActiveDuration = 0;

    private static bool casting = false;
    private float castCooldownTimer = float.MaxValue;

    // Ranged Attacks
    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] GameObject prefab = null;

    [SerializeField]
    private GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (casting)
        {
            castCooldownTimer = 0;
            spellActiveDuration += Time.deltaTime;
            if (spellActiveDuration > spellDuration)
            {
                FinishSpell();
            }
        }
        else
        {
            castCooldownTimer += Time.deltaTime;

            if (PlayerInSight())
            {
                if (castCooldownTimer > castCooldown)
                {
                    anima.SetTrigger("cast");
                }
            }
        } 
    }

    private void CastSpell()
    {
        casting = true;
        dissappearingGround.SetActive(false);
    }

    private void FinishSpell()
    {
        casting = false;
        spellActiveDuration = 0;
        dissappearingGround.SetActive(true);
    }

    private void LaunchProjectile()
    {
        // Launch the projectile
        GameObject projectile = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<EnemyProjectile>().ActivateProjectile();
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
