using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellTree : MonoBehaviour
{
    [SerializeField] private Image[] icons;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private TextMeshProUGUI spellName;
    [SerializeField] private TextMeshProUGUI description;
    private Spell[] spells;
    

    // Start is called before the first frame update
    void Start()
    {
        spells = player.GetComponent<SpellHolder>().spells;
        
        for (int i = 0; i < spells.Length; i++)
        {
            icons[i].sprite = spells[i].GetComponent<SpriteRenderer>().sprite;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string SkillDescription(int num) 
    {
        Spell spellParent = spells[num];
        string description = "";
        description += spellParent.spell.description + "\nStats:\nDamage: " + spellParent.spell.damage + 
            "\nActive: " + spellParent.spell.activeTime + "\nCooldown: " + spellParent.spell.cooldownTime
            + "\nSpeed: " + spellParent.spell.speed + "\nPrerequisite: " + spellParent.spell.prerequisite;
        return description;
    }
    public void ShowDescription(int num)
    {
        descriptionBox.SetActive(true);      
        
        if (!player.GetComponent<SpellHolder>().IsUnlocked(num))
        {
            spellName.color = Color.red;
            spellName.text = spells[num].spell.spellName + " (locked)";
        } else
        {
            spellName.text = spells[num].spell.spellName;
        }
        description.text = SkillDescription(num);
    }

    public void CloseDescription()
    {
        descriptionBox.SetActive(false);
    }
}
