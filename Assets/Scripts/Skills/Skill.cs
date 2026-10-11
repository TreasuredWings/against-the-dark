using System.Collections.Generic;
using UnityEngine;

// Skill: an object that can be Used to affect targets.
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    [Header("Base Skill Info")]
    [field: SerializeField] public string Name { get; private set; } = "Skill Name";
    [field: SerializeField] public string Description { get; private set; } = "This is a skill description.";
    // Skill icon sprite?
    
    [field: SerializeField] public SkillType Type { get; private set; } = SkillType.Basic;
    [field: SerializeField] public ClassType AssociatedClass { get; private set; } = ClassType.None;
    [field: SerializeField] public TargetType Target { get; private set; } = TargetType.Self;

    [field: SerializeField] public int MPCost { get; private set; } = 0;
    [field: SerializeField] public List<SkillEffectBase> EffectChain { get; private set; } = new List<SkillEffectBase>();

    public bool Use(Unit user, List<Unit> targets)
    {
        foreach (SkillEffectBase effect in EffectChain)
        {
            effect?.Use(user, targets);
        }
        return true;
    }
}
