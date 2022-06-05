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
        HideAll();
        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryItemsImages[i].sprite = itemList[i].GetComponent<SpriteRenderer>().sprite;
            inventoryItemsImages[i].gameObject.SetActive(true); 
        }
    }

    private void HideAll()
    {
        foreach (var i in itemList)
        {
            i.gameObject.SetActive(false);
        }
    }
}
