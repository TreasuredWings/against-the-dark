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
    [field: SerializeField] public string Name {get; private set;} = "Default Unit";
    [field: SerializeField] public int HPMax {get; private set;} = 100;
    [field: SerializeField] public int MPMax {get; private set;} = 10;
    [field: SerializeField] public int Attack {get; private set;} = 10;
    [field: SerializeField] public int Defense {get; private set;} = 10;
    [field: SerializeField] public int Magic {get; private set;} = 10;
    [field: SerializeField] public int Resist {get; private set;} = 10;
    [field: SerializeField] public int Speed {get; private set;} = 10;

    // Dynamic stats
    [field: SerializeField] public int HP {get; private set;} = 100;
    [field: SerializeField] public int MP {get; private set;} = 10;

    [field: SerializeField] public float OutDamageMod {get; private set;} = 1;
    [field: SerializeField] public float IncDamageMod {get; private set;} = 1;
    [field: SerializeField] public float SpeedMod {get; private set;} = 1;

    [field: SerializeField] public bool IsKnockedOut {get; private set;} = false;

    [field: SerializeField] public List<float> Affinities { get; private set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public List<StatusEffectBase> AppliedStatuses { get; private set; } = new List<StatusEffectBase>();
    [field: SerializeField] public List<SkillData> StartingSkills { get; private set; } = new List<SkillData>();

    // The real skill list is hidden from the editor
    private List<Skill> _skillList = new List<Skill>();
    public List<Skill> SkillList {get => _skillList; private set => _skillList = value;}

    // *** Functions ***

    // UseSkill: Attempt to use specified Skill on targets.
    // Return: True if skill could be executed, False if requirements were not met/skill was not executed.
    public bool UseSkill(Skill chosen, List<Unit> targets)
    {
        int mpCost = chosen.Config.MPCost;
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
        return HP;
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
                AppliedStatuses.RemoveAt(i);
            }
        }
    }

    public virtual void AddStatus(StatusEffectBase status)
    {
        AppliedStatuses.Add(status);
    }

    public virtual void AddSkill(Skill newSkill)
    {
        SkillList.Add(newSkill);
    }

    // *** Unity Functions ***

    void Awake()
    {
        foreach (SkillData data in StartingSkills)
        {
            SkillList.Add(new Skill(data));
        }
    }

}
