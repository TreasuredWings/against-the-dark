using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Ally : Unit
{

    [Header("Ally Parameters")]
    [field: SerializeField] public AllyClass Class {get; private set;}
    [field: SerializeField] public bool Taunting {get; set;} = false;

    public void AssignClass(AllyClass newClass)
    {
        Class = newClass;
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

    public void InitializeData(string newName, AllyClass newClass, Sprite newSprite, RuntimeAnimatorController newOverride, Vector3 newHome, int newOrder)
    {
        Name = newName;
        AssignClass(newClass);
        UnitSprite.sprite = newSprite;
        UnitAnim.Anim.runtimeAnimatorController = newOverride;
        UnitAnim.HomePoint = newHome;
        UnitSprite.sortingOrder = newOrder;
    }

    public IEnumerator TakeTurn(Skill chosen, List<Unit> targets)
    {
        Debug.Log(Name + " uses " + chosen.Name);
        yield return StartCoroutine(UseSkill(chosen, targets));
        StartCoroutine(BattleManager.Instance.InputComplete());
    }

    public override int TakeDamage(int incomingDamage)
    {
        UIManager.Instance.CreateEffectText(transform.position, incomingDamage.ToString(), Colors.White);
        int newHP = base.TakeDamage(incomingDamage);
        UIManager.Instance.UpdateAllStatusWindows();
        return newHP;
    }

    public override int TakeHealing(int incomingHealing)
    {
        UIManager.Instance.CreateEffectText(transform.position, incomingHealing.ToString(), Colors.HP);
        int newHP = base.TakeHealing(incomingHealing);
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
