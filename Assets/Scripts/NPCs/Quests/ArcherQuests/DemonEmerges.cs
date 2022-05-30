using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEmerges : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject bossPatrol;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject chest;
    [SerializeField] GameObject questDialog;
    bool questCompleted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.questActive && PlayerInteraction.ArcherQuest.questNum == 0)
        {
            door.SetActive(false);
            bossPatrol.SetActive(true);
        }

        if (!questCompleted)
        {
            if (boss.GetComponent<Health>().IsDefeated())
            {
                StartCoroutine(GiveRewards());
                questCompleted = true;
                ArcherQuestLog.added = false;
                ArcherQuestLog.questStatus[PlayerInteraction.ArcherQuest.questNum] = true;
                PlayerInteraction.questActive = false;
                PlayerInteraction.ArcherQuest = null;
                PlayerAttack.rangedUnlock = true;
            }
        }
       
    }

    IEnumerator GiveRewards()
    {
        UI.coins += 150;
        questDialog.SetActive(true);
        chest.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
      
    }
}

