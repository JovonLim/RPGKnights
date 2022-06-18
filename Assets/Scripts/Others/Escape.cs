using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject saveNotice;
    [SerializeField] private GameObject confirmation;
    [SerializeField] private GameObject volume;
    private bool inMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    public void DisplayVolume()
    {
        menu.SetActive(false);
        volume.SetActive(true);
    }

    public void CloseVolume()
    {   
        volume.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }

    public void DisplayControls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void CloseControls()
    {
        controls.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }

    public void DisplayConfirmation()
    {
        menu.SetActive(false);
        confirmation.SetActive(true);

    }

    public void CloseConfirmation()
    {
        confirmation.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
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

    void DisplayNotice()
    {
        menu.SetActive(false);
        saveNotice.SetActive(true);
    }

    public void CloseNotice()
    {
        saveNotice.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }
}
