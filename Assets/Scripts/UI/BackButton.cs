using UnityEngine;

public class BackButton : ButtonBase
{
    public override void Clicked()
    {
        SFXManager.Instance.PlaySFX(2, Vector3.zero, 1);
        InputManager.Instance.BackButtonClicked();
    }
}
