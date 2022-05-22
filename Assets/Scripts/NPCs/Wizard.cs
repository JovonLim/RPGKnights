using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] GameObject questDialog;
    private bool playerClicked;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)) {
            playerClicked = true;
        }
        if (playerClicked && playerInRange)
        {
            questDialog.SetActive(true);
        } else
        {
            questDialog.SetActive(false);
            playerClicked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
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
        player.GetComponent<PlayerInteraction>().questActive = true;
        playerClicked = false;
    }

    public void DeclineQuest()
    {
        playerClicked = false;
    }

}
