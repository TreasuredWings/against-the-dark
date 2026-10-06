using System.Collections.Generic;
using UnityEngine;

/*
The BattleManager is responsible for managing turns, recieving inputs from the UI, and telling Units to perform skills.
*/

public enum BattleState { NONE, START, COMMAND, WAIT, VICTORY, DEFEAT }

public class BattleManager : MonoBehaviour
{
    /***************************************
    * Fields *
    ***************************************/

    [SerializeField] private BattleState _state = BattleState.NONE;

    // Battle stats
    [SerializeField] private int _roundNumber = 0;
    [SerializeField] private List<int> _turnOrder;

    // Unit references
    [SerializeField] private List<Unit> _allies;
    [SerializeField] private List<Unit> _enemies;

    /***************************************
    * Properties *
    ***************************************/

    public BattleState State { get => _state; private set => _state = value; }
    public int RoundNumber { get => _roundNumber; private set => _roundNumber = value; }
    public List<int> TurnOrder { get => _turnOrder; private set => _turnOrder = value; }
    public List<Unit> Allies { get => _allies; set => _allies = value; }
    public List<Unit> Enemies { get => _enemies; set => _enemies = value; }

    /***************************************
    * Class functions *
    ***************************************/

    // Flow control

    // StartBattle initializes a battle with a list of party members and enemies
    public void StartBattle(Unit[] newAllies, Unit[] newEnemies)
    {
        State = BattleState.START;
        Allies = new List<Unit>();
        foreach (Unit ally in newAllies)
        {
            Allies.Add(ally);
        }
        _enemies = new List<Unit>();
        foreach (Unit enemy in newEnemies)
        {
            Enemies.Add(enemy);
        }
        ProcessTurns();
    }

    public void StartRound()
    {
        RoundNumber++;
        Debug.Log("Beginning round " + RoundNumber);
        // Put all Units into TurnOrder sorted by speed

    }

    // ProcessTurns runs through turns in the turn order, restarting the round if needed
    // Loop end cases:
    // All allies are knocked out
    // All enemies are knocked out
    // A player turn is reached and we need input
    private void ProcessTurns()
    {
        // If all allies are dead, end the battle
        // If all enemies are dead, end the battle
        // If turn order is empty, start a new round
        // Start calling turns
        // If the next turn is a player turn, end this process and wait for input
        // Else, process the enemy's turn by calling their AI for an action
        // Go back to the start
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
