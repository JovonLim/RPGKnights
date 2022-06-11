using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float startingHealth;
    // Ensures that health can only be get from this script from anywhere but not allowed to be modified
    public float currentHealth { get; protected set; }
    protected Animator anima;
    protected bool isDead;

    [SerializeField] protected float PhysicalDefense = 0;
    [SerializeField] protected float MagicDefense = 0;
    [SerializeField] protected float minDmg;
    [SerializeField] protected GameObject prefab;

    public abstract void TakePhysicalDamage(float damage);
    public abstract void TakeMagicDamage(float damage);
    public abstract void TakeTrueDamage(float damage);

    public abstract void ApplyDmg(float damage);

    public abstract void OnDeath();

    public abstract void GainHealth(float damage);

    public abstract bool IsDefeated();

    // Gain permanent health
    public abstract void AddHealth(float amt);
    public abstract void AddPhysicalDefense(float amt);
    public abstract void AddMagicDefense(float amt);

    public abstract void AddDefense(float amt);

    public abstract void SubtractHealth(float amt);

    public abstract void SubtractPhysicalDefense(float amt);

    public abstract void SubtractMagicDefense(float amt);

    public abstract void SubtractDefense(float amt);


}
