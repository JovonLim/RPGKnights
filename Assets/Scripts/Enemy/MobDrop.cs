using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDrop : MonoBehaviour
{
    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject[] Loots;
    [SerializeField] private EnemyHealth health;

    private GameObject dropItem;

    private bool hasDropped = false;

    // Update is called once per frame
    void Update()
    {
        if (health.IsDefeated() && !hasDropped)
        {
            DropLoots();
        }
    }

    public void DropLoots()
    {
        for (int i = 0; i < Loots.Length; i++)
        {
            dropItem = Loots[i];
            dropItem.gameObject.SetActive(true);
            var obj = Instantiate(dropItem,
                new Vector2(dropPoint.position.x, dropPoint.position.y),
                transform.rotation);
            obj.name = dropItem.name;
        }
        hasDropped = true;
    }
}
