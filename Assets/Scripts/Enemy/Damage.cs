using UnityEngine;

public class Damage : MonoBehaviour
{
    protected enum Dmg
    {
        magic, physical, trueDmg,
    }

    [SerializeField] protected Dmg damageType;
}
