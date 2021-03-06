using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ReturnToTown : MonoBehaviour
{
    [SerializeField] private GameObject warning;
    private bool Delayed;
    private bool playerInRange;
    private bool playerClicked;
    // Start is called before the first frame update
    void Start()
    {
        warning.SetActive(false);
        StartCoroutine(SetDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Delayed)
        {
            playerClicked = true;
        }
        if (playerClicked && playerInRange)
        {
            warning.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            warning.SetActive(false);
            playerClicked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void Cancel()
    {
        warning.SetActive(false);
        playerClicked = false;
        Time.timeScale = 1;
    }

    public void Accept()
    {
        Time.timeScale = 1;
        CoinCounter.coins = (int) (CoinCounter.coins * 0.9);
        DataPersistenceManager.instance.SaveGame();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject UITab = GameObject.FindGameObjectWithTag("UI");
        Destroy(player);
        Destroy(UITab);
        SceneManager.LoadScene(2);
        InventoryDatabase.update = true;
    }

    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Delayed = true;
    }
}
