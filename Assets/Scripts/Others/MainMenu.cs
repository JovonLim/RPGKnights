using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject NewGame;
    [SerializeField] GameObject ContinueGame;
    [SerializeField] GameObject[] menus;
    private void Start()
    {
        CheckSave();
    }
    public void StartNewGame()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    { 
        SceneManager.LoadScene(2);
        
    }
    public void OpenSettings()
    {
        menus[0].SetActive(false);
        menus[1].SetActive(true);
     
    }

    public void OpenControls()
    {
        menus[1].SetActive(false);
        menus[2].SetActive(true);
    }

    public void OpenVolume()
    {
        menus[1].SetActive(false);
        menus[3].SetActive(true);
    }

    public void ReturnToSettings()
    {
        for (int i = 1; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
        menus[1].SetActive(true);
    }

    public void ReturnToMenu()
    {
        for (int i = 1; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
        menus[0].SetActive(true);
        CheckSave();
    }
   public void ExitGame()
    {
        Application.Quit();
    }

    private void CheckSave()
    {
        if (DataPersistenceManager.instance.HasGameData())
        {
            ContinueGame.SetActive(true);
            NewGame.SetActive(false);
        }
        else
        {
            ContinueGame.SetActive(false);
            NewGame.SetActive(true);
        }
    }
}
