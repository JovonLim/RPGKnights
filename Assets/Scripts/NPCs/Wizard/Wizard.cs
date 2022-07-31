using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : NPC
{
    [SerializeField] private GameObject classNotice;
  
    public override void DisplayShop()
    {
        if (PlayerAttack.spellUnlock)
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
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.WizardQuest.questNum == 1 && !backstoryDone)
        {
            StartCoroutine(Backstory());
            backstoryDone = true;
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
        backstory[0].SetActive(true);
        yield return new WaitForSecondsRealtime(9);
        backstory[0].SetActive(false);
    }

    public override void LoadData(GameData data)
    {
        introduced = data.intros[0];
        backstoryDone = data.intros[3];
    }

    public override void SaveData(GameData data)
    {
        data.intros[0] = introduced;
        data.intros[3] = backstoryDone;
    }
}
