using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    

    public int coins;
    public float speed;
    public float jumpSpeed;


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        
        speed = 10;
        jumpSpeed = 10;
       
    }
}
