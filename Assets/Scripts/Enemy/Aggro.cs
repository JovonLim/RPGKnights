using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] private float speed;
    [SerializeField] GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float offset;
    

    Animator anima;
    ArcaneArcher attack;
    MeleeAndRanged attack1;
    MeleeEnemy attack2;
    Golem attack3;
    RangeEnemy attack4;

    public enum EnemyType
    {
        archer, meleeAndRanged, melee, ranged,
    }

    public EnemyType et;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        switch (et)
        {
            case EnemyType.archer:
                attack = GetComponent<ArcaneArcher>();
                break;
            case EnemyType.meleeAndRanged:
                if (GetComponent<MeleeAndRanged>() != null)
                {
                    attack1 = GetComponent<MeleeAndRanged>();
                } else
                {
                    attack3 = GetComponent<Golem>();
                }
                
                break;
            case EnemyType.melee:
                attack2 = GetComponent<MeleeEnemy>();
                break;
            case EnemyType.ranged:
                attack4 = GetComponent<RangeEnemy>();
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
                case EnemyType.archer:
                    {
                        if (attack.PlayerInSight())
                        {
                            OnDisable();
                        }
                        else
                        {
                            Move();
                        }
                        break;
                    }
    
                case EnemyType.meleeAndRanged:
                    {
                        if (GetComponent<MeleeAndRanged>() != null)
                        {
                            if (attack1.PlayerInRangedSight())
                            {
                                OnDisable();
                            }
                            else
                            {
                                Move();
                            }
                        } else
                        {
                            if (attack3.PlayerInRangedSight())
                            {
                                OnDisable();
                            }
                            else
                            {
                                Move();
                            }
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
                    if (attack4.PlayerInSight())
                    {
                        OnDisable();
                    }
                    else
                    {
                        Move();
                    }
                    break;
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
