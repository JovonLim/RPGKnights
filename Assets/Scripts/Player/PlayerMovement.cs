using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallThreshold;
    private Rigidbody2D body;
    private Animator anima;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool canDouble = false;
    private bool isFalling = false;
    private float jumpDelay = 0.5f;
    private float jumpCondition = 0;
    private float fallTimer = 0;
    
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Grab references from player object
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlayerInteracting())
        {
            body.velocity = new Vector2(0, 0);
            body.isKinematic = true;

            anima.SetBool("run", false);
            anima.SetBool("grounded", false);
        } 
        
        else
        {
            body.isKinematic = false;
            horizontalInput = Input.GetAxis("Horizontal");

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // Flip player when moving
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);


            // Player jumping
            if (isGrounded())
            {
                jumpCondition = jumpDelay;
            }
            if (jumpCondition > 0)
            {
                jumpCondition -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    canDouble = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && canDouble)
            {
                Jump();
                canDouble = false;
            }

            anima.SetFloat("yPos", body.velocity.y);

            if (!isGrounded())
            {
                isFalling = true;
                if (body.velocity.y < 0)
                {
                    fallTimer += Time.deltaTime;
                }
                else
                {
                    fallTimer = 0;
                }
            }
            else if (isGrounded() && isFalling)
            {
                if (fallTimer > 0.6f)
                {
                    FallDamage();
                    isFalling = false;
                    fallTimer = 0;
                }
            }


            // Set animation parameters
            anima.SetBool("run", horizontalInput != 0);
            anima.SetBool("grounded", isGrounded());
        }
        
        
    }

    private bool isPlayerInteracting()
    {
        return FindObjectOfType<Inventory>().IsInventoryOn();
    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);   
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

    void FallDamage()
    {
        GetComponent<Health>().TakeTrueDamage(1);
    }

    public void AddSpeed(float amt)
    {
        speed += amt;
    }

    public void SubtractSpeed(float amt)
    {
        speed -= amt;
    }

}
