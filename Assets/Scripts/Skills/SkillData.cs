using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Base Skill Info")]
    [field: SerializeField] public string Name { get; private set; } = "Skill Name";
    [field: SerializeField] public string Description { get; private set; } = "This is a skill description.";
    // Skill icon sprite?
    
    [field: SerializeField] public SkillType Type { get; private set; } = SkillType.Basic;
    [field: SerializeField] public ClassType AssociatedClass { get; private set; } = ClassType.None;
    [field: SerializeField] public TargetType Target { get; private set; } = TargetType.Self;

    [field: SerializeField] public int MPCost { get; private set; } = 0;
    [field: SerializeField] public List<SkillEffectBaseData> EffectChain { get; private set; } = new List<SkillEffectBaseData>();
}
