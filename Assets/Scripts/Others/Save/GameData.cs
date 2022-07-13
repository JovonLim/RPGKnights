using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float volume;
    public int coins;
    public int scrollsUsed;
    public bool tutorialDone;
    public Spell[] activeSpells;
    public bool[] spellsUnlocked;
    public bool[] intros;
    public bool[] archerQuests; public bool[] wizardQuests;
    public int activeWizard; public int activeArcher;
    public bool[] wizardPurchased; public bool[] archerPurchased; public bool[] knightPurchased;
    public bool rangedUnlocked;
    public bool spellUnlocked;
    public List<int> currentItems;
    public List<int> currentEquip;
    public int RoomCount;
    public int KillCount;
    public int ChestCount;
    public float Difficulty;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        volume = 1;
        scrollsUsed = 0;
        tutorialDone = false;
        activeWizard = -1;
        activeArcher = -1;
        coins = 0; ChestCount = 0; KillCount = 0; RoomCount = 0;     
        activeSpells = new Spell[4];
        spellsUnlocked = new bool[16];
        intros = new bool[6];
        archerQuests = new bool[3];
        wizardQuests = new bool[4];
        wizardPurchased = new bool[14]; archerPurchased = new bool[2]; knightPurchased = new bool[2];
        currentItems = new List<int>();
        currentEquip = new List<int>();
        rangedUnlocked = false;
        spellUnlocked = false;
        Difficulty = 1f;
    }
}
