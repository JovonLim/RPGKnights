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
    [SerializeField] private TextMeshProUGUI itemType;

    public static GameObject[] equipHolder;
    [SerializeField] private GameObject equipWindow;
    [SerializeField] private Image[] equipImages;

    private static bool inventoryOn = false;

    private static bool hasBuff = false;
    private Coroutine currCoroutine;
    private GameObject currItemConsumed;

    [SerializeField] private TextMeshProUGUI meleeAD;
    [SerializeField] private TextMeshProUGUI meleeAS;
    [SerializeField] private TextMeshProUGUI rangedAD;
    [SerializeField] private TextMeshProUGUI rangedAS;
    [SerializeField] private TextMeshProUGUI physicalDef;
    [SerializeField] private TextMeshProUGUI magicDef;
    [SerializeField] private TextMeshProUGUI speed;

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
        equipHolder = new GameObject[8];
        for (int i = 0; i < equipHolder.Length; i++)
        {
            equipHolder[i] = null;
        }
        currentInvCapacity = 0;
        UpdateStats();

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
        FindObjectOfType<PlayerMovement>().FreezeMovement();
        FindObjectOfType<PlayerMovement>().enabled = !inventoryOn;
        FindObjectOfType<PlayerAttack>().enabled = !inventoryOn;
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
        currentInvCapacity += 1;
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
            currentInvCapacity -= 1;
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
            InventoryDatabase.CurrentItems.RemoveAt(posToRemove);
            itemList[invNum].GetComponent<Item>().BeingDropped();
            itemList[invNum] = null;
            currentInvCapacity -= 1;
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
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Necklace)
        {
            if (equipHolder[5] != null)
            {
                Unequip(5);
            }
            
            equipHolder[5] = itemToBeEquipped;
            
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Ring)
        {
            if (equipHolder[6] != null)
            {
                Unequip(6);
            }

            equipHolder[6] = itemToBeEquipped;
            
        }
        else if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            if (equipHolder[7] != null)
            {
                Unequip(7);
            }

            equipHolder[7] = itemToBeEquipped;

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
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.Necklace)
        {
            if (equipHolder[5] != null)
            {
                Unequip(5);
            }

            equipHolder[5] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.Ring)
        {
            if (equipHolder[6] != null)
            {
                Unequip(6);
            }

            equipHolder[6] = equip;

        }
        else if (equip.GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            if (equipHolder[7] != null)
            {
                Unequip(7);
            }

            equipHolder[7] = equip;

        }
        else
        {
            return;
        }

        if (equip.GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            FindObjectOfType<PlayerAttack>().AddRangedAttack(equip.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddRangedAttackSpeed(equip.GetComponent<Item>().attackSpeedBoost);
        } else
        {
            FindObjectOfType<PlayerAttack>().AddAttack(equip.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddAttackSpeed(equip.GetComponent<Item>().attackSpeedBoost);
        }    
        FindObjectOfType<PlayerHealth>().AddHealth(equip.GetComponent<Item>().healthBoost);
        FindObjectOfType<PlayerHealth>().AddDefense(equip.GetComponent<Item>().defenseBoost);
        FindObjectOfType<PlayerHealth>().AddMagicDefense(equip.GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<PlayerHealth>().AddPhysicalDefense(equip.GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerHealth>().GainHealth(equip.GetComponent<Item>().regenerateHealth);
        FindObjectOfType<PlayerMovement>().AddSpeed(equip.GetComponent<Item>().speedBoost);
        UpdateUI();
    }

    public void EquipStats(int invNum)
    {
        if (itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            FindObjectOfType<PlayerAttack>().AddRangedAttack(itemList[invNum].GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddRangedAttackSpeed(itemList[invNum].GetComponent<Item>().attackSpeedBoost);
        }
        else
        {
            FindObjectOfType<PlayerAttack>().AddAttack(itemList[invNum].GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddAttackSpeed(itemList[invNum].GetComponent<Item>().attackSpeedBoost);
        }
        
        FindObjectOfType<PlayerHealth>().AddHealth(itemList[invNum].GetComponent<Item>().healthBoost);
        FindObjectOfType<PlayerHealth>().AddDefense(itemList[invNum].GetComponent<Item>().defenseBoost);
        FindObjectOfType<PlayerHealth>().AddMagicDefense(itemList[invNum].GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<PlayerHealth>().AddPhysicalDefense(itemList[invNum].GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerHealth>().GainHealth(itemList[invNum].GetComponent<Item>().regenerateHealth);
        FindObjectOfType<PlayerMovement>().AddSpeed(itemList[invNum].GetComponent<Item>().speedBoost);
        
    }

    public void EquipStats(GameObject item)
    {
        if (item.GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            FindObjectOfType<PlayerAttack>().AddRangedAttack(item.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddRangedAttackSpeed(item.GetComponent<Item>().attackSpeedBoost);
        } 
        else
        {
            FindObjectOfType<PlayerAttack>().AddAttack(item.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().AddAttackSpeed(item.GetComponent<Item>().attackSpeedBoost);
        }
        FindObjectOfType<PlayerHealth>().AddHealth(item.GetComponent<Item>().healthBoost);
        FindObjectOfType<PlayerHealth>().AddDefense(item.GetComponent<Item>().defenseBoost);
        FindObjectOfType<PlayerHealth>().AddMagicDefense(item.GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<PlayerHealth>().AddPhysicalDefense(item.GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerHealth>().GainHealth(item.GetComponent<Item>().regenerateHealth);
        FindObjectOfType<PlayerMovement>().AddSpeed(item.GetComponent<Item>().speedBoost);
    }

    public void Unequip(int equipNum)
    {
        if (equipHolder[equipNum] != null && !IsInventoryFull())
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
        if (equipNum == 7)
        {
            FindObjectOfType<PlayerAttack>().SubtractRangedAttack(equipHolder[equipNum].GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().SubtractRangedAttackSpeed(equipHolder[equipNum].GetComponent<Item>().attackSpeedBoost);
        } 
        else
        {
            FindObjectOfType<PlayerAttack>().SubtractAttack(equipHolder[equipNum].GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().SubtractAttackSpeed(equipHolder[equipNum].GetComponent<Item>().attackSpeedBoost);
        }
        FindObjectOfType<PlayerHealth>().SubtractHealth(equipHolder[equipNum].GetComponent<Item>().healthBoost);
        FindObjectOfType<PlayerHealth>().SubtractDefense(equipHolder[equipNum].GetComponent<Item>().defenseBoost);
        FindObjectOfType<PlayerHealth>().SubtractMagicDefense(equipHolder[equipNum].GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<PlayerHealth>().SubtractPhysicalDefense(equipHolder[equipNum].GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerMovement>().SubtractSpeed(equipHolder[equipNum].GetComponent<Item>().speedBoost);
    }

    public void UnequipStats(GameObject item)
    {
        if (item.GetComponent<Item>().itemType == Item.ItemType.ArcherWeapon)
        {
            FindObjectOfType<PlayerAttack>().SubtractRangedAttack(item.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().SubtractRangedAttackSpeed(item.GetComponent<Item>().attackSpeedBoost);
        }
        else
        {
            FindObjectOfType<PlayerAttack>().SubtractAttack(item.GetComponent<Item>().attackDamageBoost);
            FindObjectOfType<PlayerAttack>().SubtractAttackSpeed(item.GetComponent<Item>().attackSpeedBoost);
        }
        
        FindObjectOfType<PlayerHealth>().SubtractHealth(item.GetComponent<Item>().healthBoost);
        FindObjectOfType<PlayerHealth>().SubtractDefense(item.GetComponent<Item>().defenseBoost);
        FindObjectOfType<PlayerHealth>().SubtractMagicDefense(item.GetComponent<Item>().magicDefenseBoost);
        FindObjectOfType<PlayerHealth>().SubtractPhysicalDefense(item.GetComponent<Item>().physicalDefenseBoost);
        FindObjectOfType<PlayerMovement>().SubtractSpeed(item.GetComponent<Item>().speedBoost);
    }

    public void Consume(int invNum)
    {
        if (itemList[invNum] != null && itemList[invNum].GetComponent<Item>().itemType == Item.ItemType.Consumables)
        {
            if (itemList[invNum].GetComponent<Item>().isOverTimeEffect)
            {
                if (hasBuff)
                {
                    StopCoroutine(currCoroutine);
                    UnequipStats(currItemConsumed);
                    currCoroutine = StartCoroutine(ConsumeOverTime(itemList[invNum]));
                    currItemConsumed = itemList[invNum];
                    RemoveItem(invNum);
                    UpdateUI();
                }
                else
                {
                    currCoroutine = StartCoroutine(ConsumeOverTime(itemList[invNum]));
                    currItemConsumed = itemList[invNum];
                    RemoveItem(invNum);
                    UpdateUI();
                }
                
            } 
            else
            {
                EquipStats(invNum);
                itemList[invNum].GetComponent<Item>().BeingConsumed();
                RemoveItem(invNum);
                UpdateUI();
            }
            
        } 
        else
        {
            return;
        }
    }

    IEnumerator ConsumeOverTime(GameObject item)
    {
        EquipStats(item);
        hasBuff = true;
        yield return new WaitForSeconds(item.GetComponent<Item>().duration);
        UnequipStats(item);
        hasBuff = false;
        item.GetComponent<Item>().BeingConsumed();
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
        UpdateStats();

    }

    private void UpdateStats()
    {
        meleeAD.text = FindObjectOfType<PlayerAttack>().GetAttack().ToString();
        meleeAS.text = FindObjectOfType<PlayerAttack>().GetAttackSpeed().ToString();
        rangedAD.text = FindObjectOfType<PlayerAttack>().GetRangedAttack().ToString();
        rangedAS.text = FindObjectOfType<PlayerAttack>().GetRangedAttackSpeed().ToString();
        physicalDef.text = FindObjectOfType<PlayerHealth>().GetPhysicalDefense().ToString();
        magicDef.text = FindObjectOfType<PlayerHealth>().GetMagicDefense().ToString();
        speed.text = FindObjectOfType<PlayerMovement>().GetSpeed().ToString();

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
        itemName.text = itemList[inventoryNum].GetComponent<Item>().itemName;
        itemDescription.text = itemList[inventoryNum].GetComponent<Item>().itemDescription;
        itemType.text = itemList[inventoryNum].GetComponent<Item>().itemType.ToString();

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
        itemType.gameObject.SetActive(true);
    }

    private void HideItemInformationWindow()
    {
        // Hide the item information window and text
        itemName.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        itemInformationWindow.SetActive(false);
        itemType.gameObject.SetActive(false);
    }

    private void ShowEquipInformationWindow(int equipNum)
    {
        // Retrieve item name and item description
        itemName.text = equipHolder[equipNum].GetComponent<Item>().itemName;
        itemDescription.text = equipHolder[equipNum].GetComponent<Item>().itemDescription;

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
        itemType.gameObject.SetActive(true);
    }

}
