using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRequirements : MonoBehaviour
{
    [SerializeField] private GameObject objectiveBox;
    [SerializeField] private GameObject miniBossText;
    [SerializeField] private GameObject mobsText;
    [SerializeField] private GameObject bossText;
    [SerializeField] private GameObject objectiveCleared;
    [SerializeField] private int sceneNum;
    private GameObject[] mobs;
    private GameObject player;
    private bool cleared = false;
    private bool playerInRange = false;
    

    public enum Objective
    {
        miniBoss,
        mobs,
        boss,
    };
    public Objective objective;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin());
        switch (objective)
        {
            case Objective.miniBoss:
                {
                    mobs = GameObject.FindGameObjectsWithTag("MiniBoss");
                    break;
                }
            case Objective.mobs:
                {
                    mobs = GameObject.FindGameObjectsWithTag("Enemy");
                    break;
                }
            case Objective.boss:
                {
                    mobs = mobs = GameObject.FindGameObjectsWithTag("Boss");
                    break;
                }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {
            if (checkDefeated())
            {
                cleared = true;
                StartCoroutine(End());
            }
        }
        else
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.F))
            {
                DontDestroyOnLoad(player);
                player.GetComponent<Health>().GainHealth(1);
                SceneManager.LoadScene(sceneNum);
            }

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

    bool checkDefeated()
    {
        int defeated = 0;
        foreach (GameObject mob in mobs)
        {
            if (mob.GetComponent<Health>().IsDefeated())
            {
                defeated++;
            }
        }
        return defeated == mobs.Length;
    }

    IEnumerator Begin()
    {
        objectiveBox.SetActive(true);
        switch (objective)
        {
            case Objective.miniBoss:
                {
                    miniBossText.SetActive(true);
                    break;
                }
            case Objective.mobs:
                {
                    mobsText.SetActive(true);
                    break;
                }
            case Objective.boss:
                {
                    bossText.SetActive(true);
                    break;
                }
        }       
        yield return new WaitForSeconds(2);
        objectiveBox.SetActive(false);
        miniBossText.SetActive(false);
        mobsText.SetActive(false);
        bossText.SetActive(false);

    }

    IEnumerator End()
    {
        objectiveBox.SetActive(true);
        objectiveCleared.SetActive(true);
        yield return new WaitForSeconds(2);
        objectiveBox.SetActive(false);
        objectiveCleared.SetActive(false);

    }
}

