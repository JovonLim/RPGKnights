using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;
    [SerializeField] private float enemySpeed;
    [SerializeField] private Animator anima;
    private Vector3 initialEnemyScale;
    private bool movingRight;


    private void Awake()
    {
        initialEnemyScale = enemy.localScale;
        anima = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (movingRight)
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                ChangeDirection();
        } 
        else
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                ChangeDirection();
        }
        
    }

    private void OnDisable()
    {
        anima.SetBool("moving", false);
    }

    private void ChangeDirection()
    {
        anima.SetBool("moving", false);
        movingRight = !movingRight;
    }

    private void MoveInDirection(int direction)
    {
        anima.SetBool("moving", true);
        // Direction
        enemy.localScale = new Vector3(Mathf.Abs(initialEnemyScale.x) * direction, 
            initialEnemyScale.y, initialEnemyScale.z);

        // Move 
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * enemySpeed, 
            enemy.position.y, enemy.position.z);
    }



}
