using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Troop", menuName = "Scriptable Objects/Troop")]
public class Troop : ScriptableObject
{
    [field: SerializeField] public List<EnemyData> EnemyList { get; private set; } = new List<EnemyData>();
}
