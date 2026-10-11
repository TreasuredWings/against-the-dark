using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllyClass", menuName = "Scriptable Objects/AllyClass")]
public class AllyClass : ScriptableObject
{
    [field: SerializeField] public string Name {get; private set;} = "Default Class";
    [field: SerializeField] public ClassType Type {get; private set;} = ClassType.None;
    [field: SerializeField] public int HPMax {get; private set;} = 100;
    [field: SerializeField] public int MPMax {get; private set;} = 10;
    [field: SerializeField] public int Attack {get; private set;} = 10;
    [field: SerializeField] public int Defense {get; private set;} = 10;
    [field: SerializeField] public int Magic {get; private set;} = 10;
    [field: SerializeField] public int Resist {get; private set;} = 10;
    [field: SerializeField] public int Speed {get; private set;} = 10;
    [field: SerializeField] public List<float> Affinities { get; private set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public Skill ClassSkill { get; private set; }
}
