using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public Spell[] spells;
    private static bool[] unlocked = new bool[8];

    public bool IsUnlocked(int num)
    {
        return unlocked[num];
    }

    public void UnlockSpell(int num)
    {
        unlocked[num] = true;
    }
    
}
