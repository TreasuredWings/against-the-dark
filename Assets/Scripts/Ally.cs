using UnityEngine;
using System.Collections.Generic;

public class Ally : Unit
{

    [Header("Ally Parameters")]
    [field: SerializeField] public AllyClass Class {get; private set;}

    public void AssignClass(AllyClass newClass)
    {
        Class = newClass;
        InitializeData();
    }

    public void InitializeData()
    {
        HPMax = Class.HPMax;
        HP = HPMax;
        MPMax = Class.MPMax;
        MP = MPMax;
        Attack = Class.Attack;
        Defense = Class.Defense;
        Magic = Class.Magic;
        Resist = Class.Resist;
        Speed = Class.Speed;
        Affinities = new List<float>(Class.Affinities);
        AddSkill(Class.ClassSkill);
    }

    public override int TakeDamage(int incomingDamage)
    {
        int newHP = base.TakeDamage(incomingDamage);
        UIManager.Instance.UpdateAllStatusWindows();
        return newHP;
    }

    public override bool SpendMP(int cost)
    {
        bool result = base.SpendMP(cost);
        UIManager.Instance.UpdateAllStatusWindows();
        return result;
    }
}
