using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Objective
    {
        mobs, chests, boss, 
    }

    public Objective objective;
}
