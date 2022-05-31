using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTreasuresWithin : MonoBehaviour
{

    private GameObject[] chests;
    private bool questCompleted = false;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject startingText;
    [SerializeField] GameObject endingText;

    // Start is called before the first frame update
    void Start()
    {
        chests = GameObject.FindGameObjectsWithTag("Chest");
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
        if (!questCompleted)
        {
            if (allOpened())
            {
                StartCoroutine(giveRewards());
                questCompleted = true;
                ArcherQuestLog.questStatus[PlayerInteraction.ArcherQuest.questNum] = true;
                ArcherQuestLog.added = false;
                PlayerInteraction.questActive = false;
                PlayerInteraction.WizardQuest = null;
            }
        }
    }

    bool allOpened()
    {
        int count = 0;
        foreach (GameObject chest in chests)
        {
            if (chest.GetComponent<Chests>().chestOpened)
            {
                count++;
            }
        }
        return count == chests.Length;
    }

    IEnumerator Begin()
    {
        questDialog.SetActive(true);
        startingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        startingText.SetActive(false);
    }

    IEnumerator giveRewards()
    {
        UI.coins += 125;
        questDialog.SetActive(true);
        endingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        endingText.SetActive(false);
    }

}
