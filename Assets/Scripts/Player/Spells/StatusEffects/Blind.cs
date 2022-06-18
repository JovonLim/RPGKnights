using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : StatusEffects
{
    [SerializeField] float damage;
    protected override void ApplyEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {

            damage = enemy.GetComponent<MeleeAndRanged>().damage;
            enemy.GetComponent<MeleeAndRanged>().damage = 0;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            damage = enemy.GetComponent<MeleeEnemy>().damage;
            enemy.GetComponent<MeleeEnemy>().damage = 0;
        }
    }

    protected override void EndEffect()
    {
        if (enemy.GetComponent<MeleeAndRanged>() != null)
        {
            enemy.GetComponent<MeleeAndRanged>().damage = damage;
        }
        else if (enemy.GetComponent<MeleeEnemy>() != null)
        {
            enemy.GetComponent<MeleeEnemy>().damage = damage;
        }
        base.EndEffect();
    }
}