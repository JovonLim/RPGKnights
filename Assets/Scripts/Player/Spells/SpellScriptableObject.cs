using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellScriptableObject : ScriptableObject
{
    public float cooldownTime;
    public float speed;
    public float damage;
    public float activeTime;

    public virtual void Activate(GameObject parent)
    {

    }
}
