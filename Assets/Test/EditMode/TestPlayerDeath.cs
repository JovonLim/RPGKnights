using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerDeath
{
    // Returns the PlayerHealthComponent with physical defense values set 
    public PlayerHealth MakeHealthSystem(float def)
    {
        var player = new GameObject();
        var health = player.AddComponent<PlayerHealth>();
        health.SetTestHealth(5);
        // Damage indicator required for script 
        health.prefab = Resources.Load("DamageParent") as GameObject;
        health.AddPhysicalDefense(def);
        return health;
    }

    // Damage taken is more than total health due to insufficient defense, player dies
    [Test]
    public void PlayerDiestoPhysicalDamage()
    {
        PlayerHealth healthSystem = MakeHealthSystem(5);
        healthSystem.TakePhysicalDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }

    // Damage taken is lesser than total health due to sufficient defense, player survives
    [Test]
    public void PlayerSurvivesFromPhysicalDamage()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        healthSystem.TakePhysicalDamage(6f);
        Assert.IsFalse(healthSystem.IsDefeated());
    }

    // Damage taken is not physical, player dies regardless of physical defense
    [Test]
    public void PlayerDiesToMagicDamage()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        healthSystem.TakeMagicDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }

    // Damage taken is not physical, player dies regardless of physical defense
    [Test]
    public void PlayerDiesToTrueDamage()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        healthSystem.TakeTrueDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }
}
