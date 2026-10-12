using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyAIType { Static, Random, Ordered }

public class Enemy : Unit
{

    [Header("Enemy Data to Pull")]

    [field: SerializeField] public EnemyData Data { get; set; }

    [Header("Enemy Parameters")]

    [field: SerializeField] public int Stun { get; private set; } = 0; // Stun max is 100
    [field: SerializeField] public float StunMod { get; private set; } = 1;
    [field: SerializeField] public bool IsStunned { get; private set; } = false;

    [Header("UI Hooks")]
    [field: SerializeField] public EnemyStatus Status { get; private set; }

    // TakeTurn automates the process of taking a turn for the enemy,
    // calling the correct action function from Unit.
    // Meant to be called by BattleManager to process enemy turns.

    public void InitializeData()
    {
        Name = Data.Name;
        UnitSprite.sprite = Data.EnemySprite;
        HPMax = Data.HPMax;
        HP = HPMax;
        MPMax = Data.MPMax;
        MP = MPMax;
        Attack = Data.Attack;
        Defense = Data.Defense;
        Magic = Data.Magic;
        Resist = Data.Resist;
        Speed = Data.Speed;
        Affinities = new List<float>(Data.Affinities);
        SkillList = new List<Skill>(Data.SkillList);
    }

    public IEnumerator TakeTurn()
    {
        Skill toUse = GetSkill();
        List<Unit> onTargets = GetTargets(toUse);
        Debug.Log(Name + " uses " + toUse.Name);
        yield return StartCoroutine(UseSkill(toUse, onTargets));
    }

    public List<Unit> GetTargets(Skill skill)
    {
        List<Unit> targets;
        int targetIndex;
        switch (skill.Target)
        {
            case TargetType.SingleEnemy:
                targetIndex = UnityEngine.Random.Range(0, BattleManager.Instance.Allies.Count - 1);
                targets = new List<Unit> { BattleManager.Instance.Allies[targetIndex] };
                break;
            case TargetType.SingleAlly:
                targetIndex = UnityEngine.Random.Range(0, BattleManager.Instance.Enemies.Count - 1);
                targets = new List<Unit> { BattleManager.Instance.Enemies[targetIndex] };
                break;
            case TargetType.AllEnemies:
                targets = BattleManager.Instance.Allies.Cast<Unit>().ToList();
                break;
            case TargetType.AllAllies:
                targets = BattleManager.Instance.Enemies.Cast<Unit>().ToList();
                break;
            case TargetType.Self:
            default:
                targets = new List<Unit> { this };
                break;
        }
        return targets;
    }

    public Skill GetSkill()
    {
        Skill chosen = null;
        int index;
        switch (Data.AIType)
        {
            default:
            case EnemyAIType.Static:
                chosen = SkillList[0];
                break;
            case EnemyAIType.Random:
                for (int i = 0; i < SkillList.Count - 1; i++)
                {
                    if (UnityEngine.Random.value <= Data.SkillChance[i])
                    {
                        chosen = SkillList[i];
                    }
                }
                if (chosen == null)
                {
                    chosen = SkillList[SkillList.Count - 1];
                }
                break;
            case EnemyAIType.Ordered:
                index = (BattleManager.Instance.RoundNumber % SkillList.Count) - 1;
                chosen = SkillList[index];
                break;
        }
        return chosen;
    }

    public override int TakeDamage(int incomingDamage)
    {
        int newHP;
        if (IsStunned)
        {
            int stunDamage = (int) Math.Floor(incomingDamage * 1.5);
            UIManager.Instance.CreateEffectText(transform.position, stunDamage.ToString(), Colors.Stun);
            newHP = base.TakeDamage(stunDamage);
        } 
        else
        {
            UIManager.Instance.CreateEffectText(transform.position, incomingDamage.ToString(), Colors.White);
            newHP = base.TakeDamage(incomingDamage);
        }
        UpdateStatus();
        return newHP;
    }

    public override int TakeStun(int incomingStun)
    {
        if (incomingStun > (100 - Stun))
        {
            Stun = 100;
        }
        else
        {
            Stun += incomingStun;
        }
        if (Stun >= 100)
        {
            IsStunned = true;
        }
        UpdateStatus();
        return Stun;
    }

    public void ResetStun()
    {
        IsStunned = false;
        Stun = 0;
    }

    public void UpdateStatus()
    {
        Status.UpdateValues();
    }
}
