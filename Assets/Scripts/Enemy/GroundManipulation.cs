using UnityEngine;

public class GroundManipulation : MonoBehaviour
{
    [SerializeField] private float castCooldown;
    [SerializeField] private float attackRange;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject dissappearingGround;

    [SerializeField] private float spellDuration;
    private float spellActiveDuration = 0;

    private static bool casting = false;

    private Animator anima;

    private float spellCooldownTimer = float.MaxValue;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (casting && spellActiveDuration < spellDuration)
        {
            spellActiveDuration += Time.deltaTime;
            spellCooldownTimer = 0;
        }
        else if (casting && spellActiveDuration >= spellDuration)
        {
            spellActiveDuration = 0;
            FinishSpell();
            spellCooldownTimer += Time.deltaTime;
        }
        else
        {
            if (PlayerInSight())
            {
                if (spellCooldownTimer >= castCooldown)
                {
                    spellCooldownTimer = 0;
                    anima.SetTrigger("cast");
                }
            }
        }
    }

    private void CastSpell()
    {
        casting = true;
        dissappearingGround.SetActive(false);
    }

    private void FinishSpell()
    {
        casting = false;
        dissappearingGround.SetActive(true);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
