using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerInteraction.instance != null)
        {
            PlayerInteraction.instance.transform.position = this.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
