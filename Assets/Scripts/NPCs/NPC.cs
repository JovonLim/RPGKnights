using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IDataPersistence
{
    [SerializeField] protected GameObject options;
    [SerializeField] protected GameObject questDialog;
    [SerializeField] protected GameObject shop;
    [SerializeField] protected GameObject introText;
    [SerializeField] protected GameObject[] backstory;
    protected bool introduced;
    protected bool backstoryDone;
    protected bool playerInRange;
    protected bool playerClicked;
    // Start is called before the first frame update
    protected void Start()
    {
        playerClicked = false;
    }

    // Update is called once per frame
    protected void Update()
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


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public abstract void DisplayShop();

    public void ExitShop()
    {
        Time.timeScale = 1;
        shop.SetActive(false);
    }

    public void DisplayQuests()
    {
        questDialog.SetActive(true);
        playerClicked = false;
    }
    public abstract void ExitQuest();

    public abstract IEnumerator Intro();
    

    public abstract IEnumerator Backstory();


    public abstract void LoadData(GameData data);


    public abstract void SaveData(GameData data);
}

