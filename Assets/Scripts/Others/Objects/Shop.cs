using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    
    private bool playerInRange;
    private static bool shopOn = false;

    [SerializeField] private GameObject shopPanelWindow; 
    [SerializeField] public GameObject[] shopItems;
    [SerializeField] public int[] itemCosts;
    [SerializeField] public Image[] shopItemsImages;

    [SerializeField] public GameObject[] listOfItemPanels;
    [SerializeField] private static int currentPage;
    [SerializeField] private GameObject prevPanelButton;
    [SerializeField] private GameObject nextPanelButton;

    [SerializeField] private GameObject itemInformationWindow;
    [SerializeField] public TextMeshProUGUI itemName;
    [SerializeField] public TextMeshProUGUI itemDescription;
    [SerializeField] public TextMeshProUGUI itemCost;

    [SerializeField] private TextMeshProUGUI insufficentFundsText;

    [SerializeField] private TextMeshProUGUI shopCoinCounter;

    [SerializeField] private TextMeshProUGUI inventoryFullText;

    // Start is called before the first frame update
    void Start()
    {
        shopCoinCounter.text = UI.coins.ToString();
        shopPanelWindow.SetActive(shopOn);
        insufficentFundsText.gameObject.SetActive(shopOn);
        inventoryFullText.gameObject.SetActive(shopOn);
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] != null)
            {
                shopItemsImages[i].sprite = shopItems[i].GetComponent<SpriteRenderer>().sprite;
                shopItemsImages[i].gameObject.SetActive(true);
            }
            else
            {
                shopItemsImages[i].gameObject.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            ToggleShop();
        }    
    }

    private void ToggleShop()
    {
        shopOn = !shopOn;
        shopPanelWindow.SetActive(shopOn);
        shopCoinCounter.text = UI.coins.ToString();
        ResetPanels();

        if (shopOn)
        {
            //PauseGame();
        }
        else
        {
            //ResumeGame();
        }
    }

    public bool IsShopOn()
    {
        return shopOn;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void PurchaseItem(int slotNum)
    {
        if (!FindObjectOfType<Inventory>().IsInventoryFull())
        {
            if (UI.coins >= itemCosts[slotNum])
            {
                UI.coins -= itemCosts[slotNum];
                shopCoinCounter.text = UI.coins.ToString();
                GameObject purchased = Instantiate(shopItems[slotNum]);
                purchased.name = shopItems[slotNum].name;
                purchased.gameObject.SetActive(true);
                FindObjectOfType<Inventory>().AddItem(purchased);
            }
            else
            {
                StartCoroutine(ShowInsufficientFunds(0.5f));
            }
        } 
        else
        {
            StartCoroutine(ShowInventoryFull(0.5f));
        }
        
        
    }

    public void ShowItemInformationWindow(int slotNum)
    {
        itemName.text = shopItems[slotNum].name;
        itemDescription.text = shopItems[slotNum].GetComponent<Item>().itemDescription;
        itemCost.text = itemCosts[slotNum].ToString();

        // Show the item information window and text
        itemInformationWindow.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
        itemCost.gameObject.SetActive(true);
    }

    public void HideItemInformationWindow()
    {
        // Hide the item information window and text
        itemName.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        itemCost.gameObject.SetActive(false);
        itemInformationWindow.SetActive(false);
    }

    IEnumerator ShowInsufficientFunds(float time)
    {
        insufficentFundsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        insufficentFundsText.gameObject.SetActive(false);
    }

    IEnumerator ShowInventoryFull(float time)
    {
        inventoryFullText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        inventoryFullText.gameObject.SetActive(false);
    }

    public void ResetPanels()
    {
        currentPage = 0;
        listOfItemPanels[0].gameObject.SetActive(true);
        for (int i = 1; i < listOfItemPanels.Length; i++)
        {
            listOfItemPanels[i].gameObject.SetActive(false);
        }
        UpdateButton();
    }

    public void NextPanel()
    {
        currentPage += 1;
        listOfItemPanels[currentPage - 1].gameObject.SetActive(false);
        listOfItemPanels[currentPage].gameObject.SetActive(true);
        UpdateButton();
    }

    public void PrevPanel()
    {
        currentPage -= 1;
        listOfItemPanels[currentPage + 1].gameObject.SetActive(false);
        listOfItemPanels[currentPage].gameObject.SetActive(true);
        UpdateButton();
    }

    public void UpdateButton()
    {
        if (currentPage == 0)
        {
            // Currently at first panel
            prevPanelButton.gameObject.SetActive(false);
            nextPanelButton.gameObject.SetActive(true);
        } 
        else if (currentPage == (listOfItemPanels.Length - 1))
        {
            //Currently at last panel
            nextPanelButton.gameObject.SetActive(false);
            prevPanelButton.gameObject.SetActive(true);

        }
        else
        {
            prevPanelButton.gameObject.SetActive(true);
            nextPanelButton.gameObject.SetActive(true);
        }
    }

}
