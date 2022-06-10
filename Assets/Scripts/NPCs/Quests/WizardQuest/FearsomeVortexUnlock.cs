using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearsomeVortexUnlock : MonoBehaviour
{
    private bool unlocked;
   
    // Update is called once per frame
    void Update()
    {
        if (!unlocked)
        {
            if (GetComponent<QuestEnd>().cleared)
            {
                SpellHolder.UnlockSpell(6);
                unlocked = true;
            }
        }
    }
}
