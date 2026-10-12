using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StunDamageEffect", menuName = "Scriptable Objects/Skill Effects/StunDamageEffect")]
public class StunDamageEffect : SkillEffectBase
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

            float conditionMult = 0.25f;
            if (target.GetType() == typeof(Enemy))
            {
                Enemy temp = (Enemy) target;
                if (temp.IsStunned)
                {
                    conditionMult = 2f;
                }
            }

            int baseDamage = (int) Math.Max( Math.Floor( ( user.Attack - ( target.Defense * 0.5 ) ) * target.Affinities[ (int)Element ]), 1 );
            int finalDamage = (int) Math.Max( Math.Floor( baseDamage * user.OutDamageMod * target.IncDamageMod * conditionMult ), 1 );
            target.TakeDamage(finalDamage);
            target.TakeStun( (int) Math.Max( Math.Floor( StunValue * target.Affinities[ (int)Element ] ), 0) );
        }
        return true;
    }
}
