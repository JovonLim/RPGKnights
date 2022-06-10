using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject introText;
    [SerializeField] GameObject backstory;
    public Quest[] quests;
    private static bool introduced;
    private bool playerInRange;

    private bool playerClicked;
    // Start is called before the first frame update
    void Start()
    {
        playerClicked = false;
        foreach (Quest quest in quests)
        {
            questDialog.GetComponent<ArcherQuestLog>().AddQuest(quest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            playerClicked = true;
        }
        if (playerClicked && playerInRange && !introduced)
        {
            StartCoroutine(Intro());
        }
        else if (playerClicked && playerInRange)
        {

            options.SetActive(true);
        }
        else
        {
            options.SetActive(false);
            playerClicked = false;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void ExitQuest()
    {
        questDialog.SetActive(false);
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest.questNum == 0)
        {
            StartCoroutine(Backstory(0));
        }
    }

    public void ExitShop()
    {
        shop.SetActive(false);
    }



    public void DisplayShop()
    {
        if (PlayerAttack.rangedUnlock)
        {
            shop.SetActive(true);
            playerClicked = false;
        }
    }

    public void DisplayQuests()
    {
        questDialog.SetActive(true);
        playerClicked = false;
    }
    IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        introText.SetActive(false);
        introduced = true;
    }

    IEnumerator Backstory(int num)
    {
        backstory.SetActive(true);
        yield return new WaitForSecondsRealtime(6);
        backstory.SetActive(false);
    }
}

