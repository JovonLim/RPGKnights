using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    static UI instance;
    [SerializeField]
    private TextMeshProUGUI coinAmt;
    public static int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        coinAmt.text = "x:" + coins;
    }

    // Update is called once per frame
    void Update()
    {
        
            coinAmt.text = "x:" + coins;
      

    }
}
