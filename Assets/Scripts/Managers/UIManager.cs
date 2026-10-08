using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum UIState { Wait, Action, Skill, Target }

public class UIManager : MonoBehaviour
{

    // *** Fields ***

    [SerializeField] private UIState _state = UIState.Wait;
    [SerializeField] private UIState _previous = UIState.Wait;

    [SerializeField] private GameObject _actionWindow;
    [SerializeField] private GameObject _skillWindow;
    [SerializeField] private GameObject _targetWindow;

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
    public GameObject ActionWindow { get => _actionWindow; private set => _actionWindow = value; }
    public GameObject SkillWindow { get => _skillWindow; private set => _skillWindow = value; }
    public GameObject TargetWindow { get => _targetWindow; private set => _targetWindow = value; }

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
                PopulateSkillWindow(SkillType.Special);
                State = UIState.Skill;
                break;
            case UIState.Target:
                ActionWindow.SetActive(true);
                // TODO: Add functionality to populate TargetWindow with the correct targets
                State = UIState.Target;
                break;
            case UIState.Wait:
                State = UIState.Wait;
                break;
        }
    }

    private void PopulateSkillWindow(SkillType filter)
    {
        List<Skill> raw = InputManager.Instance.ActiveAlly.SkillList;
        List<Skill> filtered = new List<Skill>();
        foreach (Skill s in raw)
        {
            if (s.Type == filter)
            {
                filtered.Add(s);
            }
        }
        foreach (Skill s in filtered)
        {
            // Spawn a button, position it, draw the correct name, give it the correct OnClick function
        }
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
