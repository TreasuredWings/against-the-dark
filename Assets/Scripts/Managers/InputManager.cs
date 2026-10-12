using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;

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
                ActiveTargeting = ActiveSkill.Target;
                UIManager.Instance.EnterWindow(UIState.Target);
                break;
            case ActionButtons.Class:
                ActiveSkill = ActiveAlly.SkillList[2];
                ActiveTargeting = ActiveSkill.Target;
                UIManager.Instance.EnterWindow(UIState.Target);
                break;
            case ActionButtons.Skill:
                UIManager.Instance.EnterWindow(UIState.Skill);
                break;
            case ActionButtons.Defend:
                ActiveSkill = ActiveAlly.SkillList[1];
                ActiveTargeting = ActiveSkill.Target;
                UIManager.Instance.EnterWindow(UIState.Target);
                break;
            case ActionButtons.Item:
                // TODO: maybe.
                break;
            default:
                break;
        }
    }

    // Skill buttons call this to say they've been pressed
    public void SkillButtonClicked(Skill skill)
    {
        ActiveSkill = skill;
        ActiveTargeting = ActiveSkill.Target;
        UIManager.Instance.EnterWindow(UIState.Target);
    }

    // Target buttons call this to say they've been pressed
    // TargetChosen tells the ActiveAlly to use ActiveSkill on a list of targets given by the target buttons
    public void TargetButtonClicked(List<Unit> targets)
    {
        UIManager.Instance.HideDescriptionText();
        UIManager.Instance.CloseWindow();
        StartCoroutine(ActiveAlly.TakeTurn(ActiveSkill, targets));
    }

    public void BackButtonClicked()
    {
        UIManager.Instance.HideDescriptionText();
        UIManager.Instance.GoBack();
    }

    public void VictoryContinueClicked()
    {
        // TODO: Continue game
        GameManager.Instance.StartGame();
    }

    public void DefeatMainMenuClicked()
    {
        // TODO: Properly reset game to start menu
        GameManager.Instance.StartGame();
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
