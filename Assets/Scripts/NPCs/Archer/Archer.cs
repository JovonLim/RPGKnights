using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject classNotice;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject introText;
    [SerializeField] GameObject[] backstory;
    private static bool introduced = false;
    private static bool[] backstoryDone = new bool[2];
    private bool playerInRange;

    private bool playerClicked;
    // Start is called before the first frame update
    void Start()
    {
        playerClicked = false;
       
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
            Time.timeScale = 0;
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
        Time.timeScale = 1;
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest != null)
        {
            StartCoroutine(Backstory());
        }
    }

    public void ExitShop()
    {
        Time.timeScale = 1;
        shop.SetActive(false);
    }



    public void DisplayShop()
    {
        if (PlayerAttack.rangedUnlock)
        {
            shop.SetActive(true);
            playerClicked = false;
        } else
        {
            classNotice.SetActive(true);
        }
    }

    public void CloseNotice()
    {
        classNotice.SetActive(false);
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

    IEnumerator Backstory()
    {

        if (PlayerQuestInteraction.ArcherQuest.questNum == 0 && !backstoryDone[0])
        {
            backstory[0].SetActive(true);
            yield return new WaitForSecondsRealtime(6);
            backstory[0].SetActive(false);
            backstoryDone[0] = true;
        } 
        else if (PlayerQuestInteraction.ArcherQuest.questNum == 2 && !backstoryDone[1])
        {
            backstory[1].SetActive(true);
            yield return new WaitForSecondsRealtime(10);
            backstory[1].SetActive(false);
            backstoryDone[1] = true;
        }
    }

    public void LoadData(GameData data)
    {
        introduced = data.intros[1];
        backstoryDone[0] = data.intros[4];
        backstoryDone[1] = data.intros[5];
    }

    public void SaveData(GameData data)
    {
        data.intros[1] = introduced;
        data.intros[4] = backstoryDone[0];
        data.intros[5] = backstoryDone[1];
    }
}

