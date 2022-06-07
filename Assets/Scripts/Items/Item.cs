using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item: MonoBehaviour
{
    
    public enum ItemType
    {
        Consumable, WarriorWeapon, ArcherWeapon, 
        MageWeapon, Helmet, ChestPiece, Leggings, Boots
    }

    public ItemType itemType;
    public string itemDescription;
    public UnityEvent consumeEvent;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 11;
    }
    public void BeingPickedUp()
    {
        FindObjectOfType<Inventory>().AddItem(gameObject);
        gameObject.SetActive(false);

    }
}
