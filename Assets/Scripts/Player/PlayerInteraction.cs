using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cave Doors")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Respawn()
    {      
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] UIs = GameObject.FindGameObjectsWithTag("UI");
        yield return new WaitForSeconds(2);
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
        foreach (GameObject UI in UIs)
        {
            Destroy(UI);
        }
        SceneManager.LoadScene(0);
    }
}
