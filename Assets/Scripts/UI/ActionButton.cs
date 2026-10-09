using UnityEngine;
using UnityEngine.UI;

public class ActionButton : ButtonBase
{
    [field: SerializeField] public ActionButtons ButtonType { get; private set; } = ActionButtons.Attack;

    public override void Clicked()
    {
        InputManager.Instance.ActionButtonClicked(ButtonType);
    }
}
