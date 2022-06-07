using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static int currentInvCapacity;
    public const int totalInvCapacity = 9;
    public static GameObject[] itemList = new GameObject[9];
    public GameObject itemPanelWindow;
    public Image[] inventoryItemsImages;

    public GameObject itemInformationWindow;
    public Text itemName;
    public Text itemDescription;

    public static GameObject[] equipHolder;
    public GameObject equipWindow;
    public Image[] equipImages;

    public static bool inventoryOn = false;

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

    public void RemoveItem(int invNum)
    {
        if (itemList[invNum] != null)
        {
            itemList[invNum] = null;
            UpdateUI();
        } 
        else
        {
            return;
        } 
    }

    public void Equip(int invNum)
    {
        if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.WarriorWeapon)
        {
            if (equipHolder[0] != null)
            {
                Unequip(0);
            }
            equipHolder[0] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Helmet)
        {
            if (equipHolder[1] != null)
            {
                Unequip(1);
            }
            equipHolder[1] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ChestPiece)
        {
            if (equipHolder[2] != null)
            {
                Unequip(2);
            }
            equipHolder[2] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Leggings)
        {
            if (equipHolder[3] != null)
            {
                Unequip(3);
            }
            equipHolder[3] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Boots)
        {
            if (equipHolder[4] != null)
            {
                Unequip(4);
            }
            equipHolder[4] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            if (equipHolder[5] != null)
            {
                Unequip(5);
            }
            equipHolder[5] = itemList[invNum];
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.MageWeapon)
        {
            if (equipHolder[6] != null)
            {
                Unequip(6);
            }
            equipHolder[6] = itemList[invNum];
        } 
        else
        {
            return;
        }
        RemoveItem(invNum);
        UpdateUI();
    }

    public void Unequip(int equipNum)
    {
        if (equipHolder[equipNum] != null)
        {
            AddItem(equipHolder[equipNum]);
            equipHolder[equipNum] = null;
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

    public void ShowItemInformationWindow(int inventoryNum)
    {
        // Retrieve item name and item description
        itemName.text = itemList[inventoryNum].name;
        itemDescription.text = itemList[inventoryNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
    }

    public void HideItemInformationWindow()
    {
        // Hide the item information window and text
        itemName.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        itemInformationWindow.SetActive(false);
    }

    public void ShowEquipInformationWindow(int equipNum)
    {
        // Retrieve item name and item description
       // itemName.text = equipHolder[equipNum].name;
     //   itemDescription.text = itemList[equipNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
    }

}
