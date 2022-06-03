using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OhLordZeus : MonoBehaviour
{
    [SerializeField] GameObject bossPatrol;

    // Start is called before the first frame update
    void Start()
    {
        bossPatrol.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.questActive && PlayerInteraction.WizardQuest != null)
        {
            if (PlayerInteraction.WizardQuest.questNum == 2)
            {
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }
    }
}
