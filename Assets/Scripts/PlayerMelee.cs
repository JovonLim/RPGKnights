using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private Animator anima;
    private PlayerMovement playerMove;
    private float attackCooldownTimer = float.MaxValue;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0) && attackCooldownTimer > attackSpeed && playerMove.canAttack())
            Attack();

        attackCooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anima.SetTrigger("melee");
        attackCooldownTimer = 0;
    }
}
