using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffects
{
    [SerializeField] float damage;

    protected override void ApplyEffect()
    {
       enemy.GetComponent<Health>().TakeMagicDamage(damage);
    }
}

