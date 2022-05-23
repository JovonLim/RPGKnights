using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVaultClear : MonoBehaviour
{
    GameObject[] enemies;
    bool cleared;
    int enemiesDefeated;
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject transporter;


    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesDefeated = 0;
        chest1.SetActive(false);
        chest2.SetActive(false);
        transporter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cleared)
        {
            giveRewards();
            Destroy(gameObject);
        }

       

        if (checkProgress())
        {
            cleared = true;
        } else
        {
            enemiesDefeated = 0;
        }
    }

    bool checkProgress()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Health>().IsDefeated())
                enemiesDefeated++;
        }

        return enemiesDefeated == enemies.Length;
        
        
    }

    void giveRewards()
    {
        chest1.SetActive(true);
        chest2.SetActive(true);
        transporter.SetActive(true);
        UI.coins += 100;
    }
}
