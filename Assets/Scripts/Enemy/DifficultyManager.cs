using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DifficultyManager : MonoBehaviour, IDataPersistence
{
    public static DifficultyManager instance;
    public float Difficulty;
    private bool adjusted;
    private GameObject[] mobs;
    private GameObject[] miniBosses;
    private GameObject[] bosses;
    private GameObject[] questBosses;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!adjusted)
        {   
            mobs = GameObject.FindGameObjectsWithTag("Enemy");
            miniBosses = GameObject.FindGameObjectsWithTag("MiniBoss");
            bosses = GameObject.FindGameObjectsWithTag("Boss");
            questBosses = GameObject.FindGameObjectsWithTag("QuestBoss");
            AdjustEnemies();         
            adjusted = true;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        adjusted = false;
    }

    public void OnSceneUnloaded(Scene scene)
    {

    }


    public void LoadData(GameData data)
    {
        Difficulty = data.Difficulty;
    }

    public void SaveData(GameData data)
    {
        data.Difficulty = Difficulty;
    }

    private void AdjustEnemies()
    {
        for (int i = 0; i < mobs.Length; i++)
        {

            if (mobs[i].GetComponent<Damage>() != null)
            {
                mobs[i].GetComponent<Damage>().ScaleDifficulty(Difficulty);
            }
            mobs[i].GetComponent<EnemyHealth>().ScaleDifficulty(Difficulty);
        }

        for (int i = 0; i < miniBosses.Length; i++)
        {
            miniBosses[i].GetComponent<Damage>().ScaleDifficulty(Difficulty);
            miniBosses[i].GetComponent<EnemyHealth>().ScaleDifficulty(Difficulty);
        }

        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].GetComponent<Damage>().ScaleDifficulty(Difficulty);
            bosses[i].GetComponent<EnemyHealth>().ScaleDifficulty(Difficulty);
        }

        for (int i = 0; i < questBosses.Length; i++)
        {
            questBosses[i].GetComponent<Damage>().ScaleDifficulty(Difficulty);
            questBosses[i].GetComponent<EnemyHealth>().ScaleDifficulty(Difficulty);
        }
    }
}
