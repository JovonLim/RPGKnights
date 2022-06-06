using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearsomeVortexUnlock : MonoBehaviour
{
    private bool unlocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlocked)
        {
            if (GetComponent<QuestEnd>().cleared)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<SpellHolder>().UnlockSpell(5);
                unlocked = true;
            }
        }
    }
}
