using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public string Name {get; private set;} = "Default Unit";
    [field: SerializeField] public Sprite EnemySprite {get; private set;}
    [field: SerializeField] public RuntimeAnimatorController EnemySpriteOverride {get; private set;}
    [field: SerializeField] public int HPMax {get; private set;} = 100;
    [field: SerializeField] public int MPMax {get; private set;} = 10;
    [field: SerializeField] public int Attack {get; private set;} = 10;
    [field: SerializeField] public int Defense {get; private set;} = 10;
    [field: SerializeField] public int Magic {get; private set;} = 10;
    [field: SerializeField] public int Resist {get; private set;} = 10;
    [field: SerializeField] public int Speed {get; private set;} = 10;
    [field: SerializeField] public List<float> Affinities { get; private set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public List<Skill> SkillList { get; private set; } = new List<Skill>();
    [field: SerializeField] public EnemyAIType AIType {get; private set;} = EnemyAIType.Static;
    [field: SerializeField] public List<float> SkillChance { get; private set; } = new List<float>();
}
