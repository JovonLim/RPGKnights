using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewHealthBar : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalPlayerHealth;
    [SerializeField] private Image currentPlayerHealth;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private TextMeshProUGUI totalHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerHealth.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
        currentHealth.text = playerHealth.currentHealth.ToString("F1");
        totalHealth.text = playerHealth.GetStartingHealth().ToString("F1");
    }
}
