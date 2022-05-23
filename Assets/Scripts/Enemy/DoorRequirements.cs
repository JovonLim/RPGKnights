using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRequirements : MonoBehaviour
{
    [SerializeField] private GameObject objectiveBox;
    [SerializeField] private GameObject miniBossText;
    [SerializeField] private GameObject mobsText;
    [SerializeField] private GameObject objectiveCleared;
    [SerializeField] private int sceneNum;
    [SerializeField] private GameObject miniBoss;
    private GameObject[] mobs;
    private GameObject player;
    private bool cleared = false;
    private bool playerInRange = false;
    private int defeated = 0;
    

    public enum Objective
    {
        miniBoss,
        mobs,
    };
    public Objective objective;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin());
        mobs = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {
            if (objective == Objective.miniBoss)
            {
                if (miniBoss.GetComponent<Health>().IsDefeated()) {
                    cleared = true;
                    StartCoroutine(End());
                }
            }
            else
            {
                if (checkDefeated())
                {
                    cleared = true;
                    StartCoroutine(End());
                }
                else
                {
                    defeated = 0;
                }
            }
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            player.GetComponent<Health>().GainHealth(1);
            SceneManager.LoadScene(sceneNum);
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

    bool checkDefeated()
    {
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
        if (objective == Objective.miniBoss)
        {
            miniBossText.SetActive(true);
        } else
        {
            mobsText.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        objectiveBox.SetActive(false);
        miniBossText.SetActive(false);
        mobsText.SetActive(false);

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

