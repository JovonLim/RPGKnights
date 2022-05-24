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

    private int combo = 0;
    private float resetTimer;

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

        if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
        {
            Attack();
        }
      
        resetTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime;
    }
    
    void Attack()
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
 

  
}
