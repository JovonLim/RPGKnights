using UnityEngine;
using UnityEngine.UI;


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
    public static bool spellUnlock = false;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Spell defaultSpell;
    [SerializeField] private GameObject classHud;
    public Spell spellToCast = SpellHolder.activeSpells[0];
    
    [SerializeField] private enum Class
    {
        melee, ranged, mage,
    }

    private Class classType;

    private void Awake()
    {
        classType = Class.melee;
        classHud.transform.GetChild(0).GetComponent<Image>().enabled = true;
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
        resetTimer = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchMelee();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchArcher();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchMage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellToCast = SpellHolder.activeSpells[0];
            GetComponent<SpellHud>().InitializeSpell();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellToCast = SpellHolder.activeSpells[1];
            GetComponent<SpellHud>().InitializeSpell();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellToCast = SpellHolder.activeSpells[2];
            GetComponent<SpellHud>().InitializeSpell();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            spellToCast = SpellHolder.activeSpells[3];
            GetComponent<SpellHud>().InitializeSpell();
        }

        switch (classType)
        {
            case Class.melee:
                {
                    // Melee Attack
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
                    {
                        MeleeAttack();
                    }
                   
                }
                break;
            case Class.ranged:
                {
                    // Ranged Attack
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
                    {
                        RangedAttack();
                    }
                }
                break;
            case Class.mage:
                {
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > defaultSpell.spell.cooldownTime && playerMove.canAttack())
                    {
                        attackCooldownTimer = 0;
                        CastDefaultSpell();
                    }
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q) && spellTimer > spellToCast.spell.cooldownTime && playerMove.canAttack())
        {
            spellTimer = 0;
            CastSpell();
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
        GameObject projectile = Instantiate(arrow, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    public void AddAttack(float amt)
    {
        attackDamage += amt;
    }

    public void SubtractAttack(float amt)
    {
        attackDamage -= amt;
    }

    public void AddAttackSpeed(float amt)
    {
        attackSpeed -= amt;
    }

    public void SubtractAttackSpeed(float amt)
    {
        attackSpeed += amt;
    }

    private void CastSpell()
    {
        anima.SetTrigger("cast1");
    }
    private void CastDefaultSpell()
    {
        anima.SetTrigger("cast");
    }
    private void LaunchSpell()
    { 
        if (spellToCast != null)
        {
            if (spellToCast.GetComponent<ProjectileBased>() != null)
            {
                spellToCast.GetComponent<ProjectileBased>().dir = transform.localScale.x;
                float distance = spellToCast.GetComponent<ProjectileBased>().offset + projectileLaunchPoint.position.y;
                Instantiate(spellToCast, new Vector2(projectileLaunchPoint.position.x, distance), Quaternion.identity);
            }
            else if (spellToCast.GetComponent<SpawnBased>() != null)
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
        
     
    private void LaunchDefault()
    {
        
        defaultSpell.GetComponent<ProjectileBased>().dir = transform.localScale.x;
        float distance = defaultSpell.GetComponent<ProjectileBased>().offset + projectileLaunchPoint.position.y;
        Instantiate(defaultSpell, new Vector2(projectileLaunchPoint.position.x, distance), Quaternion.identity);
    }


    private void RotateSpell(int num)
    {
        spellToCast = SpellHolder.activeSpells[num];
    }

    private void SwitchMage()
    {
        if (spellUnlock)
        {
            classType = Class.mage;
            classHud.transform.GetChild(0).GetComponent<Image>().enabled = false;
            classHud.transform.GetChild(1).GetComponent<Image>().enabled = false;
            classHud.transform.GetChild(2).GetComponent<Image>().enabled = true;
        }
    }

    private void SwitchArcher()
    {
        if (rangedUnlock)
        {
            classType = Class.ranged;
            classHud.transform.GetChild(0).GetComponent<Image>().enabled = false;
            classHud.transform.GetChild(1).GetComponent<Image>().enabled = true;
            classHud.transform.GetChild(2).GetComponent<Image>().enabled = false;
        }
    }

    private void SwitchMelee()
    {
        classType = Class.melee;
        classHud.transform.GetChild(0).GetComponent<Image>().enabled = true;
        classHud.transform.GetChild(1).GetComponent<Image>().enabled = false;
        classHud.transform.GetChild(2).GetComponent<Image>().enabled = false;
    }
   
}
