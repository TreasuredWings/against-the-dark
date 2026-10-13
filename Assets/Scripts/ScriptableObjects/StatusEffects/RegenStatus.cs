using UnityEngine;
using System.Collections.Generic;
using System;


[CreateAssetMenu(fileName = "RegenStatus", menuName = "Scriptable Objects/Status Effect/RegenStatus")]
public class RegenStatus : StatusEffectBase
{
    [field: SerializeField] public float HealStrength {get; private set;} = 0.15f;

    public override bool Trigger(Unit afflicted)
    {
        int regen = (int) Math.Max( Math.Floor( afflicted.HPMax * HealStrength ), 1);
        afflicted.TakeHealing(regen);
        return true;
    }
}
