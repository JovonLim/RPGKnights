
using UnityEngine;
using UnityEngine.UI;

public class SpellHud : SpellHolder
{
    [SerializeField] Image ability;
    [SerializeField] Image abilityDark;
    private float cooldown;
    private bool OnCooldown = false;
    // Start is called before the first frame update


    protected void Start()
    {
        InitializeSpell();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UseAbility();
        if (updated)
        {
            InitializeSpell();
            updated = false;
        }
        
    }

    private void SetCooldown()
    {
        OnCooldown = true;
    }
    private void UseAbility()
    {
        if (OnCooldown)
        {
            abilityDark.fillAmount = 1;
            OnCooldown = false;
        }
        abilityDark.fillAmount -= 1 / cooldown * Time.deltaTime;
        if (abilityDark.fillAmount <= 0)
        {
            abilityDark.fillAmount = 0;
        }
    }

    public void InitializeSpell()
    {
        Spell spell = PlayerAttack.spellToCast;
        ability.GetComponent<Image>().enabled = true;
        abilityDark.GetComponent<Image>().enabled = true;
        if (spell != null)
        {
            cooldown = spell.spell.cooldownTime;
            ability.sprite = spell.GetComponent<SpriteRenderer>().sprite;
            abilityDark.sprite = spell.GetComponent<SpriteRenderer>().sprite;
            abilityDark.type = Image.Type.Filled;
            abilityDark.fillClockwise = false;
            abilityDark.fillOrigin = (int)Image.Origin360.Top;
            abilityDark.fillAmount = 0;
        } else
        {
            ability.GetComponent<Image>().enabled = false;
            abilityDark.GetComponent<Image>().enabled = false;
        }
        
    }
}
