using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MeleeAndRanged
{
    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private Transform specialLaunchPoint;
    [SerializeField] private GameObject laser;
    private Health health;
    private bool changedForm;
    private bool usedSpecial;

    public override void Awake()
    {
        base.Awake();
        health = GetComponent<Health>();
    }
    public override void Update()
    {
        base.Update();
        if (health.currentHealth <= 15)
        {
            if (!usedSpecial)
            {
                StartCoroutine(Special());
                usedSpecial = true;
            }
        }
        else if (health.currentHealth <= 25)
        {
            if (!changedForm)
            {
                Change();
                changedForm = true;
            }
        }
    }

    public override void LaunchProjectile()
    {
        attackCooldownTimer = 0;
        GameObject arm = Instantiate(prefab, projectileLaunchPoint.position, Quaternion.identity);
        arm.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        arm.GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private void Change()
    {
        anima.SetTrigger("armor");
        health.AddDefense(0.5f);
    }

    IEnumerator Special()
    {
        GetComponent<Aggro>().enabled = false;
        anima.SetTrigger("specialAttack");
        yield return new WaitForSeconds(0.6f);
        GameObject projectile = Instantiate(laser, specialLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        yield return new WaitForSeconds(1);
        GetComponent<Aggro>().enabled = true;
    }
}
