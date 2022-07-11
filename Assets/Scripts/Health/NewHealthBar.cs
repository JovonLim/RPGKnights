using UnityEngine;
using UnityEngine.UI;

public class NewHealthBar : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalPlayerHealth;
    [SerializeField] private Image currentPlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerHealth.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
    }
}
