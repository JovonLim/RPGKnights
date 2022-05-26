using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float enemyDamage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(enemyDamage);
    }
}
