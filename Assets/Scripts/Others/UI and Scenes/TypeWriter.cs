using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private string fullText;

    private void Start()
    {
        StartCoroutine(StartText());
    }

    IEnumerator StartText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            GetComponent<TextMeshProUGUI>().text = fullText.Substring(0, i);
            yield return new WaitForSecondsRealtime(delay);
        }
    }
}
