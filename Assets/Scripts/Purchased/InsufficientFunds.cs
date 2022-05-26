using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsufficientFunds : MonoBehaviour
{
    [SerializeField] private GameObject insufficientFunds;
    public void close()
    {
        insufficientFunds.SetActive(false);
    }
}

