using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladekeeper : MeleeAndRanged
{
 
    [SerializeField] Transform projectileLaunchPoint;
    private int combo = 0;
    private float resetTimer;
    private float SpecialDamage;
  

    // Start is called before the first frame update
    void Start()
    {
        resetTimer = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
   
        attackCooldownTimer += Time.deltaTime;
        if (PlayerInMeleeSight())
        {
            resetTimer = 0;
            if (attackCooldownTimer >= attackSpeed)
            {
                MeleeAttack();
            }
        }
        else
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > 10f)
            {
                attackSpeed = 1;
                combo = 0;
            }
        }

        if (PlayerInRangedSight())
        {
            if (attackCooldownTimer >= attackSpeed)
            {
                attackCooldownTimer = 0;
                anima.SetTrigger("rangedAttack");

            }
        }
    }

    private void SpecialDamagePlayer()
    {
        if (PlayerInMeleeSight())
        {
            if (damageType == Dmg.physical)
            {
                playerHealth.TakePhysicalDamage(SpecialDamage);
            }
        }
    }
    void MeleeAttack()
    {
        combo++;
        if (combo == 1)
        {
            anima.SetTrigger("atk1");
            
        }

        else if (combo == 2)
        {
            anima.SetTrigger("atk2");
            
        }

        else if (combo == 3)
        {
            anima.SetTrigger("atk3");
            StartCoroutine(AddAtkSpeed());
        }
        else if (combo == 4)
        {
            anima.SetTrigger("specialAtk");
            StartCoroutine(AddAtkSpeed());
            combo = 0;
        }
        attackCooldownTimer = 0;
    }

    IEnumerator AddAtkSpeed()
    {
        attackSpeed += 1f;
        yield return new WaitForSeconds(2);
        attackSpeed -= 1f;
    }

    public override void LaunchProjectile()
    {
        attackCooldownTimer = 0;

        // Launch the projectile
        GameObject knife = Instantiate(this.prefab, projectileLaunchPoint.position, Quaternion.identity);
        knife.GetComponent<EnemyProjectile>().enemyDamage = rangedDamage;
        knife.GetComponent<EnemyProjectile>().ActivateProjectile();
    }

}
