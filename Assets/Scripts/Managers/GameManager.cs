using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // *** Fields ***

    [SerializeField] private List<Unit> _party;

    [Header("Debug")]
    [SerializeField] private List<Unit> _testTroop;

    // *** Properties ***

    public static GameManager Instance { get; private set; }

    public List<Unit> Party { get => _party; private set => _party = value; }
    public List<Unit> TestTroop { get => _testTroop; private set => _testTroop = value; }

    /***************************************
    * Class functions *
    ***************************************/

    public void StartBattle()
    {
        Debug.Log("GameManager: Starting battle!");
        BattleManager.Instance.InitializeBattle(Party, TestTroop);
        BattleManager.Instance.ProcessTurns();
    }

    /***************************************
    * Unity Functions *
    ***************************************/

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.SetStatusWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
