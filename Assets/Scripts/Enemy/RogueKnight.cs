using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueKnight : MeleeAndRanged
{
    [SerializeField] Transform projectileLaunchPoint;
    public override void LaunchProjectile()
    {
        GameObject projectile = Instantiate(prefab, projectileLaunchPoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        projectile.GetComponent<EnemyProjectile>().enemyDamage = damage;
        projectile.GetComponent<EnemyProjectile>().ActivateProjectile();
    }
}
