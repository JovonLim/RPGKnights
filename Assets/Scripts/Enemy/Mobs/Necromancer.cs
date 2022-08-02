using UnityEngine;

public class Necromancer : RangeEnemy
{
    [SerializeField] private GameObject disappearingGround;
    [SerializeField] private float castCooldown;
    [SerializeField] private float spellDuration;
    private float spellActiveDuration = 0;

    private static bool casting = false;
    private float castCooldownTimer = float.MaxValue;


    // Update is called once per frame
    protected override void Update()
    {
        collisionTimer += Time.deltaTime;
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
        disappearingGround.SetActive(false);
    }

    private void FinishSpell()
    {
        casting = false;
        spellActiveDuration = 0;
        disappearingGround.SetActive(true);
    }
}
