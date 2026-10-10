using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class SkillWindow : MonoBehaviour
{
    [field: SerializeField] public SkillButton[] SkillButtons { get; private set; } = new SkillButton[4];
    [field: SerializeField] public BackButton BackButton { get; private set; }

    void Awake()
    {
        foreach (SkillButton btn in SkillButtons)
        {
            btn.ToggleInteractable(false);
            btn.SetText("");
        }
        BackButton.ToggleInteractable(false);
    }

    // When enabled, this window get the active ally, checks the number of skills they have,
    // then fills as many skill buttons as it can with skills.
    void OnEnable()
    {
        Unit active = InputManager.Instance.ActiveAlly;
        int maxSkills = Math.Min( active.SkillList.Count, 7 );
        for (int i = 3; i < maxSkills; i++)
        {
            SkillButton btn = SkillButtons[i-3];
            Skill sk = active.SkillList[i];
            btn.SetText( sk.Config.Name );
            btn.SetSkill( sk );
            btn.ToggleInteractable(true);
        }
        // Enable the back button
        BackButton.ToggleInteractable(true);
    }

    void OnDisable()
    {
        foreach (SkillButton btn in SkillButtons)
        {
            btn.ToggleInteractable(false);
            btn.SetText("");
        }
        BackButton.ToggleInteractable(false);
    }
}
