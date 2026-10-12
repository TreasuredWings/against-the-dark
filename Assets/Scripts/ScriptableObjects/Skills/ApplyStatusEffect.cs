using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = "Scriptable Objects/Skill Effects/ApplyStatusEffect")]
public class ApplyStatusEffect : SkillEffectBase
{
    [field: SerializeField] public StatusEffectBase StatusEffect {get; private set;}

    public override bool Use(Unit user, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            target.AddStatus(StatusEffect);
        }
        return true;
    }
}
