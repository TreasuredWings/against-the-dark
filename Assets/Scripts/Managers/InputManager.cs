using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;

public enum ActionButtons { Attack, Class, Skill, Defend, Item }

// InputManager: Recieved inputs from the buttons and performs the corresponding procedures
public class InputManager : MonoBehaviour
{

    // *** Properties ***

    public static InputManager Instance { get; private set; }

    public Ally ActiveAlly { get; set; }
    public Skill ActiveSkill { get; private set; }
    public TargetType ActiveTargeting { get; private set; }
    
    // *** Functions ***

    // Action buttons call this to say they've been pressed
    public void ActionButtonClicked(ActionButtons action)
    {
        switch (action)
        {
            case ActionButtons.Attack:
                ActiveSkill = ActiveAlly.SkillList[0];
                ActiveTargeting = ActiveSkill.Config.Target;
                UIManager.Instance.ShowWindow(UIState.Target);
                break;
            case ActionButtons.Class:
                ActiveSkill = ActiveAlly.SkillList[2];
                ActiveTargeting = ActiveSkill.Config.Target;
                UIManager.Instance.ShowWindow(UIState.Target);
                break;
            case ActionButtons.Skill:
                UIManager.Instance.ShowWindow(UIState.Skill);
                break;
            case ActionButtons.Defend:
                ActiveSkill = ActiveAlly.SkillList[1];
                ActiveTargeting = ActiveSkill.Config.Target;
                UIManager.Instance.ShowWindow(UIState.Target);
                break;
            case ActionButtons.Item:
                // TODO: MAKE THIS WORK
                break;
            default:
                break;
        }
    }

    // Skill buttons call this to say they've been pressed
    public void SkillButton(int id)
    {
        ActiveSkill = ActiveAlly.SkillList[id];
        ActiveTargeting = ActiveSkill.Config.Target;
        UIManager.Instance.ShowWindow(UIState.Target);
    }

    // Target buttons call this to say they've been pressed
    // TargetChosen tells the ActiveAlly to use ActiveSkill on a list of targets given by the target buttons
    public void TargetButtonClicked(List<Unit> targets)
    {
        if (ActiveAlly.UseSkill(ActiveSkill, targets))
        {
            BattleManager.Instance.InputComplete();
        }
        else
        {
            // Skill could not be used
            UnityEngine.Debug.Log("Skill could not be used");
            UIManager.Instance.ShowWindow(UIState.Target);
        }
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
