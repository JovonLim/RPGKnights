using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool playerInRange;
    private bool playerClicked;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject purchaseDialog;
    [SerializeField] private GameObject insufficientFunds;
    [SerializeField] private int cost;
    [SerializeField] private float amt;

    public enum Stat
    {
        Attack,
        Defense,
        Health,
    }

    public Stat stat;

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
        if (CoinCounter.coins >= cost)
        {
            switch (stat)
            {
                case Stat.Attack:
                {
                        player.GetComponent<PlayerAttack>().AddAttack(amt);
                        break;
                }

                case Stat.Defense:
                    {
                        player.GetComponent<Health>().AddDefense(amt);
                        break;
                    }

                case Stat.Health:
                    {
                        player.GetComponent<Health>().AddHealth(amt);
                        break;
                    }
            }
            CoinCounter.coins -= cost;
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
