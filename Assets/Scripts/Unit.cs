using System.Runtime;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal.Internal;

// Base class for all Unit-type classes.
public class Unit : MonoBehaviour
{
    // *** Properties ***


    // Dynamic stats
    [Header("Unit Parameters")]
    [field: SerializeField] public SpriteRenderer UnitSprite {get; set;}
    [field: SerializeField] public string Name {get; set;} = "Default Unit";
    [field: SerializeField] public int HPMax {get; protected set;} = 100;
    [field: SerializeField] public int MPMax {get; protected set;} = 10;
    [field: SerializeField] public int Attack {get; protected set;} = 10;
    [field: SerializeField] public int Defense {get; protected set;} = 10;
    [field: SerializeField] public int Magic {get; protected set;} = 10;
    [field: SerializeField] public int Resist {get; protected set;} = 10;
    [field: SerializeField] public int Speed {get; protected set;} = 10;

    // Dynamic stats
    [field: SerializeField] public int HP {get; protected set;} = 100;
    [field: SerializeField] public int MP {get; protected set;} = 10;

    [field: SerializeField] public float OutDamageMod {get; protected set;} = 1;
    [field: SerializeField] public float IncDamageMod {get; protected set;} = 1;
    [field: SerializeField] public float SpeedMod {get; protected set;} = 1;
    [field: SerializeField] public float SpeedVariance {get; protected set;} = 1;

    [field: SerializeField] public bool IsKnockedOut {get; protected set;} = false;

    [field: SerializeField] public List<float> Affinities { get; protected set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public List<StatusEffectBase> AppliedStatuses { get; protected set; } = new List<StatusEffectBase>();
    [field: SerializeField] public List<Skill> SkillList { get; protected set; } = new List<Skill>();

    // *** Functions ***

    // UseSkill: Attempt to use specified Skill on targets.
    // Return: True if skill could be executed, False if requirements were not met/skill was not executed.
    public bool UseSkill(Skill chosen, List<Unit> targets)
    {
        int mpCost = chosen.MPCost;
        if (!SpendMP(mpCost))
        {
            // Could not spend enough MP, cancel skill.
            return false;
        }
        if (!chosen.Use(this, targets))
        {
            // Could not apply skill fully, cancel skill.
            return false;
        }
        return true;
    }

    // Recieve damage, capped at this unit's current hp
    // Returns: this unit's new HP
    public virtual int TakeDamage(int incomingDamage)
    {
        if (incomingDamage > HP)
        {
            HP = 0;
        }
        else
        {
            HP -= incomingDamage;
        }
        if (HP <= 0)
        {
            Knockout();
        }
        return HP;
    }

    public virtual void Knockout()
    {
        IsKnockedOut = true;
        for (int i = AppliedStatuses.Count - 1; i >= 0; i--)
        {
            AppliedStatuses[i].Removal(this);
            AppliedStatuses.RemoveAt(i);
        }
    }

    public virtual int TakeStun(int incomingStun)
    {
        return 0;
    }

    // Attempt to spend the specified ammount of MP
    // Returns: true if it was able to spend, false if it could not
    public virtual bool SpendMP(int cost)
    {
        if (cost <= MP)
        {
            MP -= cost;
            return true;
        }
        return false;
    }

    public virtual void CheckStatuses(StatusTiming filter)
    {
        foreach (StatusEffectBase status in AppliedStatuses)
        {
            if (status.Timing == filter)
            {
                status.Trigger(this);
            }
        }
        for (int i = AppliedStatuses.Count - 1; i >= 0; i--)
        {
            StatusEffectBase status = AppliedStatuses[i];
            if (status.Duration <= 0)
            {
                status.Removal(this);
                AppliedStatuses.RemoveAt(i);
            }
        }
    }

    public virtual void AddStatus(StatusEffectBase status)
    {
        AppliedStatuses.Add(status);
        status.Application(this);
    }

    public virtual void AddSkill(Skill newSkill)
    {
        SkillList.Add(newSkill);
    }

    public void RollSpeed()
    {
        SpeedVariance = (float) (UnityEngine.Random.value * 0.4 + 0.8);
    }

    // *** Unity Functions ***

}
