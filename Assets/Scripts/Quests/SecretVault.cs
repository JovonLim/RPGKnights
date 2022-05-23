using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVault : MonoBehaviour
{
    [SerializeField] GameObject vaultClosed;
    [SerializeField] GameObject vaultOpened;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerInteraction>().questActive)
        {
            vaultClosed.SetActive(false);
            vaultOpened.SetActive(true);
        }
    }
}
