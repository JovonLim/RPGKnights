using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheArtOfCasting : MonoBehaviour
{
    [SerializeField] GameObject bossPatrol;
    [SerializeField] GameObject classNotice;
    bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        bossPatrol.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.WizardQuest != null)
        {
            if (PlayerQuestInteraction.WizardQuest.questNum == 1)
            {
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }

        if (!unlocked && GetComponent<QuestProgress>().questCompleted)
        {
            PlayerAttack.spellUnlock = true;
            StartCoroutine(ShowClassChange());
            unlocked = true;
        }
    }

    IEnumerator ShowClassChange()
    {
        yield return new WaitForSeconds(3f);
        classNotice.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseNotice()
    {
        classNotice.SetActive(false);
        Time.timeScale = 1;
    }
}
