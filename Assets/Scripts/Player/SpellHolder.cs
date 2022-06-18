using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour, IDataPersistence
{
    public Spell[] spells;
    public static Spell[] activeSpells = new Spell[4];
    protected static bool[] unlocked = new bool[13];
    public bool updated;


 
    protected virtual void Update()
    {
     
        
    }
    public static bool IsUnlocked(int num)
    {
        return unlocked[num];
    }

    public static void UnlockSpell(int num)
    {
        unlocked[num] = true;
    }

    private bool ContainSpell(Spell spell)
    {
        for (int i = 0; i < activeSpells.Length; i++)
        {
            if (activeSpells[i] != null && activeSpells[i] == spell)
            {
                return true;
            }
        }
        return false;
    }
    
    protected bool SetActiveSpell(int slot, int num)
    {
        if (slot <= 3)
        {
            if (!ContainSpell(spells[num]))
            {
                activeSpells[slot] = spells[num];
                updated = true;
                return true;
            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
       
    }

    protected bool RemoveActiveSpell(int num)
    {
        return Remove(spells[num]);
    }

    private bool Remove(Spell spell)
    {
        for (int i = 0; i < activeSpells.Length; i++)
        {
            if (activeSpells[i] != null && activeSpells[i] == spell)
            {
                activeSpells[i] = null;
                updated = true;
                return true;
            }
        }
        return false;
    }

    public void LoadData(GameData data)
    {
        unlocked = data.spellsUnlocked;
        activeSpells = data.activeSpells;
    }

    public void SaveData(GameData data)
    {
        data.spellsUnlocked = unlocked;
        data.activeSpells = activeSpells;
    }
}
