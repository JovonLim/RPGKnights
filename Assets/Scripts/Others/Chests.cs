using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    Animator anima;
    bool playerInRange = false;
    public bool chestOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chestOpened)
        {
            if (Input.GetKeyDown(KeyCode.F) && playerInRange)
            {
                anima.SetBool("Opening", true);
                chestOpened = true;
                PlayerQuestInteraction.ChestCount++;
                this.GetComponent<CoinSpawn>().Spawn();
            }
        }
        else
        {
            anima.SetBool("Opened", true);
            this.GetComponent<Collider2D>().enabled = false;
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
}
