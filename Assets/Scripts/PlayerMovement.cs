using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]private float speed;
    private Rigidbody2D body;
    private Animator anima;
    private bool grounded;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Grab references from player object
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Player jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Set animation parameters
        anima.SetBool("run", horizontalInput != 0);
        anima.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anima.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
