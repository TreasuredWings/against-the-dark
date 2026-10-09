using System.Collections.Generic;
using UnityEngine;

// Absctract class for a Skill: an object that can be Used to affect targets.
public class Skill 
{
    [field: SerializeField] public SkillData Config { get; private set; }

    public Skill(SkillData data)
    {
        Config = data;
    }

    public bool Use(Unit user, List<Unit> targets)
    {
        foreach (SkillEffectBaseData effect in Config.EffectChain)
        {
            effect?.Use(user, targets);
        }
        return true;
    }
}
