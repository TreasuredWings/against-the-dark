using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DamageData", menuName = "Scriptable Objects/Skill Effects/DamageData")]
public class DamageData : SkillEffectBase
{
    [field: SerializeField] public DamageType Element { get; private set; } = DamageType.Physical;
    [field: SerializeField] public bool Pierce { get; private set; } = false;
    [field: SerializeField] public int FlatDamage { get; private set; } = 1;
    [field: SerializeField] public int StunValue { get; private set; } = 25;

    public override bool Use(Unit user, List<Unit> targets)
    {
        bool DidSomeoneKO = false;
        int pierceValue = 1;
        if (Pierce)
        {
            pierceValue = 0;
        }

        foreach (Unit target in targets)
        {
            if (target.IsKnockedOut)
            {
                continue;
            }
            
            int baseDamage;
            if (Element == DamageType.Physical)
            {
                baseDamage = (int) Math.Max( Math.Floor( ( user.Attack - ( target.Defense * 0.5 * pierceValue ) ) * target.Affinities[0]), 1 );
            }
            else if (Element == DamageType.Flat)
            {
                baseDamage = FlatDamage;
            }
            else
            {
                baseDamage = (int) Math.Max( Math.Floor( ( user.Magic - ( target.Resist * 0.5 * pierceValue ) ) * target.Affinities[ (int)Element ]), 1 );
            }
            float variance = (float) (UnityEngine.Random.value * 0.2 + 0.90);
            int finalDamage = (int) Math.Max( Math.Floor( baseDamage * user.OutDamageMod * target.IncDamageMod * variance ), 1 );
            target.TakeDamage(finalDamage);
            target.TakeStun( (int) Math.Max( Math.Floor( StunValue * target.Affinities[ (int)Element ] ), 0) );
            if (target.IsKnockedOut)
            {
                DidSomeoneKO = true;
            }
        }
        int sfx = 3;
        if (DidSomeoneKO)
        {
            sfx = 8;
        }
        SFXManager.Instance.PlaySFX(sfx, Vector3.zero, 1);
        return true;
    }
}
