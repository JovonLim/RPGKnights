using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerQuestInteraction : MonoBehaviour, IDataPersistence
{
    public static bool questActive;

    public static Quest ArcherQuest;
    public static Quest WizardQuest;
    public static int KillCount;
    public static int RoomCount;
    public static int ChestCount;

    public void LoadData(GameData data)
    {
        RoomCount = data.RoomCount;
        KillCount = data.KillCount;
        ChestCount = data.ChestCount;
    }

    public void SaveData(GameData data)
    {
        data.RoomCount = RoomCount;
        data.KillCount = KillCount;
        data.ChestCount = ChestCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        questActive = false;
        

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
        
    }


    public void OnSceneUnloaded(Scene scene)
    {
        if (scene.buildIndex > 2 && scene.buildIndex < 17)
        {
            RoomCount += 1;
        }
    }

    void Update()
    {
        if (ChestCount >= 5)
        {
            UI.coins += 30;
            ChestCount -= 5;
        }

        if (KillCount >= 5)
        {
            UI.coins += 20;
            KillCount -= 5;
        }

        if (RoomCount >= 3)
        {
            UI.coins += 40;
            RoomCount -= 3;
        }
    }
}

