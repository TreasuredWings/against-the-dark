using UnityEngine;
using UnityEngine.UI;

public class ActionButton : ButtonBase
{
    [SerializeField] private ActionButtons _buttonType = ActionButtons.Attack;

    public ActionButtons ButtonType { get => _buttonType; private set => _buttonType = value; }

    public override void Clicked()
    {
        InputManager.Instance.ActionButtonClicked(ButtonType);
    }
}
