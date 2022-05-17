using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalPlayerHealth;
    [SerializeField] private Image currentPlayerHealth;

    private void Start()
    {
        totalPlayerHealth.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentPlayerHealth.fillAmount = playerHealth.currentHealth / 10;
    }

}
