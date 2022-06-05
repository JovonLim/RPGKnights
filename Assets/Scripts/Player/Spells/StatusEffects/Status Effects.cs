using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    [SerializeField] float duration; 
    [SerializeField] float startTime;
    [SerializeField] float repeatTime; 

    public GameObject enemy;
    private bool applied = false;

    void Update()
    {
        if (enemy != null)
        {
            if (!applied)
            {
                 applied = true;
                if (repeatTime > 0)
                    InvokeRepeating("ApplyEffect", startTime, repeatTime);
                else
                    Invoke("ApplyEffect", startTime);
                // End the effect accordingly to the duration
                Invoke("EndEffect", duration);
               
            }
        }
        // Apply the effect repeated over time or direct?
       
    }

    protected virtual void ApplyEffect()
    {
    }

    protected virtual void EndEffect()
    {
        CancelInvoke();
    }
}
