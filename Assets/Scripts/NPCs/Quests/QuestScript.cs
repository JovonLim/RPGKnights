using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set;  }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        GetComponent<TextMeshProUGUI>().color = Color.red;
        WizardQuestLog.myInstance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    public void AddActive()
    {
        GetComponent<TextMeshProUGUI>().text += " (active)";
    }

    public void RemoveActive()
    {
        GetComponent<TextMeshProUGUI>().text = MyQuest.MyTitle;
    }

    public void MarkCompleted()
    {
        RemoveActive();
        GetComponent<TextMeshProUGUI>().text += " (completed)";
    }
}
