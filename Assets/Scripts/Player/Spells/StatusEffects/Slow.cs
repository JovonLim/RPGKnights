using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffects
{
    [SerializeField] float speedToReduce;

    protected override void ApplyEffect()
    {
        enemy.GetComponent<Aggro>().speed -= speedToReduce;
    }

    protected override void EndEffect()
    {
        enemy.GetComponent<Aggro>().speed += speedToReduce;
        base.EndEffect();
    }
}
