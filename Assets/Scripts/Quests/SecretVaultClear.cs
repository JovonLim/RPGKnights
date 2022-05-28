using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVaultClear : MonoBehaviour
{
    GameObject[] mobs;
    bool cleared;
    bool questCompleted = false;
    [SerializeField] GameObject[] questRewards;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject startingText;
    [SerializeField] GameObject endingText;


    // Start is called before the first frame update
    void Start()
    {
        mobs = GameObject.FindGameObjectsWithTag("Enemy");
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
                PlayerInteraction.questActive = false;
            }

            if (checkProgress())
            {
                cleared = true;
            }
           
        }
    }
        

    bool checkProgress()
    {
        int defeated = 0;
        foreach (GameObject mob in mobs)
        {
            if (mob.GetComponent<Health>().IsDefeated())
            {
                defeated++;
            }
        }

        return defeated == mobs.Length;
        
        
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
