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
    public float attackRange = 0.5f;
    [SerializeField] private float meleeAttackDamage;
    [SerializeField] private float rangedAttackDamage;
    public LayerMask enemyLayer;

    private int combo = 0;
    private int arrowCount = 0;
    private int swordCount = 0;
    private float resetTimer;
    private float spellTimer;

    
    public static bool rangedUnlock = true;
    public static bool spellUnlock = true;

    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Spell defaultSpell;
    [SerializeField] private GameObject classHud;
    public static Spell spellToCast = SpellHolder.activeSpells[0];
    public static Passives[] rangedPassives = new Passives[2];
    public static Passives[] meleePassives = new Passives[2];
    
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
                    if (Input.GetMouseButtonDown(0) && attackCooldownTimer > meleeAttackSpeed && playerMove.canAttack())
                    {
                        MeleeAttack();
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
            if (meleePassives[0] != null)
            {
                StartCoroutine(Increase(1));
            }
            anima.SetTrigger("melee2");
        }

        else if (combo == 3)
        {
            if (meleePassives[0] != null)
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
            swordCount += 1;
        }

        if (swordCount >= 5 && meleePassives[1] != null)
        {
            GetComponent<PlayerHealth>().GainHealth(0.2f);
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
        attackCooldownTimer = 0;
        arrowCount += 1;

        if (arrowCount == 2 && rangedPassives[0] != null)
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
            if (rangedPassives[1] != null)
            {
                projectile.GetComponent<Projectile>().damageType = Damage.Dmg.magic;
            }
            arrowCount = 0;
        }
        projectile.transform.localScale = new Vector3(projectileLaunchPoint.transform.localScale.x, 1, 1);
        projectile.GetComponent<Projectile>().projectileDamage = rangedAttackDamage;
        projectile.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    public void AddAttack(float amt)
    {
        meleeAttackDamage += amt;
    }

    public void SubtractAttack(float amt)
    {
        meleeAttackDamage -= amt;
    }

    public void AddAttackSpeed(float amt)
    {
        meleeAttackSpeed -= amt;
    }

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

    public void LoadData(GameData data)
    {
        rangedUnlock = data.rangedUnlocked;
        spellUnlock = data.spellUnlocked;
    }

    public void SaveData(GameData data)
    {
        data.rangedUnlocked = rangedUnlock;
        data.spellUnlocked = spellUnlock;
    }
}
