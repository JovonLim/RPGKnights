using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    Animator animator;
    private bool playerInRange;
    private static bool shopOn = false;

    [SerializeField] private GameObject shopPanelWindow; 
    [SerializeField] public GameObject[] shopItems;
    [SerializeField] public int[] itemCosts;
    [SerializeField] public Image[] shopItemsImages = new Image[6];


    [SerializeField] private GameObject itemInformationWindow;
    [SerializeField] public TextMeshProUGUI itemName;
    [SerializeField] public TextMeshProUGUI itemDescription;
    [SerializeField] public TextMeshProUGUI itemCost;

    [SerializeField] private TextMeshProUGUI insufficentFundsText;

    [SerializeField] private TextMeshProUGUI shopCoinCounter;

    // Start is called before the first frame update
    void Start()
    {
        UI.coins = 10;
        animator = GetComponent<Animator>();
        shopCoinCounter.text = UI.coins.ToString();
        shopPanelWindow.SetActive(shopOn);
        insufficentFundsText.gameObject.SetActive(shopOn);
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItemsImages[i].sprite = shopItems[i].GetComponent<SpriteRenderer>().sprite;
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
    }

    public bool IsShopOn()
    {
        return shopOn;
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
        if (UI.coins >= itemCosts[slotNum])
        {
            UI.coins -= itemCosts[slotNum];
            shopCoinCounter.text = UI.coins.ToString();
            FindObjectOfType<Inventory>().AddItem(shopItems[slotNum]);
        }
        else
        {
            StartCoroutine(ShowInsufficientFunds(0.5f));
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

}
