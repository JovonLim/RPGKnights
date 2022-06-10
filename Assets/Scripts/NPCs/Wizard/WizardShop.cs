using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardShop : MonoBehaviour
{
    [SerializeField] Spell[] spells;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] int[] costs;
    [SerializeField] GameObject insufficientFunds;
    private static bool[] purchased = new bool[6];
    private bool update;
    private int selected = -1;
    // Start is called before the first frame update
    void Start()
    {
        update = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (update)
        {
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
        }
    }

    public void Purchase()
    {
        if (!purchased[selected] && UI.coins >= costs[selected])
        {
            UI.coins -= costs[selected];
            purchased[selected] = true;
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
    }
}
