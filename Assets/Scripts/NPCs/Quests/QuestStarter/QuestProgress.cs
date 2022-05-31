using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgress : QuestObjective
{
    public bool questCompleted = false;
    GameObject[] questObjects;


    // Start is called before the first frame update
    void Start()
    {
       if (objective == Objective.chests)
        {
            questObjects = GameObject.FindGameObjectsWithTag("chests");
        } else if (objective == Objective.boss)
        {
            questObjects = GameObject.FindGameObjectsWithTag("Boss");
        } else
        {
            questObjects = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!questCompleted)
        {
            if (checkProgress())
            {
                questCompleted = true;
            }
        }
    }

    bool checkProgress()
    {
        int count = 0;
        if (objective == Objective.chests)
        {
            foreach (GameObject chest in questObjects)
            {
                if (chest.GetComponent<Chests>().chestOpened)
                {
                    count++;
                }
            }
        }
        else
        {
            foreach (GameObject mob in questObjects)
            {
                if (mob.GetComponent<Health>().IsDefeated())
                {
                    count++;
                }
            }
        }
        return count == questObjects.Length;
    }
}