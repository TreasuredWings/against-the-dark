using UnityEngine;
using TMPro;

public class SkillButton : ButtonBase
{
    [field: SerializeField] public TextMeshProUGUI text {get; private set;}
    public Skill AssignedSkill {get; private set;}

    public void SetText(string newText)
    {
        text.text = newText;
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
