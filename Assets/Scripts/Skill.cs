using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Class, Special, Item, Limit }

// Absctract class for a Skill: an object that can be Used to affect targets.
public abstract class Skill : MonoBehaviour
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public SkillType Type { get; private set; }
    public int MPCost { get; private set; }
    public Unit Owner { get; private set; }

    public Skill(string name, string desc, Unit owner, SkillType type, int mpCost)
    {
        Name = name;
        Desc = desc;
        Owner = owner;
        Type = type;
        MPCost = mpCost;
    }

    public bool Use(List<Unit> target)
    {
        // Does nothing by default
        return true;
    }
}
