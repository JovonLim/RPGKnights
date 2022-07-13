using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDrop : MonoBehaviour
{
    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject[] Loots;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private float chanceOfDropping;

    private GameObject dropItem;

    public void DropLoots()
    {
        for (int i = 0; i < Loots.Length; i++)
        {
            if (willDrop())
            {
                dropItem = Loots[i];
                dropItem.gameObject.SetActive(true);
                var obj = Instantiate(dropItem,
                    new Vector2(dropPoint.position.x, dropPoint.position.y),
                    transform.rotation);
                obj.name = dropItem.name;
            } else
            {
                continue;
            }
            
        }
    }

    private bool willDrop()
    {
        float ceiling = chanceOfDropping * 100;
        float rand = Random.Range(1, 101);
        return rand <= ceiling;
    }
}
