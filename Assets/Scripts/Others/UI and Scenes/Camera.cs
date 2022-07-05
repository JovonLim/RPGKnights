using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    public Transform FollowTarget;
    private CinemachineVirtualCamera vcam;
    private bool applied;
    private bool playerClicked;
    // Start is called before the first frame update
    void Start()
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerClicked = !playerClicked;
        }
        if (playerClicked)
        {
            vcam.Follow = null;
            if (!applied)
            {
                vcam.ForceCameraPosition(vcam.transform.position - new Vector3(0, 2.5f, 0), Quaternion.identity);
                applied = true;
            }
        } else
        {
            vcam.Follow = FollowTarget;
            applied = false;
        }
           
    }
}
