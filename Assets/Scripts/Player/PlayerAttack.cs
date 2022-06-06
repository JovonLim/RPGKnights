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
    private float spellTimer;

    
    public static bool rangedUnlock = false;
    public static bool spellUnlock = true;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Spell spellToCast;
    
    

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
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
        {
            MeleeAttack();
        }

        if (rangedUnlock)
        {
            // Ranged Attack
            if (Input.GetMouseButtonDown(1) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
            { 
                RangedAttack();
            }

        }

        if (spellUnlock)
        {
            
            if (Input.GetKeyDown(KeyCode.Q) && spellTimer > spellToCast.spell.cooldownTime && playerMove.canAttack())
            {
                CastSpell();
                spellTimer = 0;
            }

        }
        spellTimer += Time.deltaTime;
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
            enemy.GetComponent<Health>().TakePhysicalDamage(attackDamage);
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

    public void AddAttack(float amt)
    {
        attackDamage += amt;
    }

    private void CastSpell()
    {
        anima.SetTrigger("cast");
    }
    private void LaunchSpell()
    {
        
        if (spellToCast.GetComponent<ProjectileBased>() != null)
        {
            spellToCast.GetComponent<ProjectileBased>().dir = transform.localScale.x;
            float distance = spellToCast.GetComponent<ProjectileBased>().offset + projectileLaunchPoint.position.y;
            Instantiate(spellToCast, new Vector2(projectileLaunchPoint.position.x, distance), Quaternion.identity);
        } else if (spellToCast.GetComponent<SpawnBased>() != null)
        {
            float distance = spellToCast.GetComponent<SpawnBased>().range * transform.localScale.x;
            Collider2D enemyInRange = Physics2D.OverlapArea(projectileLaunchPoint.position, 
                new Vector2(projectileLaunchPoint.position.x + distance, projectileLaunchPoint.position.y),
                enemyLayer);
            if (enemyInRange != null)
            Instantiate(spellToCast, enemyInRange.gameObject.transform.position, Quaternion.identity);
        }
    }

   
}
