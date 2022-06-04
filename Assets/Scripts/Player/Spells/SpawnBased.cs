using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBased : Spell
{

    private BoxCollider2D boxCollider;
    private Animator anima;
    private float resetTime;  
    public float range;
    

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        resetTime += Time.deltaTime;
        if (resetTime > spell.activeTime)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.layer == 10)
        {
            if (GameObject.Find("Arcane Archer") == null || collison.gameObject != GameObject.Find("Arcane Archer"))
            {
                boxCollider.enabled = false;
                collison.GetComponent<Health>().TakeDamage(spell.damage);
                anima.SetTrigger("explode");
                StartCoroutine(Impact());
            }
        }

    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Impact()
    {
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }
}
