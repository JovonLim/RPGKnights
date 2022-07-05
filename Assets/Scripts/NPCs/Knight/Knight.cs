using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knight : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject milestoneDialog;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject introText;
    [SerializeField] TextMeshProUGUI[] milestoneTitle;
    [SerializeField] GameObject[] milestoneDescription;

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
            Time.timeScale = 0;
            options.SetActive(true);
        }
        else
        {
            options.SetActive(false);
            playerClicked = false;
        }
    }

    public void ExitMilestone()
    {
        Time.timeScale = 1;
        milestoneDialog.SetActive(false);
      
    }
    public void ExitShop()
    {
        Time.timeScale = 1;
        shop.SetActive(false);
    }

    public void DisplayShop()
    {
        shop.SetActive(true);
        playerClicked = false;
    }

    public void DisplayMilestones()
    {
        milestoneDialog.SetActive(true);
        playerClicked = false;
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

    IEnumerator Intro()
    {
        introText.SetActive(true);
        yield return new WaitForSecondsRealtime(8);
        introText.SetActive(false);
        introduced = true;
    }

    public void LoadData(GameData data)
    {
        introduced = data.intros[2];
    }

    public void SaveData(GameData data)
    {
        data.intros[2] = introduced;
    }

    public void SelecttMilestone(int num)
    {
        milestoneTitle[num].color = Color.red;
        milestoneDescription[num].SetActive(true);
    }

    public void DeselectMilestone(int num)
    {
        milestoneTitle[num].color = Color.black;
        milestoneDescription[num].SetActive(false);
    }
}

