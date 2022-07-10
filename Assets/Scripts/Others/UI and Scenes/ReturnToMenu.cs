using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField] GameObject finalObjects;
    [SerializeField] GameObject skipButton;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        Destroy(UI);
        Destroy(player);
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
       
    }

    public void StartNewGamePlus()
    {
        DifficultyManager.instance.Difficulty += 0.2f;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(2);
    }

    public void Skip()
    {
        GetComponent<PlayableDirector>().Stop();
        finalObjects.SetActive(true);
        skipButton.SetActive(false);
    }
}
