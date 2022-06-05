using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffects
{
    protected override void ApplyEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {
            enemy.GetComponent<MeleeAndRanged>().enabled = false;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<MeleeEnemy>().enabled = false;
        }
        else if (enemy.GetComponent<RangeEnemy>() != null)
        {
            enemy.GetComponent<RangeEnemy>().enabled = false;
        }
        enemy.GetComponent<Aggro>().enabled = false;
    }

   
    protected override void EndEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {
            enemy.GetComponent<MeleeAndRanged>().enabled = true;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<MeleeEnemy>().enabled = true;
        }
        else if (enemy.GetComponent<RangeEnemy>() != null)
        {
            enemy.GetComponent<RangeEnemy>().enabled = true;
        }
        enemy.GetComponent<Aggro>().enabled = true;
        base.EndEffect();
    }
}
