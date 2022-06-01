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

    private bool isMelee = true;
    private bool isRange = false;
    public static bool rangedUnlock = false;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
        resetTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && rangedUnlock)
        {
            SwitchStance();
        }
        // Melee Attack
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack() && isMelee)
        {
            MeleeAttack();
        }

        if (rangedUnlock)
        {
            // Ranged Attack
            if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack() && isRange)
            { 
                RangedAttack();
            }

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
      

    }

    void RangedAttack()
    {
        anima.SetTrigger("ranged");
    }

    private void LaunchProjectile()
    {
        attackCooldownTimer = 0;

        // Launch the projectile
        GameObject projectile = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    void SwitchStance()
    {
        isMelee = !isMelee;
        isRange = !isRange;
    }

    public void AddAttack()
    {
        attackDamage += 1;
    }
}
