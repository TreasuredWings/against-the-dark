using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    private Button _btn;

    public Button Btn { get => _btn; private set => _btn = value; }

    public abstract void Clicked();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Btn = this.GetComponent<Button>();
        Btn.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        Btn.onClick.RemoveListener(Clicked);
    }
}
