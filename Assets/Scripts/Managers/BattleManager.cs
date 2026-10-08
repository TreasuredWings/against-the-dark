using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

/*
The BattleManager is responsible for managing turns, recieving inputs from the UI, and telling Units to perform skills.
*/

public enum BattleState { None, Start, Command, Wait, Victory, Defeat }

public class BattleManager : MonoBehaviour
{
    // *** Fields ***

    // State
    [SerializeField] private BattleState _state = BattleState.None;

    // Battle stats
    [SerializeField] private int _roundNumber = 0;
    [SerializeField] private List<Unit> _turnOrder;

    // Unit references
    [SerializeField] private List<Ally> _allies;
    [SerializeField] private List<Enemy> _enemies;

    [SerializeField] private Ally _activeAlly;

    // *** Properties ***

    public static BattleManager Instance { get; private set; }

    public BattleState State { get => _state; private set => _state = value; }
    public int RoundNumber { get => _roundNumber; private set => _roundNumber = value; }
    public List<Unit> TurnOrder { get => _turnOrder; private set => _turnOrder = value; }
    public List<Ally> Allies { get => _allies; set => _allies = value; }
    public List<Enemy> Enemies { get => _enemies; set => _enemies = value; }

    public Ally ActiveAlly { get => _activeAlly; private set => _activeAlly = value; }

    // *** Core Functions ***

    // StartBattle initializes a battle with a list of party members and enemies
    public void InitializeBattle(List<Ally> party, List<Enemy> troop)
    {
        // State change
        State = BattleState.Start;
        Debug.Log("BattleManager: Initializing Unit lists");

        // Add all incoming allies
        Allies = new List<Ally>();
        foreach (Ally member in party)
        {
            Allies.Add(member);
        }

        // Add all incoming enemies
        _enemies = new List<Enemy>();
        foreach (Enemy enemy in troop)
        {
            Enemies.Add(enemy);
        }

        // Initialize the TurnOrder Queue
        TurnOrder = new List<Unit>();
        RoundNumber = 0;

        // State change
        State = BattleState.Wait;
    }

    public void StartRound()
    {
        // Increase the round number
        RoundNumber++;
        Debug.Log("BattleManager: Beginning round " + RoundNumber);

        // Put all Units into TurnOrder
        foreach (Ally member in Allies)
        {
            TurnOrder.Add(member);
        }
        foreach (Enemy member in Enemies)
        {
            TurnOrder.Add(member);
        }

        // Sort the turn order by speed
        // TODO: INTRODUCE VARIANCE
        TurnOrder = TurnOrder.OrderByDescending(unit => unit.Speed).ToList();

        string logmsg = "Turn Order: ";
        for (int i = 0; i < TurnOrder.Count; i++)
        {
            Unit temp = TurnOrder[i];
            logmsg += temp.Name + " (" + temp.Speed + ") -> ";
        }
        logmsg += "Round End";
        Debug.Log(logmsg);
    }

    // TODO: CREATE A "ROUND PREVIEW"

    // ProcessTurns runs through turns in the turn order, restarting the round if needed
    // Loop end cases:
    // All allies are knocked out
    // All enemies are knocked out
    // A player turn is reached and we need input
    public void ProcessTurns()
    {
        while (State == BattleState.Wait)
        {
            // If all allies are dead, end the battle
            // TODO: ADD CODE TO CHECK FOR ALL ALLIES DEAD
            // If all enemies are dead, end the battle
            // TODO: ADD CODE TO CHECK FOR ALL ENEMIES DEAD

            // If turn order is empty, start a new round
            if (TurnOrder.Count <= 0)
            {
                StartRound();
            }
            
            // Check if the next turn is for an Ally or Enemy
            if (TurnOrder[0].GetType() == typeof(Enemy))
            {
                // Process the enemy's turn by calling it for an action
                Enemy next = (Enemy)GetNextTurn(true);
                next.TakeTurn();
            }
            else
            {
                // End this process and wait for input
                State = BattleState.Command;
                ActiveAlly = (Ally)GetNextTurn(false);
                UIManager.Instance.ShowWindow(UIState.Action);
                Debug.Log("BattleManager: Awaiting command for " + GetNextTurn(false).Name);
            }
        }
    }

    // Utility function for player input, with options to peek or dequeue
    public Unit GetNextTurn(bool dequeue)
    {
        Unit temp = TurnOrder[0];
        if (dequeue)
        {
            TurnOrder.RemoveAt(0);
        }
        return temp;
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
