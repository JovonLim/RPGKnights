using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public static int scrollsUsed = 0;
    [SerializeField] private float startingAmt;
    private void Start()
    {
        startingHealth = startingAmt + scrollsUsed * 1.0f;
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
            if (anima != null)
            {
                anima.SetTrigger("hurt");
            }
        
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
      
        // For player
        if (GetComponent<PlayerMovement>() != null)
        {
            anima.SetTrigger("die");
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

    public override void LoadData(GameData data)
    {
        scrollsUsed = data.scrollsUsed;
    }

    public override void SaveData(GameData data)
    {
        data.scrollsUsed = scrollsUsed;
    }

    public void SetTestHealth(float amt)
    {
        startingHealth = amt;
        currentHealth = amt;
    }
}
