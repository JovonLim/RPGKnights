using UnityEngine;

public class Necromancer : RangeEnemy
{
    [SerializeField] private GameObject dissappearingGround;
    [SerializeField] private float castCooldown;
    [SerializeField] private float spellDuration;
    private float spellActiveDuration = 0;

    private static bool casting = false;
    private float castCooldownTimer = float.MaxValue;

    public override void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    void Update()
    {
        if (casting)
        {
            castCooldownTimer = 0;
            spellActiveDuration += Time.deltaTime;
            if (spellActiveDuration > spellDuration)
            {
                FinishSpell();
            }
        }
        else
        {
            castCooldownTimer += Time.deltaTime;

            if (PlayerInSight())
            {
                if (castCooldownTimer > castCooldown)
                {
                    anima.SetTrigger("cast");
                }
            }
        }
    }

    private void CastSpell()
    {
        casting = true;
        dissappearingGround.SetActive(false);
    }

    private void FinishSpell()
    {
        casting = false;
        spellActiveDuration = 0;
        dissappearingGround.SetActive(true);
    }
}
