using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anima;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool canDouble = false;
    private bool isFalling = false;
    private float maxVelocity;


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
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // Player jumping
        if (isGrounded() && !isFalling)
        {
            maxVelocity = 0;
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

       
        if (!isGrounded())
        {
            isFalling = true;
            if (body.velocity.y < maxVelocity)
            {
                maxVelocity = body.velocity.y;
            }
        }
        else if (isGrounded() && isFalling)
        {
            if (maxVelocity <= -20)
            {
                FallDamage();
            }
            isFalling = false;
        }


        // Set animation parameters
        anima.SetFloat("yPos", body.velocity.y);
        anima.SetBool("run", horizontalInput != 0);
        anima.SetBool("grounded", isGrounded());

    }

    public void FreezeMovement()
    {
        body.velocity = new Vector2(0, 0);
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
        return isGrounded() && WithinRangeOfAttack();
    }

    private bool WithinRangeOfAttack()
    {
        return horizontalInput < 0.05 || horizontalInput > -0.05;
    }

    void FallDamage()
    {
        GetComponent<Health>().TakeTrueDamage(0.5f);
    }

    public float GetSpeed()
    {
        return speed;
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
