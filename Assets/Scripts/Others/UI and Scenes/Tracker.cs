using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracker :  PlayerQuestInteraction
{
    [SerializeField] private TextMeshProUGUI[] titles;
    [SerializeField] private TextMeshProUGUI description;
    private string Milestone = "Current Progress: ";
    // Start is called before the first frame update
    void OnEnable()
    {
        description.text = "";
        if (WizardQuest != null)
        {
            titles[0].text = WizardQuest.title;
        }
        else if (ArcherQuest != null)
        {
            titles[0].text = ArcherQuest.title;
        } else
        {
            titles[0].text = "No active quest";
        }
    }

    public void Select(int num)
    {
        if (num == 0)
        {
            if (WizardQuest != null)
            {
                description.text = WizardQuest.description;
            }
            else if (ArcherQuest != null)
            {

                description.text = ArcherQuest.description;
            }
            else
            {
                description.text = "Not applicable";
            }
        }
        else if (num == 1)
        {
            description.text = Milestone + "\n" + KillCount + " / 5";
        }
        else if (num == 2)
        {
            description.text = Milestone + "\n" + RoomCount + " / 3";
        } else
        {
            description.text = Milestone + "\n" + ChestCount + " / 5";
        }
    }
}
