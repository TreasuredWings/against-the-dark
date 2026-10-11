using UnityEngine;
using TMPro;

public class SkillButton : ButtonBase
{
    public Skill AssignedSkill {get; private set;}

    public void SetText(string newText)
    {
        Text.text = newText;
    }

    public void SetSkill(Skill newSkill)
    {
        AssignedSkill = newSkill;
    }

    public override void Clicked()
    {
        InputManager.Instance.SkillButtonClicked(AssignedSkill);
    }
}
