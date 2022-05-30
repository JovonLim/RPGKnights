using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] GameObject questDialog;
    public Quest[] quests;
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
        if (playerClicked && playerInRange)
        {
            questDialog.SetActive(true);
        }
        else
        {
            questDialog.SetActive(false);
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

    public void Exit()
    {
        playerClicked = false;
    }
}
