using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;



[CreateAssetMenu(fileName = "DamageData", menuName = "Skill Effects/DamageData")]
public class DamageData : SkillEffectBaseData
{
    [field: SerializeField] public DamageType Element { get; private set; } = DamageType.Physical;
    [field: SerializeField] public bool Pierce { get; private set; } = false;
    [field: SerializeField] public int FlatDamage { get; private set; } = 1;

    public override bool Use(Unit user, List<Unit> targets)
    {
        int pierceValue = 0;
        if (Pierce)
        {
            pierceValue = 1;
        }

        foreach (Unit target in targets)
        {
            if (Element == DamageType.Physical)
            {
                int damage = (int) Math.Floor( Math.Max( user.Attack - ( target.Defense * 0.5 * pierceValue ), 1 ) );
                target.TakeDamage(damage);
            }
            else if (Element == DamageType.Flat)
            {
                target.TakeDamage(FlatDamage);
            }
            else
            {
                int damage = (int) Math.Floor( Math.Max( ( user.Magic - ( target.Resist * 0.5 * pierceValue ) ) * target.Affinities[(int)Element], 1 ) );
                target.TakeDamage(damage);
            }
        }

        return true;
    }
}
