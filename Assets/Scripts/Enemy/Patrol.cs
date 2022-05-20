using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    private float speed = 3.0f;

    Animator anima;
    Vector3 newPos;
    private Vector3 initialEnemyScale;
  
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
            transform.localScale = Vector3.one;

        } else if (transform.position == pos1.position)
        {
            newPos = pos2.position;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        anima.SetBool("moving", true);
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        anima.SetBool("moving", false);
    }
}

