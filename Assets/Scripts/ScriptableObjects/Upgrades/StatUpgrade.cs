using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatUpgrade", menuName = "Scriptable Objects/Upgrades/StatUpgrade")]
public class StatUpgrade : Upgrade
{
    [field: SerializeField] public List<Stat> StatUps {get; private set;} = new List<Stat>();
    [field: SerializeField] public List<int> StatUpValues {get; private set;} = new List<int>();

    public override bool Application(Unit target)
    {
        for (int i = 0; i < StatUps.Count; i++)
        {
            switch (StatUps[i])
            {
                case Stat.HPMax:
                    target.HPMax += StatUpValues[i];
                    target.TakeHealing(StatUpValues[i]);
                    break;
                case Stat.MPMax:
                    target.MPMax += StatUpValues[i];
                    target.GainMP(StatUpValues[i]);
                    break;
                case Stat.Attack:
                    target.Attack += StatUpValues[i];
                    break;
                case Stat.Defense:
                    target.Defense += StatUpValues[i];
                    break;
                case Stat.Magic:
                    target.Magic += StatUpValues[i];
                    break;
                case Stat.Resist:
                    target.Resist += StatUpValues[i];
                    break;
                case Stat.Speed:
                    target.Speed += StatUpValues[i];
                    break;
                
            }
        }
        return true;
    }
}
