using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InventoryDatabase : MonoBehaviour, IDataPersistence
{
    public static InventoryDatabase instance;
    public static List<GameObject> CurrentItems = new List<GameObject>();
    public static bool update;

    public static List<GameObject> currentEquip = new List<GameObject>();

    public void LoadData(GameData data)
    {
        List<GameObject> items = new List<GameObject>();
        List<GameObject> equips = new List<GameObject>();
        for (int i = 0; i < data.currentItems.Count; i++)
        {
            GameObject item = Resources.Load("" + data.currentItems[i]) as GameObject;
            GameObject itemToAdd = Instantiate(item);
            items.Add(itemToAdd);
        }
        for (int i = 0; i < data.currentEquip.Count; i++)
        {
            GameObject equip = Resources.Load("" + data.currentEquip[i]) as GameObject;
            GameObject equipToAdd = Instantiate(equip);           
            equips.Add(equipToAdd);
        }
        CurrentItems = items;
        currentEquip = equips; 
        update = true;
    }

    public void SaveData(GameData data)
    {
        List<int> items = new List<int>();
        List<int> equips = new List<int>();
        foreach (GameObject item in CurrentItems)
        {
            items.Add(item.GetComponent<Item>().itemId);
            
        }
        foreach (GameObject item in currentEquip)
        {
            equips.Add(item.GetComponent<Item>().itemId);
           
        }
        data.currentItems = items;
        data.currentEquip = equips;
    }

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (update && SceneManager.GetActiveScene().buildIndex == 2)
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }

         
            foreach (GameObject item in CurrentItems)
            {
                FindObjectOfType<Inventory>().AddItem(item);
                item.transform.SetParent(instance.transform);
            }

            foreach (GameObject equip in currentEquip)
            {

                FindObjectOfType<Inventory>().DirectEquip(equip);
                equip.transform.SetParent(instance.transform);

            }

            update = false;
        }
       
    }
}
