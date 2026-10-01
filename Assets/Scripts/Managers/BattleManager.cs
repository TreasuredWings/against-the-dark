using System.Collections.Generic;
using UnityEngine;

/*
The BattleManager is responsible for managing turns, recieving inputs from the UI, and telling Units to perform skills.
*/

public enum BATTLE_STATE { START, COMMAND, WAIT, VICTORY, DEFEAT }

public class BattleManager : MonoBehaviour
{
    /***************************************
    * Fields *
    ***************************************/

    // Battle stats
    [SerializeField] private int _roundNumber = 0;
    [SerializeField] private List<Unit> _turnOrder;

    // Unit references
    [SerializeField] private List<Unit> _allies;
    [SerializeField] private List<Unit> _enemies;

    /***************************************
    * Properties *
    ***************************************/

    

    /***************************************
    * Class functions *
    ***************************************/

    // Flow control

    // StartBattle initializes a battle with a list of party members and enemies
    public void StartBattle(Unit[] allies, Unit[] enemies)
    {
        _allies = new List<Unit>();
        foreach (Unit ally in allies)
        {
            _allies.Add(ally);
        }
        _enemies = new List<Unit>();
        foreach (Unit enemy in enemies)
        {
            _enemies.Add(enemy);
        }
        StartRound();
    }

    public void StartRound()
    {
        _roundNumber++;
        Debug.Log("Beginning round " + _roundNumber);
    }

    // ProcessBattle runs through turns in the turn order until a player turn is reached
    private void ProcessBattle()
    {
        
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
