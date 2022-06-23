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
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!questCompleted)
        {
            if (questObjects == null)
            {
                if (objective == Objective.chests)
                {
                    questObjects = GameObject.FindGameObjectsWithTag("Chest");
                }
                else if (objective == Objective.boss)
                {
                    questObjects = GameObject.FindGameObjectsWithTag("QuestBoss");
                }
                else
                {
                    questObjects = GameObject.FindGameObjectsWithTag("Enemy");
                }
            }
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