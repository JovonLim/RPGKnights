using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardQuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questParent;
    [SerializeField] private TextMeshProUGUI description;

    private static WizardQuestLog instance;
    private Quest selected;
    private List<GameObject> listOfQuests = new List<GameObject>();
    public static bool[] questStatus = new bool[4];
    public static bool added;

    public static WizardQuestLog myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WizardQuestLog>();
            } 
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!added)
        {
            for (int i = 0; i < questStatus.Length; i++)
            {
                if (questStatus[i])
                {
                    listOfQuests[i].GetComponent<QuestScript>().MarkCompleted();
                    listOfQuests[i].GetComponent<QuestScript>().MyQuest.isCompleted = true;
                }
            }
            added = true;
        }
    }

    public void AddQuest(Quest quest)
    {
        GameObject selectedQuest = Instantiate(questPrefab, questParent);
        listOfQuests.Add(selectedQuest);
        QuestScript qs = selectedQuest.GetComponent<QuestScript>();
        quest.myQuestScript = qs;
        qs.MyQuest = quest;
        selectedQuest.GetComponent<TextMeshProUGUI>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null)
        {
            selected.myQuestScript.DeSelect();
        }
        selected = quest;
        description.text = selected.MyDescription;
    }

    public void AcceptQuest()
    {
        if (!PlayerInteraction.questActive && !selected.isCompleted)
        {
            if (selected.questNum == 2 || selected.questNum == 3)
            {
                if (Wizard.unlockedSkills)
                {
                    selected.myQuestScript.AddActive();
                    PlayerInteraction.WizardQuest = selected;
                    PlayerInteraction.questActive = true;
                }
            } else
            {
                selected.myQuestScript.AddActive();
                PlayerInteraction.WizardQuest = selected;
                PlayerInteraction.questActive = true;
            }
            
        }
        
    }

    public void Untrack()
    {
        selected.myQuestScript.RemoveActive();
        PlayerInteraction.WizardQuest = null;
        PlayerInteraction.questActive = false;
    }
}
