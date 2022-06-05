using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedSlow : StatusEffects
{
    [SerializeField] float atkSpeedToSlow;
    protected override void ApplyEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {
            enemy.GetComponent<MeleeAndRanged>().attackSpeed += atkSpeedToSlow;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<MeleeEnemy>().attackSpeed += atkSpeedToSlow;
        } else if (enemy.GetComponent<RangeEnemy>() != null)
        {
            enemy.GetComponent<RangeEnemy>().attackSpeed += atkSpeedToSlow;
        }
    }

    protected override void EndEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {
            enemy.GetComponent<MeleeAndRanged>().attackSpeed -= atkSpeedToSlow;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<MeleeEnemy>().attackSpeed -= atkSpeedToSlow;
        }
        else if (enemy.GetComponent<RangeEnemy>() != null)
        {
            enemy.GetComponent<RangeEnemy>().attackSpeed -= atkSpeedToSlow;
        }
        base.EndEffect();
    }
}
