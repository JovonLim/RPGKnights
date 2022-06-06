
using UnityEngine;
using UnityEngine.UI;

public class SpellHud : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] Image ability;
    [SerializeField] Image abilityDark;
    private float cooldown;
    private bool OnCooldown = true;
    // Start is called before the first frame update


    private void Start()
    {       
        InitializeSpell(); 
    }
    // Update is called once per frame
    void Update()
    {
        UseAbility();
    }

    private void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q) && OnCooldown == false)
        {
            OnCooldown = true;
            abilityDark.fillAmount = 1;
        }
        if (OnCooldown)
        {
            abilityDark.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (abilityDark.fillAmount <= 0)
            {
                abilityDark.fillAmount = 0;
                OnCooldown = false;
            }
        }
    }

    void InitializeSpell()
    {
        Spell spell = player.GetComponent<PlayerAttack>().spellToCast;
        cooldown = spell.spell.cooldownTime;
        ability.sprite = spell.GetComponent<SpriteRenderer>().sprite;
        abilityDark.sprite = spell.GetComponent<SpriteRenderer>().sprite;
        abilityDark.type = Image.Type.Filled;
        abilityDark.fillClockwise = false;
        abilityDark.fillOrigin = (int) Image.Origin360.Top;
    }
}
