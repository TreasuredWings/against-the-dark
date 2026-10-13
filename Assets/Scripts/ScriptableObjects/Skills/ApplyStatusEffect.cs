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
            if (target.IsKnockedOut)
            {
                continue;
            }
            
            target.AddStatus(StatusEffect);
        }
        int sfx = 6;
        if (StatusEffect.Type == StatusType.Positive)
        {
            sfx = 5;
        }
        SFXManager.Instance.PlaySFX(sfx, Vector3.zero, 1);
        return true;
    }
}
