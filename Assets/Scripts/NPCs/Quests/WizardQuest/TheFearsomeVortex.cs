using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFearsomeVortex : MonoBehaviour
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
        if (PlayerQuestInteraction.questActive && PlayerQuestInteraction.WizardQuest != null)
        {
            if (PlayerQuestInteraction.WizardQuest.questNum == 3)
            {
                door.SetActive(true);
            }
        }
    }
}
