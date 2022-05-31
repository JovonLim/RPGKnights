using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBegin : QuestObjective
{
    [SerializeField] GameObject questDialog;
    [SerializeField] GameObject startingText;
    // Start is called before the first frame update
    void Start()
    {
        if (objective == Objective.mobs || objective == Objective.chests)
        {
            StartCoroutine(Begin());
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Begin()
    {
        questDialog.SetActive(true);
        startingText.SetActive(true);
        yield return new WaitForSeconds(2);
        questDialog.SetActive(false);
        startingText.SetActive(false);
    }
}
