using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TargetWindow : MonoBehaviour
{
    [field: SerializeField] public TargetButton[] TargetButtons { get; private set; } = new TargetButton[4];
    [field: SerializeField] public BackButton BackButton { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (TargetButton btn in TargetButtons)
        {
            btn.ToggleInteractable(false);
            btn.SetText("");
        }
        BackButton.ToggleInteractable(false);
    }

    void OnEnable()
    {
        TargetType targetFilter = InputManager.Instance.ActiveTargeting;
        bool isList = false;
        List<Unit> targetList;
        string singleButtonText = "";
        switch (targetFilter)
        {
            case TargetType.AllEnemies:
                singleButtonText = "All Enemies";
                targetList = BattleManager.Instance.Enemies.Cast<Unit>().ToList();
                break;
            case TargetType.AllAllies:
                singleButtonText = "All Allies";
                targetList = BattleManager.Instance.Allies.Cast<Unit>().ToList();
                break;
            case TargetType.SingleEnemy:
                isList = true;
                targetList = BattleManager.Instance.Enemies.Cast<Unit>().ToList();
                break;
            case TargetType.SingleAlly:
                isList = true;
                targetList = BattleManager.Instance.Allies.Cast<Unit>().ToList();
                break;
            case TargetType.Self:
            default:
                singleButtonText = "Self";
                targetList = new List<Unit> { InputManager.Instance.ActiveAlly };
                break;
        }
        if (isList)
        {
            FillByList(targetList);
        }
        else
        {
            FillTop(targetList, singleButtonText);
        }
    }

    private void FillByList(List<Unit> targets)
    {
        int maxTargets = Math.Min( TargetButtons.Count(), targets.Count() );
        for (int i = 0; i < maxTargets; i++)
        {
            TargetButton btn = TargetButtons[i];
            Unit target = targets[i];
            btn.SetText( target.Name );
            btn.SetTargets( new List<Unit> { target } );
            if (!target.IsKnockedOut)
            {
                btn.ToggleInteractable(true);
            }
        }
        BackButton.ToggleInteractable(true);
    }

    private void FillTop(List<Unit> targets, string text)
    {
        TargetButton btn = TargetButtons[0];
        btn.SetText( text );
        btn.SetTargets( targets );
        btn.ToggleInteractable(true);
        BackButton.ToggleInteractable(true);
    }

    void OnDisable()
    {
        foreach (TargetButton btn in TargetButtons)
        {
            btn.ToggleInteractable(false);
            btn.SetText("");
        }
        BackButton.ToggleInteractable(false);
    }
}
