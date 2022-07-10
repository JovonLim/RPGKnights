using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcherQuestLog : MonoBehaviour
{
    public static ArcherQuestLog instance;
    [SerializeField] private TextMeshProUGUI[] questTitles;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject questNotice;
    public Quest[] quests;
    public static bool[] completedQuests = new bool[3];
    private static int selected = -1;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        completedQuests = DataPersistenceManager.instance.gameData.archerQuests;
        for (int i = 0; i < completedQuests.Length; i++)
        {
            if (completedQuests[i])
            {
                questTitles[i].text = quests[i].title + " (completed)";
            }
        }
        selected = DataPersistenceManager.instance.gameData.activeArcher;
        if (selected >= 0)
        {
            AddActive();
            ShowDescription();
        }
    }

    public void AcceptQuest()
    {
        if (selected < 0)
        {
            return;
        }
        if (!PlayerQuestInteraction.questActive && !completedQuests[selected])
        {
            AddActive();
            PlayerQuestInteraction.ArcherQuest = quests[selected];
            PlayerQuestInteraction.questActive = true;
        } else
        {
            questNotice.SetActive(true);
        }
    }

    public void CloseNotice()
    {
        questNotice.SetActive(false);
    }

    public void ShowDescription()
    {
        description.text = quests[selected].description;
    }

    public void Select(int num)
    {

        for (int i = 0; i < questTitles.Length; i++)
        {
            questTitles[i].color = Color.black;
        }
        selected = num;
        if (!completedQuests[selected])
        {
            questTitles[selected].color = Color.red;
            ShowDescription();
        }

    }
    private void AddActive()
    {
        questTitles[selected].color = Color.red;
        questTitles[selected].text += " (active)";
    }

    private void RemoveActive()
    {
        questTitles[selected].color = Color.black;
        questTitles[selected].text = quests[selected].title;
    }
    public void Untrack()
    {
        if (selected < 0)
        {
            return;
        }
        if (!completedQuests[selected] && PlayerQuestInteraction.ArcherQuest.questNum == selected)
        {
            RemoveActive();
            PlayerQuestInteraction.ArcherQuest = null;
            PlayerQuestInteraction.questActive = false;
        }
    }


}
