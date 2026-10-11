using System.Collections.Generic;
using UnityEngine;

public class ActionWindow : MonoBehaviour
{
    [field: SerializeField] public List<ButtonBase> Buttons { get; private set; } = new List<ButtonBase>();

    void OnEnable()
    {
        Buttons[1].Text.text = InputManager.Instance.ActiveAlly.SkillList[2].Name;
        foreach (ButtonBase btn in Buttons)
        {
            btn.ToggleInteractable(true);
        }
        if (InputManager.Instance.ActiveAlly.MP < InputManager.Instance.ActiveAlly.SkillList[2].MPCost)
        {
            Buttons[1].ToggleInteractable(false);
        }
        Buttons[4].ToggleInteractable(false);
    }

    void OnDisable()
    {
        foreach (ButtonBase btn in Buttons)
        {
            btn.ToggleInteractable(false);
        }
    }
}
