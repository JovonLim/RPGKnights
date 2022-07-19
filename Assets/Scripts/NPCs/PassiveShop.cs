using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassiveShop : MonoBehaviour, IDataPersistence
{
    [SerializeField] Passives[] passives;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] GameObject insufficientFunds;
    [SerializeField] TextMeshProUGUI coinAmt;
    [SerializeField] TextMeshProUGUI description;
    private bool[] purchased = new bool[2];
    private bool update;
    private int selected = -1;

    private enum NPCType
    {
        archer, knight
    }

    [SerializeField] private NPCType npc;
    // Start is called before the first frame update
    void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);
        update = true;
        coinAmt.text = UI.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (update)
        {
            coinAmt.text = UI.coins.ToString();
            for (int i = 0; i < purchased.Length; i++)
            {
                if (purchased[i])
                {
                    texts[i].text = passives[i].passiveName + " (purchased)";
                }
                else
                {
                    texts[i].text = passives[i].passiveName;
                }
            }
            update = false;
        }
    }

    public void Select(int num)
    {
        Deselect();
        texts[num].color = Color.red;
        selected = num;
        ShowDescription(num);
    }

    public void Deselect()
    {
        if (selected != -1)
        {
            texts[selected].color = Color.black;
            RemoveDescription();
            selected = -1;
        }
    }

    public void Purchase()
    {
        if (selected < 0)
        {
            return;
        }
        if (!purchased[selected] && UI.coins >= passives[selected].cost)
        {
            UI.coins -= passives[selected].cost;
            purchased[selected] = true;
            SaveData(DataPersistenceManager.instance.gameData);
            update = true;
            if (npc == NPCType.archer)
            {
                PlayerAttack.rangedPassives[selected] = true;
            } else
            {
                PlayerAttack.meleePassives[selected] = true;
            }
            Deselect();
        }
        else if (UI.coins < passives[selected].cost)
        {
            Deselect();
            insufficientFunds.SetActive(true);
            gameObject.SetActive(false);

        }
    }

    private void ShowDescription(int num)
    {
        description.text = passives[num].description + "\nCost: " + passives[num].cost;
    }

    private void RemoveDescription()
    {
        description.text = "";
    }

    public void Exit()
    {
        insufficientFunds.SetActive(false);
        gameObject.SetActive(true);
    }

    public void LoadData(GameData data)
    {
        if (npc == NPCType.archer)
        {
            purchased = data.archerPurchased;
        } else
        {
            purchased = data.knightPurchased;
        }
     
    }

    public void SaveData(GameData data)
    {
        if (npc == NPCType.archer)
        {
            data.archerPurchased = purchased;
        }
        else
        {
            data.knightPurchased = purchased;
        }
    }
}

