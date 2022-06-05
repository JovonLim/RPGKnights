using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public enum ItemType
    {
        Consumable, WarriorWeapon, ArcherWeapon, 
        MageWeapon, Helmet, ChestPiece, Leggings, Boots
    }

    public ItemType itemType;
    public int amount;
    public string itemDescription;
    
    public void beingInteracted()
    {
        FindObjectOfType<Inventory>().AddItem(gameObject);
        gameObject.SetActive(false);

    }
}
