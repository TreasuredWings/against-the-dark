using UnityEngine;
using TMPro;

public class EffectPopup : MonoBehaviour
{
    private TextMeshPro Text;
    [field: SerializeField] public float FlySpeed {get; private set;} = 5;
    [field: SerializeField] public float FlyTime {get; private set;} = 1;

    public void OnCreate(string text, Color32 color)
    {
        Text = GetComponent<TextMeshPro>();
        Text.text = text;
        Text.color = color;
    }

    void Update()
    {
        // Make the text fly upwards
        transform.position += new Vector3(0, FlySpeed, 0) * Time.deltaTime;
        // After a specified duration, delete self
        FlyTime -= Time.deltaTime;
        if (FlyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
