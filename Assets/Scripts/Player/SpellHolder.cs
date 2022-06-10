using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public Spell[] spells;
    public static Spell[] activeSpells = new Spell[4];
    protected static bool[] unlocked = new bool[8];
    public bool updated;


 
    protected virtual void Update()
    {
     
        
    }
    protected bool IsUnlocked(int num)
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
    
}
