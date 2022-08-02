using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerQuestInteraction : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject tracker;
    private bool display = false;
    public static bool questActive;
    public static Quest ArcherQuest;
    public static Quest WizardQuest;
    protected static int KillCount;
    protected static int RoomCount;
    protected static int ChestCount;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTracker();
        }
    }

    public static void IncreaseChestCount()
    {
        ChestCount++;
        AllocateChestCoins();
    }

    public static void AllocateChestCoins()
    {
        if (ChestCount >= 5)
        {
            CoinCounter.coins += 80;
            ChestCount -= 5;
        }
    }

    public static void IncreaseKillCount()
    {
        KillCount++;
        AllocateKillCoins();
    }
    public static void AllocateKillCoins()
    {
        if (KillCount >= 5)
        {
            CoinCounter.coins += 60;
            KillCount -= 5;
        }
    }

    public static void IncreaseRoomCount()
    {
        RoomCount++;
        AllocateRoomCoins();
    }

    public static void AllocateRoomCoins()
    {
        if (RoomCount >= 3)
        {
            CoinCounter.coins += 120;
            RoomCount -= 3;
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
        
    }


    public void OnSceneUnloaded(Scene scene)
    {
        if (scene.buildIndex > 2 && scene.buildIndex < 17)
        {
            IncreaseRoomCount();
        }
    }

    void ToggleTracker()
    {
        display = !display;
        tracker.SetActive(display);
        if (display)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    public void CloseTracker()
    {
        tracker.SetActive(false);
        Time.timeScale = 1;
        display = !display;
    }

    public void LoadData(GameData data)
    {
        RoomCount = data.RoomCount;
        KillCount = data.KillCount;
        ChestCount = data.ChestCount;
        if (data.activeWizard >= 0)
        {
            WizardQuest = WizardQuestLog.instance.quests[data.activeWizard];
            ArcherQuest = null;
            questActive = true;
        }
        else if (data.activeArcher >= 0)
        {
            ArcherQuest = ArcherQuestLog.instance.quests[data.activeArcher];
            WizardQuest = null;
            questActive = true;
        }
        else
        {
            questActive = false;
            WizardQuest = null;
            ArcherQuest = null;
        }
    }

    public void SaveData(GameData data)
    {
        data.RoomCount = RoomCount;
        data.KillCount = KillCount;
        data.ChestCount = ChestCount;
        if (WizardQuest == null)
        {
            data.activeWizard = -1;
        }
        else
        {
            data.activeWizard = WizardQuest.questNum;
        }
        if (ArcherQuest == null)
        {
            data.activeArcher = -1;
        }
        else
        {
            data.activeArcher = ArcherQuest.questNum;
        }
    }

    public void SetTestValues(int kills, int chests, int rooms)
    {
        KillCount = kills;
        ChestCount = chests;
        RoomCount = rooms;
    }
}

