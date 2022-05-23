using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform Pos1, Pos2;
    public float speed;
    Vector3 nextPos;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = Pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == Pos1.position)
        {
            nextPos = Pos2.position;
        } else if (transform.position == Pos2.position)
        {
            nextPos = Pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }

}
