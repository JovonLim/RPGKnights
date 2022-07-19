using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject MenuButton;
    [SerializeField] GameObject NewGamePlusButton;
    [SerializeField] GameObject skipButton;
    void Start()
    {
        text.SetActive(true);
        MenuButton.SetActive(true);
        skipButton.SetActive(false);
        if (DifficultyManager.instance.Difficulty > 1.0f)
        {
            NewGamePlusButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue New Game +"; 
        }
        NewGamePlusButton.SetActive(true);
    }
}
