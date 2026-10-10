using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

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
            _previous = _state;
            _state = value;
        }
    }
    public UIState Previous { get => _previous; private set => _previous = value; }
    [field: SerializeField] public GameObject ActionWindow { get; private set; }
    [field: SerializeField] public GameObject SkillWindow { get; private set; }
    [field: SerializeField] public GameObject TargetWindow { get; private set; }

    // *** Functions ***

    public void ShowWindow(UIState option)
    {
        ActionWindow.SetActive(false);
        SkillWindow.SetActive(false);
        TargetWindow.SetActive(false);
        switch (option)
        {
            case UIState.Action:
                ActionWindow.SetActive(true);
                State = UIState.Action;
                break;
            case UIState.Skill:
                SkillWindow.SetActive(true);
                State = UIState.Skill;
                break;
            case UIState.Target:
                TargetWindow.SetActive(true);
                State = UIState.Target;
                break;
            case UIState.Wait:
                State = UIState.Wait;
                break;
        }
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
