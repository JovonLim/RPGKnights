using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("CurrentScene", 0);
    }
}
