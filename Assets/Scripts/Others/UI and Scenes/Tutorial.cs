using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject skipButton;
    private int page = 0;
    private static bool tutorialDone = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (!tutorialDone)
        {
            tutorials[page].SetActive(true);
            nextButton.SetActive(true);
            skipButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void NextPage()
    {
        tutorials[page].SetActive(false);
        page++;
        CheckPage();       
    }

    private void CheckPage()
    {
        if (page == tutorials.Length - 1)
        {
            nextButton.SetActive(false);
            closeButton.SetActive(true);
            skipButton.SetActive(false);       
        }
        tutorials[page].SetActive(true);
    }

    public void CloseTutorial()
    {
        nextButton.SetActive(false);
        closeButton.SetActive(false);
        skipButton.SetActive(false);
        for (int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].SetActive(false);
        }
        Time.timeScale = 1;
        tutorialDone = true;
    }

    public void LoadData(GameData data)
    {
        tutorialDone = data.tutorialDone;
    }

    public void SaveData(GameData data)
    {    
        data.tutorialDone = tutorialDone;
    }
}
