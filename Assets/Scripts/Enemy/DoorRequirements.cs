using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRequirements : MonoBehaviour
{
    [SerializeField] private GameObject miniBoss;
    [SerializeField] private GameObject player;
    private Health playerHealth;
    bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (miniBoss.GetComponent<Health>().IsDefeated() && playerInRange)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            playerHealth = player.GetComponent<Health>();
            playerHealth.GainHealth(3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
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
}

