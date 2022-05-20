using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
  
    public GameObject prefab = null;

    public GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }

    public void Spawn()
    {
        Instantiate(this.prefab, new Vector2(this.transform.position.x - 0.2f, this.transform.position.y), Quaternion.identity);
        Instantiate(this.prefab, this.transform.position, Quaternion.identity);
        Instantiate(this.prefab, new Vector2(this.transform.position.x + 0.2f, this.transform.position.y), Quaternion.identity);

    }
}
