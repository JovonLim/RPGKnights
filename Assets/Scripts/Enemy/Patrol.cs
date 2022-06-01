using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    [SerializeField] private float speed;

    Animator anima;
    Vector3 newPos;
    float timer = 1.0f;
  
    // Start is called before the first frame update
    void Start()
    {
  
        newPos = pos1.position;
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos2.position)
        {
            newPos = pos1.position;
            if (timer > 0)
            {
                OnDisable();
                timer -= Time.deltaTime;
            } else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, 1);
                Move();
            }
        }
        else if (transform.position == pos1.position)
        {
            newPos = pos2.position;
            if (timer > 0)
            {
                OnDisable();
                timer -= Time.deltaTime;
            }
            else
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, 1);
                Move();

            }
        } else
        {
            timer = 1.0f;
            Move();
        }
        
        
    }
        

    private void OnDisable()
    {
        anima.SetBool("moving", false);
    }

    private void Move()
    {
        anima.SetBool("moving", true);
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }
}

