using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    bool Used;

    // Update is called once per frame
    void Update()
    {
        if (!Used)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = this.transform.position;
            Used = true;
        }
    }
}
