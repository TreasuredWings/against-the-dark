using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public enum UIState { Wait, Action, Skill, Target }

public class UIManager : MonoBehaviour
{

    // *** Fields ***

    [SerializeField] private UIState _state = UIState.Wait;
    [SerializeField] private UIState _previous = UIState.Wait;

    // *** Properties ***

    public static UIManager Instance { get; private set; }

    public UIState State
    {
        get => _state;
        private set
        {
            if (_state != value)
            {
                _previous = _state;
            }
            _state = value;
        }
    }
    public UIState Previous { get => _previous; private set => _previous = value; }
    [field: SerializeField] public List<UIState> MenuLayers { get; private set; } = new List<UIState> { UIState.Wait };
    public int MenuNumber { get; private set; } = 0;
    [field: SerializeField] public GameObject ActionWindow { get; private set; }
    [field: SerializeField] public GameObject SkillWindow { get; private set; }
    [field: SerializeField] public GameObject TargetWindow { get; private set; }
    [field: SerializeField] public AllyStatus AllyStatus1 { get; private set; }
    [field: SerializeField] public AllyStatus AllyStatus2 { get; private set; }
    [field: SerializeField] public AllyStatus AllyStatus3 { get; private set; }

    [field: SerializeField] public GameObject EffectTextPrefab { get; private set; }
    [field: SerializeField] public GameObject EffectTextContainer {get; private set;}
    [field: SerializeField] public GameObject DescriptionText {get; private set;}

    // *** Functions ***

    public void ShowWindow()
    {
        ActionWindow.SetActive(false);
        SkillWindow.SetActive(false);
        TargetWindow.SetActive(false);
        switch (MenuLayers[MenuLayers.Count - 1])
        {
            case UIState.Action:
                ActionWindow.SetActive(true);
                break;
            case UIState.Skill:
                SkillWindow.SetActive(true);
                break;
            case UIState.Target:
                TargetWindow.SetActive(true);
                break;
            case UIState.Wait:
                break;
        }
    }

    public void SetStatusWindow()
    {
        AllyStatus1.SetAlly( (Ally)GameManager.Instance.Party[0] );
        AllyStatus2.SetAlly( (Ally)GameManager.Instance.Party[1] );
        AllyStatus3.SetAlly( (Ally)GameManager.Instance.Party[2] );
        UpdateAllStatusWindows();
    }

    public void UpdateAllStatusWindows()
    {
        AllyStatus1.UpdateValues();
        AllyStatus2.UpdateValues();
        AllyStatus3.UpdateValues();
    }

    public void EnterWindow(UIState option)
    {
        MenuLayers.Add(option);
        ShowWindow();
    }

    public void GoBack()
    {
        MenuLayers.RemoveAt(MenuLayers.Count - 1);
        ShowWindow();
    }

    public void CloseWindow()
    {
        for (int i = MenuLayers.Count - 1; i > 0; i--)
        {
            MenuLayers.RemoveAt(i);
        }
        ShowWindow();
    }

    public void CreateEffectText(Vector3 pos, string text, Color32 color)
    {
        GameObject newText = Instantiate(EffectTextPrefab, pos, Quaternion.identity, EffectTextContainer.transform);
        newText.GetComponent<EffectPopup>().OnCreate(text, color);
    }

    public void ShowDescriptionText(string text)
    {
        DescriptionText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void HideDescriptionText()
    {
        DescriptionText.GetComponent<TextMeshProUGUI>().text = "";
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
