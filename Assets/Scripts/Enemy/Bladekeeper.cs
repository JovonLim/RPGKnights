using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladekeeper : MeleeAndRanged
{
    private int combo = 0;
    private float resetTimer;
    

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

        }
        else if (combo == 4)
        {
            anima.SetTrigger("specialAtk");
            
            combo = 0;
        }
        attackCooldownTimer = 0;
    }
}
