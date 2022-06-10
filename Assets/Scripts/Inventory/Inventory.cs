using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    private static int currentInvCapacity;
    private const int totalInvCapacity = 9;
    public static GameObject[] itemList;
    [SerializeField] private GameObject itemPanelWindow;
    [SerializeField] private Image[] inventoryItemsImages;

    [SerializeField] private GameObject itemInformationWindow;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public static GameObject[] equipHolder;
    [SerializeField] private GameObject equipWindow;
    [SerializeField] private Image[] equipImages;

    private static bool inventoryOn = false;

    private void Start()
    {
        itemPanelWindow.SetActive(inventoryOn);

        // Initialising Array for inventory
        itemList = new GameObject[9];
        for (int i = 0; i < itemList.Length; i++)
        {
            itemList[i] = null;
        }
        currentInvCapacity = 0;

        //Initialising Array for equipments
        equipHolder = new GameObject[7];
        for (int i = 0; i < equipHolder.Length; i++)
        {
            equipHolder[i] = null;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

    }

    private void ToggleInventory()
    {
        inventoryOn = !inventoryOn;
        itemPanelWindow.SetActive(inventoryOn);
        UpdateUI();
    }

    public bool IsInventoryOn()
    {
        return inventoryOn;
    }

    public bool IsInventoryFull()
    {
        return currentInvCapacity == totalInvCapacity;
    }

    public void AddItem(GameObject item)
    {       
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = item;
                if (!InventoryDatabase.CurrentItems.Contains(item))
                {
                    InventoryDatabase.CurrentItems.Add(item);
                    item.transform.SetParent(InventoryDatabase.instance.transform);
                }      
                break;
            }
        } 
        UpdateUI();

    }

    public bool ContainsItem(GameObject item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null && itemList[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    // For internal inventory usage
    public void RemoveItem(int invNum)
    {
        if (itemList[invNum] != null)
        {
            int posToRemove = InventoryDatabase.CurrentItems.IndexOf(itemList[invNum]);
            InventoryDatabase.CurrentItems.RemoveAt(posToRemove);
            itemList[invNum] = null;
            UpdateUI();
        } 
        else
        {
            return;
        } 
    }

    // When player want to drop item
    public void DropItem(int invNum)
    {
        if (itemList[invNum] != null)
        {
            int posToRemove = InventoryDatabase.CurrentItems.IndexOf(itemList[invNum]);
            Destroy(InventoryDatabase.CurrentItems[posToRemove]);
            InventoryDatabase.CurrentItems.RemoveAt(posToRemove);
            itemList[invNum].GetComponent<Item>().BeingDropped();
            itemList[invNum] = null;
            UpdateUI();
        }
        else
        {
            return;
        }
    }

    // Equip from inventory
    public void Equip(int invNum)
    {

        GameObject itemToBeEquipped = itemList[invNum];

        if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.WarriorWeapon)
        {
            if (equipHolder[0] != null)
            {
                Unequip(0);
            }
            equipHolder[0] = itemToBeEquipped;

        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Helmet)
        {
            if (equipHolder[1] != null)
            {
                Unequip(1);
            }
            
            equipHolder[1] = itemToBeEquipped;

        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ChestPiece)
        {
            if (equipHolder[2] != null)
            {
                Unequip(2);
            }
            
            equipHolder[2] = itemToBeEquipped;
            
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Leggings)
        {
            if (equipHolder[3] != null)
            {
                Unequip(3);
            }
            
            equipHolder[3] = itemToBeEquipped;
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Boots)
        {
            if (equipHolder[4] != null)
            {
                Unequip(4);
            }
            equipHolder[4] = itemToBeEquipped;
            
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            if (equipHolder[5] != null)
            {
                Unequip(5);
            }
            
            equipHolder[5] = itemToBeEquipped;
            
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.MageWeapon)
        {
            if (equipHolder[6] != null)
            {
                Unequip(6);
            }

            equipHolder[6] = itemToBeEquipped;
            
        } 
        else
        {
            return;
        }
        InventoryDatabase.currentEquip.Add(itemToBeEquipped);
        EquipStats(invNum);
        RemoveItem(invNum);
        UpdateUI();
    }

    // For direct equiping of items to the specific slot 
    public void DirectEquip(GameObject equip)
    {

        if (equip.GetComponent<Item>().itemType == Item.ItemType.WarriorWeapon)
        {
            if (equipHolder[0] != null)
            {
                Unequip(0);
            }
            equipHolder[0] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.Helmet)
        {
            if (equipHolder[1] != null)
            {
                Unequip(1);
            }

            equipHolder[1] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.ChestPiece)
        {
            if (equipHolder[2] != null)
            {
                Unequip(2);
            }

            equipHolder[2] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.Leggings)
        {
            if (equipHolder[3] != null)
            {
                Unequip(3);
            }

            equipHolder[3] = equip;
        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.Boots)
        {
            if (equipHolder[4] != null)
            {
                Unequip(4);
            }
            equipHolder[4] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            if (equipHolder[5] != null)
            {
                Unequip(5);
            }

            equipHolder[5] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.MageWeapon)
        {
            if (equipHolder[6] != null)
            {
                Unequip(6);
            }

            equipHolder[6] = equip;

        }
        else
        {
            return;
        }
        FindObjectOfType<PlayerAttack>().AddAttack(equip.GetComponent<Item>().attackDamageBoost);
        FindObjectOfType<PlayerAttack>().AddAttackSpeed(equip.GetComponent<Item>().attackSpeedBoost);
        FindObjectOfType<Health>().AddHealth(equip.GetComponent<Item>().healthBoost);
        FindObjectOfType<Health>().AddDefense(equip.GetComponent<Item>().defenseBoost);
        FindObjectOfType<Health>().AddMagicDefense(equip.GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<Health>().AddPhysicalDefense(equip.GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<Health>().GainHealth(equip.GetComponent<Item>().regenerateHealth);
        FindObjectOfType<PlayerMovement>().AddSpeed(equip.GetComponent<Item>().speedBoost);
        UpdateUI();
    }

    public void EquipStats(int invNum)
    {
        FindObjectOfType<PlayerAttack>().AddAttack(itemList[invNum].GetComponent<Item>().attackDamageBoost);
        FindObjectOfType<PlayerAttack>().AddAttackSpeed(itemList[invNum].GetComponent<Item>().attackSpeedBoost);
        FindObjectOfType<Health>().AddHealth(itemList[invNum].GetComponent<Item>().healthBoost);
        FindObjectOfType<Health>().AddDefense(itemList[invNum].GetComponent<Item>().defenseBoost);
        FindObjectOfType<Health>().AddMagicDefense(itemList[invNum].GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<Health>().AddPhysicalDefense(itemList[invNum].GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<Health>().GainHealth(itemList[invNum].GetComponent<Item>().regenerateHealth);
        FindObjectOfType<PlayerMovement>().AddSpeed(itemList[invNum].GetComponent<Item>().speedBoost);
        
    }

    public void Unequip(int equipNum)
    {
        if (equipHolder[equipNum] != null)
        {
            InventoryDatabase.currentEquip.Remove(equipHolder[equipNum]);
            UnequipStats(equipNum);
            AddItem(equipHolder[equipNum]);
            equipHolder[equipNum] = null;
            UpdateUI();
        }
    }

    public void UnequipStats(int equipNum)
    {
        FindObjectOfType<PlayerAttack>().SubtractAttack(equipHolder[equipNum].GetComponent<Item>().attackDamageBoost);
        FindObjectOfType<PlayerAttack>().SubtractAttackSpeed(equipHolder[equipNum].GetComponent<Item>().attackSpeedBoost);
        FindObjectOfType<Health>().SubtractHealth(equipHolder[equipNum].GetComponent<Item>().healthBoost);
        FindObjectOfType<Health>().SubtractDefense(equipHolder[equipNum].GetComponent<Item>().defenseBoost);
        FindObjectOfType<Health>().SubtractMagicDefense(equipHolder[equipNum].GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<Health>().SubtractPhysicalDefense(equipHolder[equipNum].GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerMovement>().SubtractSpeed(equipHolder[equipNum].GetComponent<Item>().speedBoost);
    }

    public void Consume(int invNum)
    {
        if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Consumables)
        {
            EquipStats(invNum);
            itemList[invNum].GetComponent<Item>().BeingConsumed();
            itemList[invNum] = null;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        HideAllItems();

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                inventoryItemsImages[i].sprite = itemList[i].GetComponent<SpriteRenderer>().sprite;
                inventoryItemsImages[i].gameObject.SetActive(true);
            } 
            else
            {
                inventoryItemsImages[i].gameObject.SetActive(false);
            }
        }

        for (int k = 0; k < equipHolder.Length; k++)
        {
            if (equipHolder[k] != null)
            {
                equipImages[k].sprite = equipHolder[k].GetComponent<SpriteRenderer>().sprite;
                equipImages[k].gameObject.SetActive(true);
            }
            else
            {
                equipImages[k].gameObject.SetActive(false);
            }
        }

    }

    private void HideAllItems()
    {
        foreach (var item in itemList)
        {
            if (item != null)
                item.gameObject.SetActive(false);
        }

        foreach (var equip in equipHolder)
        {
            if (equip != null)
                equip.gameObject.SetActive(false);
        }
        HideItemInformationWindow();
    }

    private void ShowItemInformationWindow(int inventoryNum)
    {
        // Retrieve item name and item description
        itemName.text = itemList[inventoryNum].name;
        itemDescription.text = itemList[inventoryNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
    }

    private void HideItemInformationWindow()
    {
        // Hide the item information window and text
        itemName.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        itemInformationWindow.SetActive(false);
    }

    private void ShowEquipInformationWindow(int equipNum)
    {
        // Retrieve item name and item description
        itemName.text = equipHolder[equipNum].name;
        itemDescription.text = equipHolder[equipNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
    }

}
