using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefUp : StatusEffects
{
   
    [SerializeField] private float defAmt;
    GameObject player;
    protected override void ApplyEffect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().AddPhysicalDefense(defAmt);
    }

    protected override void EndEffect()
    {
        player.GetComponent<PlayerHealth>().SubtractPhysicalDefense(defAmt);

    }
}
