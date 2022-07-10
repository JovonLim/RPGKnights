using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellTree : SpellHolder
{
    [SerializeField] private Image[] icons;
    [SerializeField] private Image[] activeSkills;
    [SerializeField] private GameObject activeSkillHolders;
    [SerializeField] private GameObject[] skillPanel;
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private GameObject skillBox;
    [SerializeField] private TMP_InputField slot;
    [SerializeField] private TextMeshProUGUI spellName;
    [SerializeField] private TextMeshProUGUI description;
    private static int currentPage = 0;
    private static int selectedSkill;
    private bool changed = true;
    private bool display = false;
    

    // Start is called before the first frame update
    protected void Start()
    {
        skillPanel[currentPage].SetActive(display);
        activeSkillHolders.SetActive(display);
        for (int i = 0; i < spells.Length; i++)
        {
            icons[i].sprite = spells[i].GetComponent<SpriteRenderer>().sprite;
        }

        UpdateSkills();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
        {
            TogglePanel();
        }
        UpdateSkills();
    }

    void TogglePanel()
    {
        display = !display;
        activeSkillHolders.SetActive(display);
        skillPanel[currentPage].SetActive(display);
        GetComponent<PlayerAttack>().enabled = !display;
        if (display)
        {
            Time.timeScale = 0;
            
        } else
        {
            skillBox.SetActive(false);
            descriptionBox.SetActive(false);
            Time.timeScale = 1;
        }
    }
    void UpdateSkills()
    {
        if (changed)
        {
            for (int i = 0; i < activeSpells.Length; i++)
            {
                activeSkills[i].GetComponent<Image>().enabled = true;
                if (activeSpells[i] != null)
                {
                    activeSkills[i].sprite = activeSpells[i].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    activeSkills[i].GetComponent<Image>().enabled = false;
                }
            }
            changed = false;
        }
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
        if (!skillBox.activeInHierarchy)
        {
            descriptionBox.SetActive(true);
        }
             
        
        if (!IsUnlocked(num))
        {
            spellName.color = Color.red;
            spellName.text = spells[num].spell.spellName + " (locked)";
        } else
        {
            spellName.color = Color.white;
            spellName.text = spells[num].spell.spellName;
        }
        description.text = SkillDescription(num);
    }

    public void CloseDescription()
    {
        descriptionBox.SetActive(false);
    }

    public void ShowSkillSelection(int num)
    {
        
        selectedSkill = num;
        if (IsUnlocked(selectedSkill))
        {
            descriptionBox.SetActive(false);
            skillBox.SetActive(true);           
        } else
        {
            if (skillBox.activeInHierarchy)
            {
                skillBox.SetActive(false);
            }
        }
    }

    public void CloseSkillSelection()
    {
        skillBox.SetActive(false);
        selectedSkill = -1;
    }

    public void AddSkill()
    {
        if (SetActiveSpell(selectedSkill))
        {
            changed = true;
        }
    }

    public void RemoveSkill()
    {
        if (RemoveActiveSpell(selectedSkill))
        {
            changed = true;
        }
    }

    public void NextPage()
    {
        skillPanel[currentPage].SetActive(false);
        currentPage = 1;
        skillPanel[currentPage].SetActive(true);     
    }
    public void PrevPage()
    {
        skillPanel[currentPage].SetActive(false);
        currentPage = 0;
        skillPanel[currentPage].SetActive(true);     
    }
}
