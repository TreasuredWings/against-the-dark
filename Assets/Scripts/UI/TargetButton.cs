using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetButton : ButtonBase
{
    [field: SerializeField] public List<Unit> Targets {get; private set;} = new List<Unit>();

    public void SetText(string newText)
    {
        Text.text = newText;
    }

    public void SetTargets(List<Unit> newTargets)
    {
        Targets = newTargets;
    }

    public void AssignTargets(List<Unit> newTargets, string newText)
    {
        Targets = newTargets;
        Btn.GetComponentInChildren<Text>().text = newText;
    }

    public override void Clicked()
    {
        InputManager.Instance.TargetButtonClicked(Targets);
    }
}
