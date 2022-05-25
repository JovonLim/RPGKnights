using UnityEngine;


public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackSpeed;
    private PlayerMovement playerMove;
    private float attackCooldownTimer = float.MaxValue;

    public Animator anima;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    [SerializeField] public float attackDamage;
    public LayerMask enemyLayer;

    private int combo = 0;
    private float resetTimer;

    private bool isMelee = false;
    private bool isRange = false;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] GameObject prefab = null;

    [SerializeField] private GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    // public static PlayerMelee instance;
    // public bool isAttacking = false;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
        resetTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Melee Attack
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack() && !isRange)
        {
            isMelee = true;
            MeleeAttack();
        }

        // Ranged Attack
        if (Input.GetMouseButtonDown(1) && attackCooldownTimer > attackSpeed && playerMove.canAttack() && !isMelee)
        {
            isRange = true;
            RangedAttack();
        }
      
        resetTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime;
    }
    
    void MeleeAttack()
    {
        if (resetTimer > 4.0f)
        {
            combo = 0;
        }
        combo++;
        if (combo == 1)
        {
            anima.SetTrigger("melee1");
        }

        else if (combo == 2)
        {
            anima.SetTrigger("melee2");
        }

        else if (combo == 3)
        {
            anima.SetTrigger("melee3");
            combo = 0;
            
        }
        resetTimer = 0;
        attackCooldownTimer = 0;

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }
        isMelee = false;

    }

    void RangedAttack()
    {
        isRange = false;
        anima.SetTrigger("ranged");
    }

    private void LaunchProjectile()
    {
        attackCooldownTimer = 0;

        // Launch the projectile
        GameObject projectile = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<EnemyProjectile>().ActivateProjectile();
    }


}
