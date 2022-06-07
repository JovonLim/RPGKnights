using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int totalInvCapacity = 9;
    public GameObject[] itemList;
    public GameObject itemPanelWindow;
    public Image[] inventoryItemsImages;

    public GameObject itemInformationWindow;
    public Text itemName;
    public Text itemDescription;

    public static List<GameObject> equipHolder = new List<GameObject>();
    public GameObject equipWindow;
    public Image[] equipImages;

    public static bool inventoryOn = false;

    private void Start()
    {
        itemPanelWindow.SetActive(inventoryOn);
        itemList = new GameObject[9];
        for (int i = 0; i < itemList.Length; i++)
        {
            itemList[i] = null;
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
    }

    public bool IsInventoryOn()
    {
        return inventoryOn;
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                itemList[i] = item;
                break;
            }
        }
        UpdateUI();

    }

    public void RemoveItem(int itemNum)
    {
        return;
          
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

    }

    private void HideAllItems()
    {
        foreach (var item in itemList)
        {
            item.gameObject.SetActive(false);
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
        itemName.text = equipHolder[equipNum].name;
        itemDescription.text = itemList[equipNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
    }

}
