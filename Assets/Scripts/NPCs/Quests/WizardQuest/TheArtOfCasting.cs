using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheArtOfCasting : MonoBehaviour
{
    [SerializeField] GameObject bossPatrol;
    bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        bossPatrol.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.WizardQuest != null)
        {
            if (PlayerQuestInteraction.WizardQuest.questNum == 1)
            {
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }

        if (!unlocked && GetComponent<QuestProgress>().questCompleted)
        {
            PlayerAttack.spellUnlock = true;
            unlocked = true;
        }
    }
}
