using System.Runtime;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;

public enum UnitStats { Level, HPMax, MPMax, Attack, Defense, Magic, Resist, Speed }

// Base class for all Unit-type classes.
public class Unit : MonoBehaviour
{
    // *** Properties ***

    // Base stats
    [Header("Unit Parameters")]
    [field: SerializeField] public string Name {get; private set;} = "Default Unit";
    [field: SerializeField] public int Level {get; private set;} = 1;
    [field: SerializeField] public int HPMax {get; private set;} = 100;
    [field: SerializeField] public int MPMax {get; private set;} = 10;
    [field: SerializeField] public int Attack {get; private set;} = 10;
    [field: SerializeField] public int Defense {get; private set;} = 10;
    [field: SerializeField] public int Magic {get; private set;} = 10;
    [field: SerializeField] public int Resist {get; private set;} = 10;
    [field: SerializeField] public int Speed {get; private set;} = 10;

    // Dynamic stats
    [field: SerializeField] public int HP {get; private set;}
    [field: SerializeField] public int MP {get; private set;}

    [field: SerializeField] public bool IsKnockedOut {get; private set;} = false;

    [field: SerializeField] public List<float> Affinities { get; private set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public List<SkillData> StartingSkills { get; private set; } = new List<SkillData>();

    // The real skill list is hidden from the editor
    private List<Skill> _skillList = new List<Skill>();
    public List<Skill> SkillList {get => _skillList; private set => _skillList = value;}

    // *** Functions ***

    // Perform a basic attack against the target Unit
    // Damage formula is currently:
    // damage = a.atk - (b.def * 0.5)
    // Damage defaults to 1 if at 0 or under.
    public virtual void BasicAttack(Unit target)
    {
        int damage = (int)Math.Floor( Math.Max( this.Attack - (target.Defense * 0.5), 1 ));
        target.TakeDamage(damage);
        // Debug log
        UnityEngine.Debug.Log("Unit: " + this.Name + " dealt " + damage + " damage to " + target.Name);
    }

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
    public bool SpendMP(int cost)
    {
        if (cost <= MP)
        {
            MP -= cost;
            return true;
        }
        return false;
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
