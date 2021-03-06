using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField]
    private int minCoin = 3;
    [SerializeField]
    private int maxCoin = 6;
    [SerializeField]
    private float offset;
    private int count;
    

    void Start()
    {
        count = Random.Range(minCoin, maxCoin);
    }

    [SerializeField]
    private GameObject prefab = null;

    [SerializeField]
    private GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(this.prefab, new Vector2(transform.position.x, transform.position.y + offset), Quaternion.identity);
        }
    }
}
