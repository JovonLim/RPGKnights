using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardQuestLog : MonoBehaviour
{
    public static WizardQuestLog instance;
    [SerializeField] private TextMeshProUGUI[] questTitles;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject questNotice;
    [SerializeField] private GameObject unlockNotice;
    public Quest[] quests;
    public static bool[] completedQuests = new bool[4];
    private static int selected;

  
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        completedQuests = DataPersistenceManager.instance.gameData.wizardQuests;
        for (int i = 0; i < completedQuests.Length; i++)
        {
            if (completedQuests[i])
            {
                questTitles[i].text = quests[i].title + " (completed)";
            }
        }
        selected = DataPersistenceManager.instance.gameData.activeWizard;
        if (selected >= 0)
        {
            AddActive();
            ShowDescription();
        }
        
    }
    public void AcceptQuest()
    {
        if (!PlayerQuestInteraction.questActive && !completedQuests[selected])
        {
            if (selected == 2)
            {
                if (PlayerAttack.spellUnlock && SpellHolder.IsUnlocked(1))
                {
                    AddActive();
                    PlayerQuestInteraction.WizardQuest = quests[selected];
                    PlayerQuestInteraction.questActive = true;
                } else 
                {
                    unlockNotice.SetActive(true);
                    unlockNotice.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Requires lightning bolt unlocked to accept quest";
                }
            }
            else if (selected == 3)
            {
                if (PlayerAttack.spellUnlock && SpellHolder.IsUnlocked(2))
                {
                    AddActive();
                    PlayerQuestInteraction.WizardQuest = quests[selected];
                    PlayerQuestInteraction.questActive = true;
                }
                else
                {
                    unlockNotice.SetActive(true);
                    unlockNotice.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Requires water ball unlocked to accept quest";
                }
            }
            else
            {
                AddActive();
                PlayerQuestInteraction.WizardQuest = quests[selected];
                PlayerQuestInteraction.questActive = true;
            }

        } else
        {
            questNotice.SetActive(true);
        }
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
        if (!completedQuests[selected])
        {
            RemoveActive();
            PlayerQuestInteraction.WizardQuest = null;
            PlayerQuestInteraction.questActive = false;
        }
    }

    public void CloseNotice()
    {
        questNotice.SetActive(false);
        unlockNotice.SetActive(false);
    }
}
