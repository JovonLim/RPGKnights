using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVaultClear : MonoBehaviour
{
    GameObject[] enemies;
    bool cleared;
    bool questCompleted = false;
    int enemiesDefeated;
    GameObject[] questRewards = new GameObject[3];
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject transporter;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject startingText;
    [SerializeField] GameObject endingText;


    // Start is called before the first frame update
    void Start()
    {
        questRewards[0] = chest1;
        questRewards[1] = chest2;
        questRewards[2] = transporter;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesDefeated = 0;
        foreach(GameObject reward in questRewards)
        {
            reward.SetActive(false);
        }
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
        if (!questCompleted)
        {
            if (cleared)
            {
                StartCoroutine(giveRewards());
                questCompleted = true;
            }



            if (checkProgress())
            {
                cleared = true;
            }
            else
            {
                enemiesDefeated = 0;
            }
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

    IEnumerator Begin()
    {
        questDialog.SetActive(true);
        startingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        startingText.SetActive(false);
    }

    IEnumerator giveRewards()
    {
        foreach (GameObject reward in questRewards)
        {
            reward.SetActive(true);
        }
        UI.coins += 100;
        questDialog.SetActive(true);
        endingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        endingText.SetActive(false);
    }

}
