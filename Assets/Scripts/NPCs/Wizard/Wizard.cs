using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject introText;
    public Quest[] quests;
    private static bool introduced = true;
    private bool playerInRange;
    
    private bool playerClicked;
    public static bool unlockedSkills = false;
    // Start is called before the first frame update
    void Start()
    {
        playerClicked = false;
        foreach (Quest quest in quests)
        {
            questDialog.GetComponent<WizardQuestLog>().AddQuest(quest);
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
        } else
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
    }

    public void ExitShop()
    {
        shop.SetActive(false);
    }

    IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        introText.SetActive(false);
        introduced = true;
    }

    public void DisplayShop()
    {
        shop.SetActive(true);
        playerClicked = false;
    }

    public void DisplayQuests()
    {
        questDialog.SetActive(true);
        playerClicked = false;
    }
}
