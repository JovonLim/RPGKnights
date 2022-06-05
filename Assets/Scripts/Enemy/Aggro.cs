using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;   
    [SerializeField] GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float offset;
    public float speed;

    Animator anima;
    MeleeAndRanged attack1;
    MeleeEnemy attack2;
    RangeEnemy attack3;

    public enum EnemyType
    {
        meleeAndRanged, melee, ranged,
    }

    public EnemyType et;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        switch (et)
        {
            case EnemyType.meleeAndRanged:
                {
                attack1 = GetComponent<MeleeAndRanged>();       
                }
                break;
            case EnemyType.melee:
                attack2 = GetComponent<MeleeEnemy>();
                break;
            case EnemyType.ranged:
                attack3 = GetComponent<RangeEnemy>();
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CheckPlayer())
        {
            switch (et)
            {
             
                case EnemyType.meleeAndRanged:
                    {  
                            if (attack1.PlayerInRangedSight())
                            {
                                OnDisable();
                            }
                            else
                            {
                                Move();
                            }                            
                        break;
                    }
                case EnemyType.melee:
                    {
                        if (attack2.PlayerInSight())
                        {
                            OnDisable();
                        }
                        else
                        {
                            Move();
                        }
                        break;
                    }
                case EnemyType.ranged:
                    {
                        if (attack3.PlayerInSight())
                        {
                            OnDisable();
                        }
                        else
                        {
                            Move();
                        }
                        break;
                    }
            }
        } else
        {
            OnDisable();
        }
    }

    bool CheckPlayer()
    {
        Collider2D playerEnter = Physics2D.OverlapArea(pos1.position, pos2.position, playerLayer);
        if (playerEnter != null)
        {
            player = playerEnter.gameObject;
        }
        return playerEnter != null;
    }

    private void OnDisable()
    {  
        anima.SetBool("moving", false);
    }

    private void Move()
    {
        direction();
        anima.SetBool("moving", true);
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + offset), speed * Time.deltaTime);
    }

    private void direction()
    {
        Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
        if (relativePoint.x < 0.0)
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        else if (relativePoint.x >= 0.0)
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
    }
}
