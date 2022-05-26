using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool playerInRange;
    private bool playerClicked;
    [SerializeField] private GameObject collectible;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject purchaseDialog;
    [SerializeField] private GameObject insufficientFunds;
    [SerializeField] private int cost;
 
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            playerClicked = true;
        }
        if (playerClicked && playerInRange)
        {
            purchaseDialog.SetActive(true);
        }
        else
        {
            purchaseDialog.SetActive(false);
            playerClicked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
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

    public void Purchased()
    {
        if (UI.coins >= cost)
        {
            if (collectible.CompareTag("Health"))
            {
                player.GetComponent<Health>().AddHealth();

            } else if (collectible.CompareTag("Defense"))
            {
                player.GetComponent<Health>().AddDefense();

            } else if (collectible.CompareTag("Attack"))
            {
                player.GetComponent<PlayerAttack>().AddAttack();
            }
            UI.coins -= cost;
            Destroy(purchaseDialog);
            Destroy(gameObject);
        } else
        {
            playerClicked = false;
            insufficientFunds.SetActive(true);
        }       
    }

    public void Decline()
    {
        playerClicked = false;
    }
}
