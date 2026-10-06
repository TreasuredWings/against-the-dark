using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /***************************************
    * Fields *
    ***************************************/

    // Unit fields
    [SerializeField] private BattleManager battleM;
    [SerializeField] private List<Ally> _party;
    [Header("Debug")]
    [SerializeField] private List<Enemy> _testTroop;

    public List<Ally> Party { get => _party; private set => _party = value; }
    public List<Enemy> TestTroop { get => _testTroop; private set => _testTroop = value; }

    /***************************************
    * Class functions *
    ***************************************/

    public void StartBattle()
    {
        Debug.Log("GameManager: Starting battle!");
        battleM.InitializeBattle(Party, TestTroop);
        battleM.ProcessTurns();
    }

    /***************************************
    * Unity Functions *
    ***************************************/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
