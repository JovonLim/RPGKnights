using UnityEngine;

public class SpellHolder : MonoBehaviour, IDataPersistence
{
    public Spell[] spells;
    public static Spell[] activeSpells = new Spell[4];
    protected static bool[] unlocked = new bool[16];
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

    protected bool SetActiveSpell(int num)
    {
        if (!ContainSpell(spells[num]))
        {
            for (int i = 0; i < activeSpells.Length; i++)
            {
                if (activeSpells[i] == null)
                {
                    activeSpells[i] = spells[num];
                    updated = true;
                    return true;
                }
            }
            return false;
        }
        else
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
