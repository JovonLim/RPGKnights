using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private bool hit;
    private float direction;

    // References
    private BoxCollider2D boxCollider;
    private Animator anima;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;

        // Move the projectile every frame if it does not hit anything
        float movementSpeed = projectileSpeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        hit = true;
        boxCollider.enabled = false;
        anima.SetTrigger("explode");
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Make sure projectile shot is same direction as the character
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
