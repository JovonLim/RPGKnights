using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : NPC
{
    [SerializeField] private GameObject classNotice;
    private bool secondBackStoryDone;
    
    public override void DisplayShop()
    {
        if (PlayerAttack.rangedUnlock)
        {
            shop.SetActive(true);
            playerClicked = false;
        }
        else
        {
            classNotice.SetActive(true);
        }
    }

    public void CloseNotice()
    {
        classNotice.SetActive(false);
        playerClicked = false;
        Time.timeScale = 1;
    }

    public override void ExitQuest()
    {
        questDialog.SetActive(false);
        Time.timeScale = 1;
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest != null)
        {
            StartCoroutine(Backstory());
        }
    }

    public override IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        introText.SetActive(false);
        introduced = true;
        talking = false;
    }

    public override IEnumerator Backstory()
    {
        if (PlayerQuestInteraction.ArcherQuest.questNum == 0 && !backstoryDone)
        {
            backstory[0].SetActive(true);
            yield return new WaitForSecondsRealtime(6);
            backstory[0].SetActive(false);
            backstoryDone = true;
        }
        else if (PlayerQuestInteraction.ArcherQuest.questNum == 2 && !secondBackStoryDone)
        {
            backstory[1].SetActive(true);
            yield return new WaitForSecondsRealtime(10);
            backstory[1].SetActive(false);
            secondBackStoryDone = true;
        }
    }

    public override void LoadData(GameData data)
    {
        introduced = data.intros[1];
        backstoryDone = data.intros[4];
        secondBackStoryDone = data.intros[5];
    }

    public override void SaveData(GameData data)
    {
        data.intros[1] = introduced;
        data.intros[4] = backstoryDone;
        data.intros[5] = secondBackStoryDone;
    }
}
