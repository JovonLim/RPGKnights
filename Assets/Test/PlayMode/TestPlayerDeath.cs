using System.Collections;
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
        // Damage indicator required for playtest scene
        health.prefab = Resources.Load("DamageParent") as GameObject;
        health.AddPhysicalDefense(def);
        return health;
    }

    // Damage taken is more than total health due to insufficient defense, player dies
    [UnityTest]
    public IEnumerator PlayerDiestoPhysicalDamageEnumeratorPasses()
    {
        PlayerHealth healthSystem = MakeHealthSystem(5);
        yield return new WaitForSeconds(1);
        healthSystem.TakePhysicalDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }

    // Damage taken is lesser than total health due to sufficient defense, player survives
    [UnityTest]
    public IEnumerator PlayerSurvivesFromPhysicalDamageEnumeratorPasses()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        yield return new WaitForSeconds(1);
        healthSystem.TakePhysicalDamage(6f);
        Assert.IsFalse(healthSystem.IsDefeated());
    }

    // Damage taken is not physical, player dies regardless of physical defense
    [UnityTest]
    public IEnumerator PlayerDiesToMagicDamageEnumeratorPasses()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        yield return new WaitForSeconds(1);
        healthSystem.TakeMagicDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }

    // Damage taken is not physical, player dies regardless of physical defense
    [UnityTest]
    public IEnumerator PlayerDiesToTrueDamageEnumeratorPasses()
    {
        PlayerHealth healthSystem = MakeHealthSystem(50);
        yield return new WaitForSeconds(1);
        healthSystem.TakeTrueDamage(6f);
        Assert.IsTrue(healthSystem.IsDefeated());
    }
}
