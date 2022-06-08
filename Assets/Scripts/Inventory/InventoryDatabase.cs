using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase : MonoBehaviour
{
    public static InventoryDatabase instance;
    public static List<GameObject> CurrentItems = new List<GameObject>();
    public static bool update;

    public static GameObject[] currentEquip = new GameObject[7];
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
        if (update)
        {
            foreach (GameObject item in CurrentItems)
            {
                if (!FindObjectOfType<Inventory>().ContainsItem(item))
                    FindObjectOfType<Inventory>().AddItem(item);
            }
            

            update = false;
        }
       
    }
}
