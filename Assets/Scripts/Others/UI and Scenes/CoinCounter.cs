using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour, IDataPersistence
{
    public static CoinCounter instance;
    [SerializeField]
    private TextMeshProUGUI coinAmt;
    public static int coins = 0; 
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        if (coinAmt != null)
        {
            coinAmt.text = coins.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (coinAmt != null)
        {
            coinAmt.text = coins.ToString();
        }
      
    }

    public void LoadData(GameData data)
    {
        CoinCounter.coins = data.coins;
    }

    public void SaveData(GameData data)
    {
        data.coins = CoinCounter.coins;
    }
}
