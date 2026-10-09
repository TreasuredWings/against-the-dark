using UnityEngine;
using System.Collections.Generic;

public class CommandWindow : MonoBehaviour
{
    private int _buttonYInit = -15;
    private int _buttonYStep = -30;

    [field: SerializeField] public TargetButton Prefab {set; private get;}

    private void SpawnTargetButtons()
    {
        TargetType filter = InputManager.Instance.ActiveTargeting;
        switch (filter)
        {
            case TargetType.Self:
                TargetButton tempBtn = Instantiate(Prefab);
                tempBtn.transform.localPosition = new Vector3(0, _buttonYInit, 0);
                List<Unit> tempTarget = new List<Unit> { InputManager.Instance.ActiveAlly };
                tempBtn.AssignTargets(tempTarget, "Self");
                break;
            case TargetType.SingleEnemy:
                break;
        }
    }

}
