using System.Collections.Generic;
using UnityEngine;

public class ActionWindow : MonoBehaviour
{
    [field: SerializeField] public List<ButtonBase> Buttons { get; private set; } = new List<ButtonBase>();

    void OnEnable()
    {
        foreach (ButtonBase btn in Buttons)
        {
            btn.ToggleInteractable(true);
        }
    }

    void OnDisable()
    {
        foreach (ButtonBase btn in Buttons)
        {
            btn.ToggleInteractable(false);
        }
    }
}
