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
    [SerializeField] private List<Unit> _allies;
    [SerializeField] private List<Unit> _enemies;

    // *** Properties ***

    public static BattleManager Instance { get; private set; }
    [field: SerializeField] public GameObject PartyContainer { get; private set; }
    
    [field: SerializeField] public GameObject TroopContainer { get; private set; }
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }

    public BattleState State { get => _state; private set => _state = value; }
    public int RoundNumber { get => _roundNumber; private set => _roundNumber = value; }
    public List<Unit> TurnOrder { get => _turnOrder; private set => _turnOrder = value; }
    public List<Ally> Allies { get; private set; }
    public List<Enemy> Enemies { get; private set; }

    // *** Core Functions ***

    // StartBattle initializes a battle with a list of party members and enemies
    public void InitializeBattle(List<Ally> party, List<EnemyData> troop)
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
        Enemies = new List<Enemy>();
        int i = 0;
        foreach (EnemyData data in troop)
        {
            i++;
            GameObject tempObj = Instantiate(EnemyPrefab, new Vector3(i*2, i - 2, 0), Quaternion.identity, TroopContainer.transform);
            Enemy tempE = tempObj.GetComponent<Enemy>();
            tempE.Data = data;
            tempE.InitializeData();
            tempE.UpdateStatus();
            Enemies.Add(tempE);
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
        foreach (Unit member in Allies)
        {
            TurnOrder.Add(member);
        }
        foreach (Enemy member in Enemies)
        {
            TurnOrder.Add(member);
            if (member.IsStunned)
            {
                member.ResetStun();
            }
        }

        // Sort the turn order by each unit's speed * their speed mod * their speed variance
        foreach (Unit unit in TurnOrder)
        {
            unit.RollSpeed();
        }
        TurnOrder = TurnOrder.OrderByDescending(unit => unit.Speed * unit.SpeedMod * unit.SpeedVariance).ToList();

        string logmsg = "Turn Order: ";
        for (int i = 0; i < TurnOrder.Count; i++)
        {
            Unit temp = TurnOrder[i];
            logmsg += temp.Name + " (" + temp.Speed + ") -> ";
        }
        logmsg += "Round End";
        Debug.Log(logmsg);
    }

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
            if (AllKO(Allies.Cast<Unit>().ToList()))
            {
                // End the battle on a loss
                Debug.Log("BattleManager: DEFEAT!!");
                break;
            }

            // If all enemies are dead, end the battle
            if (AllKO(Enemies.Cast<Unit>().ToList()))
            {
                // End the battle on a win
                Debug.Log("BattleManager: VICTORY!!");
                break;
            }

            // If turn order is empty, start a new round
            if (TurnOrder.Count <= 0)
            {
                StartRound();
            }
            
            // Check if the next turn is for an Ally or Enemy
            if (TurnOrder[0].GetType() == typeof(Enemy) && !TurnOrder[0].IsKnockedOut)
            {
                // Process the enemy's turn by calling it for an action
                Enemy next = (Enemy)TurnOrder[0];
                next.CheckStatuses(StatusTiming.Start);
                next.TakeTurn();
                next.CheckStatuses(StatusTiming.End);
                TurnOrder.RemoveAt(0);
            }
            else if (TurnOrder[0].GetType() == typeof(Ally) && !TurnOrder[0].IsKnockedOut)
            {
                // End this process and wait for input
                State = BattleState.Command;
                InputManager.Instance.ActiveAlly = (Ally)TurnOrder[0];
                InputManager.Instance.ActiveAlly.CheckStatuses(StatusTiming.Start);
                UIManager.Instance.UpdateAllStatusWindows();
                UIManager.Instance.EnterWindow(UIState.Action);
                Debug.Log("BattleManager: " + InputManager.Instance.ActiveAlly.Name + "'s turn!");
            }
            else
            {
                // Skip this person's turn if they're dead
                TurnOrder.RemoveAt(0);
            }
        }
    }

    // InputManager calls InputComplete when the player successfully completed their turn
    public void InputComplete()
    {
        // Go back to waiting, remove the ally from the turn queue, and resume turn processing.
        State = BattleState.Wait;
        InputManager.Instance.ActiveAlly.CheckStatuses(StatusTiming.End);
        TurnOrder.RemoveAt(0);
        ProcessTurns();
    }

    public bool AllKO(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            if (!unit.IsKnockedOut)
            {
                return false;
            }
        }
        return true;
    }

    // TODO
    public void Victory()
    {
        // Show a victory screen
    }

    // TODO
    public void Defeat()
    {
        // Show a defeat screen
    }

    // *** Unity Functions ***

    void Awake()
    {
        Instance = this;
    }
}
