using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ButtonBase : MonoBehaviour
{
    [field: SerializeField] public Button Btn { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Text {get; private set;}

    public abstract void Clicked();

    public void ToggleInteractable(bool flag)
    {
        Btn.interactable = flag;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Btn = this.GetComponent<Button>();
        Btn.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        Btn.onClick.RemoveListener(Clicked);
    }
}
