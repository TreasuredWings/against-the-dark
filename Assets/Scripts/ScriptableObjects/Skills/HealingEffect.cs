using System;
using System.Collections.Generic;
using UnityEngine;

public enum HealType { Stat, Percent }

[CreateAssetMenu(fileName = "HealingEffect", menuName = "Scriptable Objects/Skill Effects/HealingEffect")]
public class HealingEffect : SkillEffectBase
{
    [field: SerializeField] public HealType Type {get; private set;} = HealType.Stat;
    [field: SerializeField] public float HealStrength {get; private set;} = 0.1f;

    public override bool Use(Unit user, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {   
            if (target.IsKnockedOut)
            {
                continue;
            }

            int statBoost = 0;
            if (Type == HealType.Stat)
            {
                statBoost = user.Resist;
            }
            float variance = (float) (UnityEngine.Random.value * 0.2 + 0.90);
            int totalHealing = (int) Math.Max( Math.Floor( (target.HPMax * HealStrength + user.Resist) * variance ), 1);
            target.TakeHealing(totalHealing);
        }
        SFXManager.Instance.PlaySFX(4, Vector3.zero, 1);
        return true;
    }
}
