using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallThreshold;
    private Rigidbody2D body;
    private Animator anima;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool canDouble = false;
    private bool firstTime = true;
    private bool isFalling = false;
    private Vector3 previousPosition;
    private float highestPosition;
    
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Grab references from player object
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
            canDouble = true;
        } else if (Input.GetKeyDown(KeyCode.Space) && canDouble)
        {
            Jump();
            canDouble = false;
        }

        anima.SetFloat("yPos", body.velocity.y);

        if (!isGrounded())
        {
            if (transform.position.y < previousPosition.y && firstTime)
            {
                firstTime = false;
                isFalling = true;
                highestPosition = transform.position.y;
            }
            previousPosition = transform.position;
        }

        if (isGrounded() && isFalling)
        {
            if (highestPosition - transform.position.y > fallThreshold)
            {
                fallDamage();
            }
            isFalling = false;
            firstTime = true;
        }

            
        // Set animation parameters
        anima.SetBool("run", horizontalInput != 0);
        anima.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);   
    }

    // Use a box shape ray from the player to detect whether the ray hit the ground layer. If the player
    // is on the ground, the collider will return a non-null. Vice versa
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, 
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded();
    }

    void fallDamage()
    {
        GetComponent<Health>().TakeDamage(1);
    }

}
