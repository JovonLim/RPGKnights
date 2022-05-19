using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    [SerializeField] private float attackSpeed;
    private PlayerMovement playerMove;
    private float attackCooldownTimer = float.MaxValue;

    public Animator anima;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    [SerializeField] public float attackDamage;
    public LayerMask enemyLayer;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
            Attack();

        attackCooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        // Play animation
        anima.SetTrigger("melee");
        attackCooldownTimer = 0;

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }

    }
}
