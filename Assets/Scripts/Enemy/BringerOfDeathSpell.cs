using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathSpell :EnemyDamage
{ 
    [SerializeField] private float projectileResetTime;
    private float projectileLifetime;
    private Collider2D boxCollider;
    private bool hit = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        
        projectileLifetime = 0;
        gameObject.SetActive(true);

    }
    private void Update()
    {
        projectileLifetime += Time.deltaTime;
        if (projectileLifetime > projectileResetTime)
            Deactivate();
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            boxCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hit = false;
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Impact()
    {
        if (hit)
        {
            base.OnTriggerEnter2D(boxCollider);
        }
        yield return new WaitForSeconds(2f);
        Deactivate();
    }
}

