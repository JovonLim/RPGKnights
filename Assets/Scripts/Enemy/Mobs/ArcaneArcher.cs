using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcher : RangeEnemy
{
    public override void Awake()
    {
        base.Awake();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            anima.SetTrigger("roll");
        }
    }
}

