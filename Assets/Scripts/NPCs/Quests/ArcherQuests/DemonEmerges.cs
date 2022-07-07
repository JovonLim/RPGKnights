using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEmerges : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject bossPatrol;
    [SerializeField] GameObject classNotice;
    bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest != null)
        {
            if (PlayerQuestInteraction.ArcherQuest.questNum == 0)
            {
                door.SetActive(false);
                bossPatrol.SetActive(true);
                GetComponent<QuestProgress>().enabled = true;
                GetComponent<QuestEnd>().enabled = true;
            }
        }

        if (!unlocked && GetComponent<QuestProgress>().questCompleted)
        {
           PlayerAttack.rangedUnlock = true;
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

