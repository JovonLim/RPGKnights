using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]private float speed;
    private Rigidbody2D body;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);
    }
}
