using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    public Transform FollowTarget;
    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        if (Player != null)
        {
            FollowTarget = Player.transform;
            vcam.Follow = FollowTarget;
        }
}

    // Update is called once per frame
    void Update()
    {

    }
}
