using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MeleeAndRanged
{
    [SerializeField] private Transform projectileLaunchPoint;
    [SerializeField] private Transform specialLaunchPoint;
    [SerializeField] private GameObject laser;
    private bool usedSpecial;
    private bool gotArmor;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (GetComponent<EnemyHealth>().currentHealth <= 15)
        {
            if (!usedSpecial)
            {
                anima.SetTrigger("specialAttack");
                usedSpecial = true;
            }
            
        } else if (GetComponent<EnemyHealth>().currentHealth <= 25)
        {
            if (!gotArmor)
            {
                GainArmor();
                gotArmor = true;
            }
        }
    }

    public override void LaunchProjectile()
    {
        GameObject projectile = Instantiate(prefab, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        projectile.GetComponent<EnemyProjectile>().enemyDamage = rangedDamage;
        projectile.GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private void GainArmor()
    {
        anima.SetTrigger("armor");
        GetComponent<EnemyHealth>().AddPhysicalDefense(1f);
    }
    IEnumerator Special()
    {
        GetComponent<Aggro>().enabled = false;
        GameObject projectile = Instantiate(laser, specialLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        yield return new WaitForSeconds(1);
        GetComponent<Aggro>().enabled = true;
    }
}
