using System.Runtime;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;

// Base class for all Unit-type classes.
public class Unit : MonoBehaviour
{
    
    // *** Fields ***

    [Header("Unit Parameters")]

    // Unit stats
    [SerializeField] private string _name = "Unitman";
    [SerializeField] private int _level = 1;
    [SerializeField] private int _hp = 100;
    [SerializeField] private int _hpMax = 100;
    [SerializeField] private int _mp = 10;
    [SerializeField] private int _mpMax = 10;
    [SerializeField] private int _attack = 5;
    [SerializeField] private int _defense = 5;
    [SerializeField] private int _magic = 5;
    [SerializeField] private int _resist = 5;
    [SerializeField] private int _speed = 5;

    [SerializeField] private List<Skill> _skillList = new List<Skill>();

    // Statuses
    [SerializeField] private bool _knockout = false;

    // In battle stats
    

    // *** Properties ***
    
    // Read-write properties
    public int HP {get => _hp; set => _hp = value;}
    public int MP {get => _mp; set => _mp = value;}

    // Read-only properties
    public string Name {get => _name; private set => _name = value;}
    public int Level {get => _level; private set => _level = value;}
    public int HPMax {get => _hpMax; private set => _hpMax = value;}
    public int MPMax {get => _mpMax; private set => _mpMax = value;}
    public int Attack {get => _attack; private set => _attack = value;}
    public int Defense {get => _defense; private set => _defense = value;}
    public int Magic {get => _magic; private set => _magic = value;}
    public int Resist {get => _resist; private set => _resist = value;}
    public int Speed {get => _speed; private set => _speed = value;}

    public List<Skill> SkillList {get => _skillList; private set => _skillList = value;}

    public bool IsKnockedOut {get => _knockout; private set => _knockout = value;}

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
        int mpCost = chosen.MPCost;
        if (!SpendMP(mpCost))
        {
            return false;
        }
        if (!chosen.Use(targets))
        {
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


}
