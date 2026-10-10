using UnityEngine;
using TMPro;

public class AllyValueStatus : MonoBehaviour
{
    [field: SerializeField] public StatBar Bar {get; private set;}
    [field: SerializeField] public TextMeshProUGUI Text {get; private set;}

    public void SetValues(int max, int value)
    {
        Text.text = value.ToString();
        Bar.SetMaxValue(max);
        Bar.SetValue(value);
    }
}
