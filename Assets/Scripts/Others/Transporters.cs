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
            PlayerPrefs.SetInt("CurrentScene", sceneNum);
            DontDestroyOnLoad(player);
            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
