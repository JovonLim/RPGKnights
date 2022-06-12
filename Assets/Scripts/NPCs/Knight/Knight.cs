using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] GameObject shop;
    [SerializeField] GameObject introText;
    private static bool introduced = false;
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
            shop.SetActive(true);
        } else
        {
            shop.SetActive(false);
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

    public void ExitShop()
    {
        shop.SetActive(false);
        playerClicked = false;
    }

    IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(8);
        introText.SetActive(false);
        introduced = true;
    }

}

