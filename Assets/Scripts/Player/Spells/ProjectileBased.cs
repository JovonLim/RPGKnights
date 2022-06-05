using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBased : Spell
{

    private bool hit;
    private float resetTime;
    public float dir;
    public float offset;

    private BoxCollider2D boxCollider;
    private Animator anima;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            return;
        }

        // Move the projectile every frame if it does not hit anything
        transform.localScale = new Vector2(dir, transform.localScale.y);
        float movementSpeed = spell.speed * Time.deltaTime;
        transform.Translate(movementSpeed * dir, 0, 0);

        resetTime += Time.deltaTime;
        // If projectile does not hit anything for a duration, the projectile will be destroyed
        if (resetTime > spell.activeTime)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.layer == 10)
        {
            if (GameObject.Find("Arcane Archer") == null || collison.gameObject != GameObject.Find("Arcane Archer"))
            {
                hit = true;
                GetComponent<StatusEffects>().enemy = collison.gameObject;
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
