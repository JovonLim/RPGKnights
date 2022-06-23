using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject[] options;
    private bool inMenu = false;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inMenu)
        {
            menu.SetActive(true);     
            Time.timeScale = 0;
            inMenu = true;
         
        } else if (Input.GetKeyDown(KeyCode.Escape) && inMenu && menu.activeInHierarchy)
        {
            menu.SetActive(false);      
            Time.timeScale = 1;
            inMenu = false;
        }
    }

    public void ReturnToOptionMenu()
    {   
        for (int i = 0; i < options.Length; i++)
        {
            options[i].SetActive(false);
        }
        menu.SetActive(true);
    }

    public void DisplayControls()
    {
        menu.SetActive(false);
        options[0].SetActive(true);
    }

    public void DisplayVolume()
    {
        menu.SetActive(false);
        options[1].SetActive(true);
    }

    void DisplayNotice()
    {
        menu.SetActive(false);
        options[2].SetActive(true);
    }

    public void DisplayConfirmation()
    {
        menu.SetActive(false);
        options[3].SetActive(true);

    }

    public void ReturnToMenu()
    {
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
        menu.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }

    public void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            DataPersistenceManager.instance.SaveGame();
        } else
        {
            DisplayNotice();
        }
       
    }
}
