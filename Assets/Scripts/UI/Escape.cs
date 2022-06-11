using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controls;
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        menu.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }
}
