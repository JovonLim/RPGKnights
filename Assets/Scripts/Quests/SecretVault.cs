using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVault : MonoBehaviour
{
    [SerializeField] GameObject vaultClosed;
    [SerializeField] GameObject vaultOpened;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteraction.instance.questActive)
        {
            vaultClosed.SetActive(false);
            vaultOpened.SetActive(true);
        }
    }
}
