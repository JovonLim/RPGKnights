using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : StatusEffects
{
    [SerializeField] private float speedAmt;
    GameObject player;
    protected override void ApplyEffect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().AddSpeed(speedAmt);
    }

    protected override void EndEffect()
    {
        player.GetComponent<PlayerMovement>().SubtractSpeed(speedAmt);
        base.EndEffect();
    }
}
