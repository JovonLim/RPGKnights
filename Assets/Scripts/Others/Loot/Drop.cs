using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject[] Loots;
    [SerializeField] private int chanceOfDropping;

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
                    new Vector2(transform.position.x, transform.position.y - offset),
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
        int rand = Random.Range(1, 101);
        return rand <= chanceOfDropping;
    }
}
