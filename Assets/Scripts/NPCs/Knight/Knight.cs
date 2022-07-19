using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knight : NPC
{
    [SerializeField] TextMeshProUGUI[] milestoneTitle;
    [SerializeField] GameObject[] milestoneDescription;

    public override IEnumerator Backstory()
    {
        yield break;
    }

    public override void DisplayShop()
    {
        shop.SetActive(true);
        playerClicked = false;
    }

    public override void ExitQuest()
    {
        Time.timeScale = 1;
        questDialog.SetActive(false);
    }

    public override IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(8);
        introText.SetActive(false);
        introduced = true;
    }

    public void SelectMilestone(int num)
    {
        milestoneTitle[num].color = Color.red;
        milestoneDescription[num].SetActive(true);
    }

    public void DeselectMilestone(int num)
    {
        milestoneTitle[num].color = Color.black;
        milestoneDescription[num].SetActive(false);
    }

    public override void LoadData(GameData data)
    {
        introduced = data.intros[2];
    }

    public override void SaveData(GameData data)
    {
        data.intros[2] = introduced;
    }
}
