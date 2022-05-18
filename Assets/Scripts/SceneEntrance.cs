using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerInteraction.instance.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
