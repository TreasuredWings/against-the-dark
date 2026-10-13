using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [field: SerializeField] public Slider Slider {get; private set;}

    public void SetMaxValue(int newMaxValue)
    {
        Slider.maxValue = newMaxValue;
    }

    public void SetValue(int newValue)
    {
        Slider.value = newValue;
    }
}
