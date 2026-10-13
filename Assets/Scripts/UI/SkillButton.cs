using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillButton : ButtonBase, IPointerEnterHandler, IPointerExitHandler
{
    public Skill AssignedSkill {get; private set;} = null;

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
        SFXManager.Instance.PlaySFX(1, Vector3.zero, 1);
        InputManager.Instance.SkillButtonClicked(AssignedSkill);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (AssignedSkill != null)
        {
            UIManager.Instance.ShowDescriptionText( AssignedSkill.Description );
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UIManager.Instance.HideDescriptionText();
    }
}
