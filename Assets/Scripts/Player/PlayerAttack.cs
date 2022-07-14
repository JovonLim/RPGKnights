using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlayerAttack : MonoBehaviour, IDataPersistence
{

    [SerializeField] private float meleeAttackSpeed;
    [SerializeField] private float rangedAttackSpeed;
    private PlayerMovement playerMove;
    private float attackCooldownTimer = float.MaxValue;
 

    public Animator anima;
    public Transform attackPoint;
    public float attackRange;
    [SerializeField] private float meleeAttackDamage;
    [SerializeField] private float rangedAttackDamage;
    [SerializeField] private BoxCollider2D boxCollider;
    public LayerMask enemyLayer;

    private int combo = 0;
    private int arrowCount = 0;
    private int swordCount = 0;
    private float resetTimer;
    private float spellTimer;

    
    public static bool rangedUnlock = false;
    public static bool spellUnlock = false;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Spell defaultSpell;
    [SerializeField] private GameObject classHud;
    private Image attack;
    private Image attackDark;
    private bool attacked = false;
    private float cooldown;
    public static Spell spellToCast = SpellHolder.activeSpells[0];
    public static bool[] rangedPassives = new bool[2];
    public static bool[] meleePassives = new bool[2];
    
    [SerializeField] private enum Class
    {
        melee, ranged, mage,
    }

    private Class classType;

    private void Awake()
    {
        classType = Class.melee;
        InitializeAttack();
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
        resetTimer = 0;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * attackRange,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (classType != Class.melee)
            {
                classType = Class.melee;
                InitializeAttack();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (rangedUnlock)
            {
                if (classType != Class.ranged) {
                    classType = Class.ranged;
                    InitializeAttack();
                }
            } 
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (spellUnlock)
            {
                if (classType != Class.mage)
                {
                    classType = Class.mage;
                    InitializeAttack();
                }                           
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (spellToCast != SpellHolder.activeSpells[0])
            {
                spellToCast = SpellHolder.activeSpells[0];
                UpdateSpellHUD();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (spellToCast != SpellHolder.activeSpells[1])
            {
                spellToCast = SpellHolder.activeSpells[1];
                UpdateSpellHUD();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            if (spellToCast != SpellHolder.activeSpells[2])
            {
                spellToCast = SpellHolder.activeSpells[2];
                UpdateSpellHUD();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            if (spellToCast != SpellHolder.activeSpells[3])
            {
                spellToCast = SpellHolder.activeSpells[3];
                UpdateSpellHUD();
            }
        }

        switch (classType)
        {
            case Class.melee:
                {
                    // Melee Attack
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > meleeAttackSpeed && playerMove.canAttack())
                    {
                        MeleeAttack();
                        attacked = true;
                    }
                   
                }
                break;
            case Class.ranged:
                {
                    // Ranged Attack
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > rangedAttackSpeed && playerMove.canAttack())
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
                        attacked = true;
                    }
                }
                break;
        }

        if (spellToCast != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && spellTimer > spellToCast.spell.cooldownTime && playerMove.canAttack())
            {
                spellTimer = 0;
                CastSpell();
            }
        }
        ShowCooldown();
         
        
        spellTimer += Time.deltaTime;
        resetTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime;
    }

    void UpdateSpellHUD()
    {
        spellTimer = 0;
        GetComponent<SpellHud>().InitializeSpell();
        GetComponent<SpellHud>().SetCooldown();
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
            if (meleePassives[0])
            {
                StartCoroutine(Increase(1));
            }
            anima.SetTrigger("melee2");
        }

        else if (combo == 3)
        {
            if (meleePassives[0])
            {
                StartCoroutine(Increase(2));
            }
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
            enemy.GetComponent<EnemyHealth>().TakePhysicalDamage(meleeAttackDamage);
            if (meleePassives[1])
            {
                swordCount += 1;
            }     
        }

        if (swordCount >= 5)
        {
            GetComponent<PlayerHealth>().GainHealth(0.5f);
            swordCount -= 5;
        }
      

    }

    IEnumerator Increase(int num)
    {
        meleeAttackDamage += 0.5f * num;
        yield return new WaitForSeconds(meleeAttackSpeed - 0.2f);
        meleeAttackDamage -= 0.5f * num;
    }

    void RangedAttack()
    {
        anima.SetTrigger("ranged");
    }

    private void LaunchProjectile()
    {
        
        arrowCount += 1;

        if (arrowCount == 2 && rangedPassives[0])
        {
            rangedAttackDamage += 0.5f;
            Launch();
            rangedAttackDamage -= 0.5f;
        } else
        {
            Launch();
        }       
    }

    private void Launch()
    {
        
        GameObject projectile = Instantiate(arrow, projectileLaunchPoint.position, Quaternion.identity);
        if (arrowCount == 2)
        {
            if (rangedPassives[1])
            {
                projectile.GetComponent<Projectile>().damageType = Damage.Dmg.magic;
            }
            arrowCount = 0;
        }
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<Projectile>().projectileDamage = rangedAttackDamage;
        projectile.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        attackCooldownTimer = 0;
        attacked = true;
    }

    public float GetRangedAttack()
    {
        return rangedAttackDamage;
    }

    public void AddRangedAttack(float amt)
    {
        rangedAttackDamage += amt;
    }
    public void SubtractRangedAttack(float amt)
    {
        rangedAttackDamage -= amt;
    }

    public float GetRangedAttackSpeed()
    {
        return rangedAttackSpeed;
    }
    public void AddRangedAttackSpeed(float amt)
    {
        rangedAttackSpeed -= amt;
    }
    public void SubtractRangedAttackSpeed(float amt)
    {
        rangedAttackSpeed += amt;
    }

    public float GetAttack()
    {
        return meleeAttackDamage;
    }
    // Add Melee Attack Damage
    public void AddAttack(float amt)
    {
        meleeAttackDamage += amt;
    }

    // Subtract Melee Attack Damage
    public void SubtractAttack(float amt)
    {
        meleeAttackDamage -= amt;
    }

    public float GetAttackSpeed()
    {
        return meleeAttackSpeed;
    }
    // Add Melee Attack Speed
    public void AddAttackSpeed(float amt)
    {
        meleeAttackSpeed -= amt;
    }

    // Subtract Melee Attack Speed
    public void SubtractAttackSpeed(float amt)
    {
        meleeAttackSpeed += amt;
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


    private void InitializeAttack()
    {
        int childCount = classHud.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            classHud.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
       
        switch (classType)
        {
            case Class.melee:
                {
                    attack = classHud.transform.GetChild(0).GetComponent<Image>();   
                    attackDark = classHud.transform.GetChild(3).GetComponent<Image>();                  
                }
                break;
            case Class.ranged:
                {
                    attack = classHud.transform.GetChild(1).GetComponent<Image>();
                    attackDark = classHud.transform.GetChild(4).GetComponent<Image>();
                }
                break;
            case Class.mage:
                {
                    attack = classHud.transform.GetChild(2).GetComponent<Image>();
                    attackDark = classHud.transform.GetChild(5).GetComponent<Image>();
                }
                break;
        }
        attack.enabled = true;
        attackDark.enabled = true;
        attackDark.type = Image.Type.Filled;
        attackDark.fillClockwise = false;
        attackDark.fillOrigin = (int)Image.Origin360.Top;
        attackDark.fillAmount = 0;
        attackCooldownTimer = 0;
        attacked = true;
    }

    private void ShowCooldown()
    {
        if (attacked)
        {   
            attackDark.fillAmount = 1;
            attacked = false;
        }  
        
        switch (classType)
        {
            case Class.melee:
                {
                    cooldown = meleeAttackSpeed;                                     
                }
                break;
            case Class.ranged:
                {
                    cooldown = rangedAttackSpeed;
                }
                break;
            case Class.mage:
                {
                    cooldown = defaultSpell.spell.cooldownTime;
                }
                break;
        }
        attackDark.fillAmount -= 1 / cooldown * Time.deltaTime;
        if (attackDark.fillAmount <= 0)
        {
            attackDark.fillAmount = 0;
        }    
    }

    public void LoadData(GameData data)
    {
        rangedUnlock = data.rangedUnlocked;
        spellUnlock = data.spellUnlocked;
        meleePassives = data.knightPurchased;
        rangedPassives = data.archerPurchased;
    }

    public void SaveData(GameData data)
    {
        data.rangedUnlocked = rangedUnlock;
        data.spellUnlocked = spellUnlock;
    }
}
