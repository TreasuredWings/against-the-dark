using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : ButtonBase, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public ActionButtons ButtonType { get; private set; } = ActionButtons.Attack;

    public override void Clicked()
    {
        InputManager.Instance.ActionButtonClicked(ButtonType);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        string description = "";
        switch (ButtonType)
        {
            case ActionButtons.Attack:
                description = InputManager.Instance.ActiveAlly.SkillList[0].Description;
                break;
            case ActionButtons.Defend:
                description = InputManager.Instance.ActiveAlly.SkillList[1].Description;
                break;
            case ActionButtons.Class:
                description = InputManager.Instance.ActiveAlly.SkillList[2].Description;
                break;
            case ActionButtons.Skill:
                description = "Use a skill that this party member knows.";
                break;
            default:
                break;
        }
        UIManager.Instance.ShowDescriptionText( description );
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UIManager.Instance.HideDescriptionText();
    }
}
