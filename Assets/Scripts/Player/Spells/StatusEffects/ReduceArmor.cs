using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceArmor : StatusEffects
{
    [SerializeField] float reduction;

    protected override void ApplyEffect()
    {
        enemy.GetComponent<EnemyHealth>().SubtractPhysicalDefense(reduction);
    }

    protected override void EndEffect()
    {
        enemy.GetComponent<EnemyHealth>().AddPhysicalDefense(reduction);
    }
}
