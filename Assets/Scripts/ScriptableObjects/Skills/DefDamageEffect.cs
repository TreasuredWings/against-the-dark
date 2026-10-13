using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DefDamageEffect", menuName = "Scriptable Objects/Skill Effects/DefDamageEffect")]
public class DefDamageEffect : SkillEffectBase
{
    [field: SerializeField] public DamageType Element { get; private set; } = DamageType.Physical;
    [field: SerializeField] public int StunValue { get; private set; } = 25;

    public override bool Use(Unit user, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            if (target.IsKnockedOut)
            {
                continue;
            }

            int baseDamage = (int) Math.Max( Math.Floor( ( user.Defense - ( target.Defense * 0.5 ) ) * target.Affinities[ (int)Element ]), 1 );
            float variance = (float) (UnityEngine.Random.value * 0.1 + 0.95);
            int finalDamage = (int) Math.Max( Math.Floor( baseDamage * user.OutDamageMod * target.IncDamageMod * variance ), 1 );
            target.TakeDamage(finalDamage);
            target.TakeStun( (int) Math.Max( Math.Floor( StunValue * target.Affinities[ (int)Element ] ), 0) );
        }
        SFXManager.Instance.PlaySFX(3, Vector3.zero, 1);
        return true;
    }
}
