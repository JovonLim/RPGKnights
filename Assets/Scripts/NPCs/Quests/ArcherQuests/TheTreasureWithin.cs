using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTreasureWithin : MonoBehaviour
{
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.ArcherQuest != null)
        {
            if (PlayerQuestInteraction.ArcherQuest.questNum == 1)
            {
                door.SetActive(true);
            }
        }
    }
}
