using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEmerges : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.questActive && PlayerInteraction.questNum == 4)
        {
            door.SetActive(false);
            boss.SetActive(true);
        }
    }
}
