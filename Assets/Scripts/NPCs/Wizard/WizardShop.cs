using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardShop : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject[] panels;
    [SerializeField] Spell[] spells;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] int[] costs;
    [SerializeField] GameObject insufficientFunds;
    [SerializeField] TextMeshProUGUI coinAmt;
    private static bool[] purchased = new bool[14];
    private bool update;
    private int selected = -1;
    private int currentPage = 0;
    void Awake()
    {
        LoadData(DataPersistenceManager.instance.gameData);
        update = true;
        panels[currentPage].SetActive(true);
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
                    texts[i].text = spells[i].spell.spellName + " (purchased)";
                } else
                {
                    texts[i].text = spells[i].spell.spellName + " Cost: " + costs[i];
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
    }

    public void Deselect()
    {
        if (selected != -1)
        {
            texts[selected].color = Color.black;
            selected = -1;
        }
    }

    public void Purchase()
    {
        if (selected < 0)
        {
            return;
        }
        if (!purchased[selected] && UI.coins >= costs[selected])
        {
            UI.coins -= costs[selected];
            purchased[selected] = true;
            SaveData(DataPersistenceManager.instance.gameData);
            update = true;
            SpellHolder.UnlockSpell(spells[selected].spell.id);
            Deselect();
        } else if (UI.coins < costs[selected])
        {
            Deselect();
            insufficientFunds.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    public void Exit()
    {
        insufficientFunds.SetActive(false);
        gameObject.SetActive(true);
    }

    public void LoadData(GameData data)
    {
        purchased = data.wizardPurchased;
    }

    public void SaveData(GameData data)
    {
        data.wizardPurchased = purchased;
    }

    public void NextPage()
    {
        panels[currentPage].SetActive(false);
        currentPage++;
        panels[currentPage].SetActive(true);
        update = true;
    }

    public void PrevPage()
    {
        panels[currentPage].SetActive(false);
        currentPage--;
        panels[currentPage].SetActive(true);
        update = true;
    }
}
