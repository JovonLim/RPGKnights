using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : Damage
{
    [SerializeField] private float enemyDamage;
    private GameObject player;
    private bool hit = false;
    private Animator anima;

    private void Start()
    {
        anima = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetTrigger("explode");
        if (collision.CompareTag("Player"))
        {
            hit = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hit = false;
        }
    }
    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Impact()
    {
        if (hit)
        {
            if (damageType == Dmg.magic)
            {
                player.GetComponent<Health>().TakeMagicDamage(enemyDamage);
            }
            else if (damageType == Dmg.physical)
            {
                player.gameObject.GetComponent<Health>().TakePhysicalDamage(enemyDamage);
            }
            else
            {
                player.gameObject.GetComponent<Health>().TakeTrueDamage(enemyDamage);
            }
        }
        yield return new WaitForSeconds(1f);
        Deactivate();
    }
}

