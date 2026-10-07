using System.Runtime.Serialization;
using UnityEngine;

public enum UIState { Wait, Command, Menu, Target }

public class UIManager : MonoBehaviour
{

    [SerializeField] private BattleManager battleM;
    [SerializeField] private GameObject actionWindow;
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject targetingHUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
