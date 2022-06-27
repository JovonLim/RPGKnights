using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRival : MonoBehaviour
{
    [SerializeField] GameObject bossPatrol;
    [SerializeField] GameObject bow;
    bool added;

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest != null)
        {
            if (PlayerQuestInteraction.ArcherQuest.questNum == 2)
            {
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }

        if (!added)
        {
            if (GetComponent<QuestEnd>().cleared)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject reward = Instantiate(bow);
                player.GetComponent<Inventory>().AddItem(reward);
                added = true;
            }
        }

    }
}
