using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : StatusEffects
{
    [SerializeField] private float healAmt;

    protected override void ApplyEffect()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
       player.GetComponent<PlayerHealth>().GainHealth(healAmt);
         
        
    }

}
   
