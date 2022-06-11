using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject button;

    void Start()
    {
        text.SetActive(true);
        button.SetActive(true);
    }
}
