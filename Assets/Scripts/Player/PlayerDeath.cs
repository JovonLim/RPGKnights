using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        UI.coins = (int) (UI.coins * 0.8);
        DataPersistenceManager.instance.SaveGame();

        yield return new WaitForSeconds(1);
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
       
        SceneManager.LoadScene(2);
        InventoryDatabase.update = true;
    }
}
