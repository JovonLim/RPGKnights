using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priestess : MeleeEnemy
{
    private int combo = 0;
    private float resetTimer;
    private bool usedHeal;
   
    // Start is called before the first frame update
    void Start()
    {
        resetTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().currentHealth < 20)
        {
            if (!usedHeal)
            {
                anima.SetTrigger("heal");
                GetComponent<Health>().GainHealth(15);
                usedHeal = true;
            }
            
        }
        attackCooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            resetTimer = 0;
            if (attackCooldownTimer >= attackSpeed)
            {
                MeleeAttack();
            }
        } else
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > 10f)
            {
                attackSpeed = 1;
                combo = 0;
            }
        }
        
    }

    void MeleeAttack()
    {
        combo++;
        if (combo == 1)
        {
            anima.SetTrigger("atk1");
            attackSpeed += 0.75f;
        }

        else if (combo == 2)
        {
            anima.SetTrigger("atk2");
            attackSpeed += 1f;
        }

        else if (combo == 3)
        {
            anima.SetTrigger("atk3");

        } else if (combo == 4)
        {
            StartCoroutine(AddDamage());
            anima.SetTrigger("specialAtk");
            attackSpeed -= 1.75f;
            combo = 0;
        }
        attackCooldownTimer = 0;
    }

    IEnumerator AddDamage()
    {
        damage += 2;
        yield return new WaitForSeconds(2.5f);
        damage -= 2;
    }
}
