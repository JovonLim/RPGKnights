using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEmerges : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject bossPatrol;
    bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.questActive && PlayerInteraction.ArcherQuest != null)
        {
            if (PlayerInteraction.ArcherQuest.questNum == 0)
            {
                door.SetActive(false);
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }

        if (!unlocked && GetComponent<QuestProgress>().questCompleted)
        {
           PlayerAttack.rangedUnlock = true;
           unlocked = true;
        } 
    }
}

