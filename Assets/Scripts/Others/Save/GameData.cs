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

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        
        coins = 2000;
        activeSpells = new Spell[4];
        spellsUnlocked = new bool[8];
        intros = new bool[3];
        archerQuests = new bool[3];
        wizardQuests = new bool[4];
        wizardPurchased = new bool[6]; archerPurchased = new bool[2]; knightPurchased = new bool[2];
    }
}