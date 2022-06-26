using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coins;
    public Spell[] activeSpells;
    public bool[] spellsUnlocked;
    public bool[] intros;
    public bool[] archerQuests; public bool[] wizardQuests;
    public bool[] wizardPurchased; public bool[] archerPurchased; public bool[] knightPurchased;
    public bool rangedUnlocked;
    public bool spellUnlocked;
    public List<int> currentItems;
    public List<int> currentEquip;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        coins = 0;
        activeSpells = new Spell[4];
        spellsUnlocked = new bool[13];
        intros = new bool[3];
        archerQuests = new bool[3];
        wizardQuests = new bool[4];
        wizardPurchased = new bool[11]; archerPurchased = new bool[2]; knightPurchased = new bool[2];
        currentItems = new List<int>();
        currentEquip = new List<int>();
        rangedUnlocked = false;
        spellUnlocked = false;
    }
}
