using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public GameObject itemPanelWindow;
    public Image[] inventoryItemsImages;

    public GameObject itemInformationWindow;
    public Text itemName;
    public Text itemDescription;

    public bool inventoryOn = false;

    private void Start()
    {
        itemPanelWindow.SetActive(inventoryOn);
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

    public void AddItem(GameObject item)
    {
        itemList.Add(item);
        UpdateUI();
    }

    private void UpdateUI()
    {
        HideAllItems();
        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryItemsImages[i].sprite = itemList[i].GetComponent<SpriteRenderer>().sprite;
            inventoryItemsImages[i].gameObject.SetActive(true); 
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
}
