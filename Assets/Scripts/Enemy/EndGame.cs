using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject objectiveBox;
    [SerializeField] private GameObject objectiveText;
    [SerializeField] private GameObject coreText;
    [SerializeField] private GameObject objectiveCleared;
    [SerializeField] private GameObject core;
    private bool cleared;
    private bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {
            if (core.GetComponent<EnemyHealth>().IsDefeated())
            {
                StartCoroutine(End());
                cleared = true;
            }
        } else
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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

    IEnumerator Begin()
    {
        objectiveBox.SetActive(true);
        coreText.SetActive(true);
        yield return new WaitForSeconds(2);
        coreText.SetActive(false);
        objectiveText.SetActive(true);
        yield return new WaitForSeconds(2);
        objectiveText.SetActive(false);
        objectiveBox.SetActive(false);
    }
    IEnumerator End()
    {
        objectiveBox.SetActive(true);
        objectiveCleared.SetActive(true);
        yield return new WaitForSeconds(4);
        objectiveCleared.SetActive(false);
        objectiveBox.SetActive(false);
    }

}
