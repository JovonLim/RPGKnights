using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int health;
    public Vector3 playerPosition;
    public int coins;


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.health = 3;
        playerPosition = Vector3.zero;
        UI.coins = 0;
    }
}
