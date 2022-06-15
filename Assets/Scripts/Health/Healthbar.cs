using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalPlayerHealth;
    [SerializeField] private Image currentPlayerHealth;

    private void Start()
    {
        UpdateStartingHealth();
    }

    private void Update()
    {
        
        currentPlayerHealth.fillAmount = playerHealth.currentHealth / 10;
        
    }

    public void UpdateStartingHealth()
    {
        totalPlayerHealth.fillAmount = playerHealth.currentHealth / 10;
    }

}
