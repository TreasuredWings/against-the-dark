using System.Runtime.Serialization;
using UnityEngine;

public enum UIState { Wait, Command, Menu, Target }

public class UIManager : MonoBehaviour
{

    // *** Fields ***

    [SerializeField] private UIState _state = UIState.Wait;

    [SerializeField] private GameObject _actionWindow;
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _targetingHUD;

    // *** Properties ***

    public static UIManager Instance { get; private set; }

    public UIState State { get => _state; private set => _state = value; }
    public GameObject ActionWindow { get => _actionWindow; private set => _actionWindow = value; }
    public GameObject MenuWindow { get => _menuWindow; private set => _menuWindow = value; }
    public GameObject TargetingHUD { get => _targetingHUD; private set => _targetingHUD = value; }

    // *** Functions ***

    public void DisplayActions()
    {
        ActionWindow.SetActive(true);
        State = UIState.Command;
    }

    public void DisplayMenu()
    {
        State = UIState.Menu;
    }

    public void HideMenu()
    {
        State = UIState.Command;
    }

    public void EnableTargeting()
    {
        State = UIState.Target;
    }

    public void DisableTargeting()
    {
        State = UIState.Command;
    }

    public void FinalizeCommand()
    {
        State = UIState.Wait;
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
