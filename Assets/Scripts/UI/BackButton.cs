using UnityEngine;

public class BackButton : ButtonBase
{
    public override void Clicked()
    {
        InputManager.Instance.BackButtonClicked();
    }
}
