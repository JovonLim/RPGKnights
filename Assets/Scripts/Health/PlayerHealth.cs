using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    
    private void Awake()
    {
       
        currentHealth = startingHealth;
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 7 ||
            SceneManager.GetActiveScene().buildIndex == 13)
        {
            currentHealth = startingHealth;
        }

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }


    public override void TakePhysicalDamage(float damage)
    {
        float netDamage;
        if (damage == 0)
        {
            netDamage = 0;
        }
        else
        {
            float reduction = PhysicalDefense * 0.1f;
            netDamage = damage - reduction;
            if (netDamage <= 0)
            {
                netDamage = minDmg;
            }
        }
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().text = netDamage.ToString("F1");
        ApplyDmg(netDamage);
    }

    public override void TakeMagicDamage(float damage)
    {
        float netDamage;
        if (damage == 0)
        {
            netDamage = 0;
        }
        else
        {
            float reduction = MagicDefense * 0.1f;
            netDamage = damage - reduction;
            if (netDamage <= 0)
            {
                netDamage = minDmg;
            }
        }
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().color = Color.blue;
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().text = netDamage.ToString("F1");
        ApplyDmg(netDamage);
    }

    public override void TakeTrueDamage(float damage)
    {
        GameObject damageInd = Instantiate(prefab, transform.position, Quaternion.identity);
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
        damageInd.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString("F1");
        ApplyDmg(damage);
    }

    public override void ApplyDmg(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            // Player is hurt
            anima.SetTrigger("hurt");
        }
        else
        {
            // Player will die once it has no health
            if (!isDead)
            {
                OnDeath();
                isDead = true;
            }
        }
    }

    public override void OnDeath()
    {
        anima.SetTrigger("die");

        // For enemy
        if (GetComponent<MeleeEnemy>() != null)
        {
            GetComponent<MeleeEnemy>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();

        }

        if (GetComponent<RangeEnemy>() != null)
        {
            GetComponent<RangeEnemy>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();

        }

        if (GetComponent<MeleeAndRanged>() != null)
        {
            GetComponent<MeleeAndRanged>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CoinSpawn>().Spawn();

        }
        if (GetComponent<Aggro>() != null)
        {
            GetComponentInParent<Aggro>().enabled = false;
        }

        // For player
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerDeath>().Respawn();
        }
    }

    public override void GainHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth + damage, 1, startingHealth);
    }

    public override bool IsDefeated()
    {
        return isDead;
    }

    // Gain permanent health
    public override void AddHealth(float amt)
    {
        startingHealth += amt;
        currentHealth = Mathf.Clamp(currentHealth + amt, 1, startingHealth);
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }

    public override void AddPhysicalDefense(float amt)
    {
        PhysicalDefense += amt;
    }

    public override void AddMagicDefense(float amt)
    {
        MagicDefense += amt;
    }

    public override void AddDefense(float amt)
    {
        PhysicalDefense += amt;
        MagicDefense += amt;
    }

    public override void SubtractHealth(float amt)
    {
        startingHealth -= amt;
        if (startingHealth < currentHealth)
        {
            currentHealth = startingHealth;
        }
    }

    public override void SubtractPhysicalDefense(float amt)
    {
        PhysicalDefense -= amt;
    }

    public override void SubtractMagicDefense(float amt)
    {
        MagicDefense -= amt;
    }

    public override void SubtractDefense(float amt)
    {
        PhysicalDefense -= amt;
        MagicDefense -= amt;
    }

}
