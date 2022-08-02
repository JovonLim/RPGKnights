using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEnd : MonoBehaviour
{
    [SerializeField] GameObject[] questRewards;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject endingText;
    [SerializeField] int coins;
    public bool cleared;

    public enum NPC
    {
        wizard, archer,
    }

    public NPC npc;
    // Start is called before the first frame update
    void Start()
    {
        if (questRewards.Length != 0)
        {
            foreach (GameObject reward in questRewards)
            {
                reward.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     
        if (!cleared)
        {
            if (GetComponent<QuestProgress>().questCompleted)
            {
                StartCoroutine(giveRewards());
                ClearQuest();
                cleared = true;
            }
        }
       
    }


    IEnumerator giveRewards()
    {
        foreach (GameObject reward in questRewards)
        {
            reward.SetActive(true);
        }
        CoinCounter.coins += coins;
        questDialog.SetActive(true);
        endingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        endingText.SetActive(false);
    }

    void ClearQuest()
    {
        if (npc == NPC.wizard)
        {
            DataPersistenceManager.instance.gameData.wizardQuests[PlayerQuestInteraction.WizardQuest.questNum] = true;
            PlayerQuestInteraction.WizardQuest = null;
        }
        else
        {
            DataPersistenceManager.instance.gameData.archerQuests[PlayerQuestInteraction.ArcherQuest.questNum] = true;
            PlayerQuestInteraction.ArcherQuest = null;
        }
     
        PlayerQuestInteraction.questActive = false;
    }
}
