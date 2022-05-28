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
    private GameObject[] mobs;
    private GameObject player;
    private bool cleared = false;
    private bool playerInRange = false;
    

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
        if (objective == Objective.miniBoss)
        {
            mobs = GameObject.FindGameObjectsWithTag("MiniBoss");
        } else
        {
            mobs = GameObject.FindGameObjectsWithTag("Enemy");
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

