using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item: MonoBehaviour
{
    
    public enum ItemType
    {
        WarriorWeapon, 
        Helmet, 
        ChestPiece, 
        Leggings, 
        Boots, 
        ArcherWeapon,
        MageWeapon, 
        Consumables
    }

    public ItemType itemType;
    public string itemDescription;
    public UnityEvent consumeEvent;
    [SerializeField] public float attackDamageBoost;
    [SerializeField] public float attackSpeedBoost;
    [SerializeField] public float physicalDefenseBoost;
    [SerializeField] public float magicDefenseBoost;
    [SerializeField] public float healthBoost;
    [SerializeField] public float speedBoost;
    [SerializeField] public float defenseBoost;

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

    public void BeingDropped()
    {
        FindObjectOfType<PlayerItemInteraction>().DropItem(gameObject);
        Destroy(gameObject);
    }
}
