using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcher : RangeEnemy
{
 
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerProjectile"))
        {
            anima.SetTrigger("roll");
        }
    }
}

