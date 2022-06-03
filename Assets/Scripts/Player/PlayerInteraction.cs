using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public static bool questActive;

    public static Quest ArcherQuest;
    public static Quest WizardQuest;
    // Start is called before the first frame update
    void Start()
    {
        questActive = false;
        

    }

    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] UIs = GameObject.FindGameObjectsWithTag("UI");

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

