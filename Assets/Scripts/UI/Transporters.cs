using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transporters : MonoBehaviour
{
    [SerializeField] int sceneNum;
    bool playerInRange = false;
    GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            SceneManager.LoadScene(sceneNum);
            DontDestroyOnLoad(player);
            player.GetComponent<Health>().GainHealth(1);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            player = other.gameObject;
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
