using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 0.7f);
        transform.localPosition += new Vector3(0, 0.3f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
