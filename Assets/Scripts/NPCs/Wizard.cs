using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] GameObject questDialog;
    private bool playerClicked;
    private bool questAccepted;
    GameObject player;
    //[SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerClicked = false;
        questAccepted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!questAccepted)
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
        } else
        {
            questDialog.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void AcceptQuest()
    {
        questAccepted = true;
        PlayerInteraction.questActive = true;
    }

    public void DeclineQuest()
    {
        playerClicked = false;
    }

}