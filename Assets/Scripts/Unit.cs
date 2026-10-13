using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.VisualScripting;

// Base class for all Unit-type classes.
public class Unit : MonoBehaviour
{
    // *** Properties ***


    // Dynamic stats
    [Header("Unit Parameters")]
    [field: SerializeField] public SpriteRenderer UnitSprite {get; set;}
    [field: SerializeField] public string Name {get; set;} = "Default Unit";
    [field: SerializeField] public int HPMax {get; set;} = 100;
    [field: SerializeField] public int MPMax {get; set;} = 10;
    [field: SerializeField] public int Attack {get; set;} = 10;
    [field: SerializeField] public int Defense {get; set;} = 10;
    [field: SerializeField] public int Magic {get; set;} = 10;
    [field: SerializeField] public int Resist {get; set;} = 10;
    [field: SerializeField] public int Speed {get; set;} = 10;

    // Dynamic stats
    [field: SerializeField] public int HP {get; protected set;} = 100;
    [field: SerializeField] public int MP {get; protected set;} = 10;

    [field: SerializeField] public float OutDamageMod {get; set;} = 1;
    [field: SerializeField] public float IncDamageMod {get; set;} = 1;
    [field: SerializeField] public float SpeedMod {get; set;} = 1;
    [field: SerializeField] public float SpeedVariance {get; protected set;} = 1;

    [field: SerializeField] public bool IsKnockedOut {get; protected set;} = false;

    [field: SerializeField] public List<float> Affinities { get; protected set; } = new List<float> { 1, 1, 1, 1, 1, 1 };
    [field: SerializeField] public List<StatusEffectBase> AppliedStatuses { get; protected set; } = new List<StatusEffectBase>();
    [field: SerializeField] public List<int> AppliedStatusesDurations {get; protected set;} = new List<int>();
    [field: SerializeField] public List<Skill> SkillList { get; protected set; } = new List<Skill>();

    [field: SerializeField] public UnitAnimator UnitAnim {get; protected set;}

    // *** Functions ***

    // UseSkill: Attempt to use specified Skill on targets.
    // Return: True if skill could be executed, False if requirements were not met/skill was not executed.
    public IEnumerator UseSkill(Skill chosen, List<Unit> targets)
    {
        SpendMP(chosen.MPCost);
        yield return StartCoroutine(chosen.PreEffectAnim(this, targets));
        yield return StartCoroutine(chosen.Effect(this, targets));
        yield return StartCoroutine(chosen.PostEffectAnim(this));
    }

    // Recieve damage, capped at this unit's current hp
    // Returns: this unit's new HP
    public virtual int TakeDamage(int incomingDamage)
    {
        StartCoroutine(UnitAnim.DamageAnim());
        if (incomingDamage > HP)
        {
            HP = 0;
        }
        else
        {
            HP -= incomingDamage;
        }
        if (HP <= HPMax / 3)
        {
            UnitAnim.Anim.SetBool("Low", true);
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
        UnitAnim.Anim.SetBool("Knockout", true);
        for (int i = AppliedStatuses.Count - 1; i >= 0; i--)
        {
            AppliedStatuses[i].Removal(this);
            AppliedStatuses.RemoveAt(i);
        }
    }

    public virtual void Revive()
    {
        IsKnockedOut = false;
        TakeHealing( (int) (HPMax * 0.5f) );
        UnitAnim.Anim.SetBool("Knockout", false);
    }

    public virtual int TakeHealing(int incomingHealing)
    {
        if (IsKnockedOut)
        {
            return 0;
        }

        if (incomingHealing > (HPMax - HP))
        {
            HP = HPMax;
        }
        else
        {
            HP += incomingHealing;
        }
        if (HP > HPMax / 3)
        {
            UnitAnim.Anim.SetBool("Low", false);
            UnitAnim.CheckIdle();
        }
        return HP;
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

    public virtual int GainMP(int gain)
    {
        if (IsKnockedOut)
        {
            return 0;
        }

        if (gain > (MPMax - MP))
        {
            MP = MPMax;
        }
        else
        {
            MP += gain;
        }
        return MP;
    }

    public virtual IEnumerator CheckStatuses(StatusTiming filter)
    {
        for (int i = 0; i < AppliedStatuses.Count; i++)
        {
            StatusEffectBase status = AppliedStatuses[i];
            if (status.Timing == filter)
            {
                status.Trigger(this);
                AppliedStatusesDurations[i]--;
            }
        }
        for (int i = AppliedStatuses.Count - 1; i >= 0; i--)
        {
            if (AppliedStatusesDurations[i] <= 0)
            {
                yield return StartCoroutine(RemoveStatus(i));
            }
        }
    }

    public virtual bool AddStatus(StatusEffectBase newStatus)
    {
        bool refresh = false;
        for (int i = 0; i < AppliedStatuses.Count; i++)
        {
            if (newStatus.Name == AppliedStatuses[i].Name)
            {
                refresh = true;
                AppliedStatusesDurations[i] = newStatus.Duration;
                break;
            }
        }
        if (!refresh)
        {
            AppliedStatuses.Add(newStatus);
            AppliedStatusesDurations.Add(newStatus.Duration);
            newStatus.Application(this);
        }
        UIManager.Instance.CreateEffectText(transform.position, "+" + newStatus.Name, Colors.White);
        return true;
    }

    public virtual IEnumerator RemoveStatus(int index)
    {
        StatusEffectBase status = AppliedStatuses[index];
        UIManager.Instance.CreateEffectText(transform.position, "-" + status.Name, Colors.White);
        status.Removal(this);
        AppliedStatuses.RemoveAt(index);
        AppliedStatusesDurations.RemoveAt(index);
        yield return new WaitForSeconds(0.25f);
    }


    public virtual void AddSkill(Skill newSkill)
    {
        SkillList.Add(newSkill);
    }
    
    public void RollSpeed()
    {
        SpeedVariance = (float) (UnityEngine.Random.value * 0.2 + 0.9);
    }

    // *** Unity Functions ***

}
