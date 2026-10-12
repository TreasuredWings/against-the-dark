using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // *** Properties ***

    public static GameManager Instance { get; private set; }

    [field: SerializeField] public GameObject AllyPrefab { get; private set; }
    [field: SerializeField] public GameObject PartyContainer { get; private set; }
    [field: SerializeField] public List<Ally> Party { get; private set; } = new List<Ally>();
    [field: SerializeField] public Troop TestTroop { get; private set; }

    [field: SerializeField] public List<string> AllyNames {get; private set;} = new List<string> { "Alex", "Victoria", "Sylvie" };
    [field: SerializeField] public List<Sprite> AllySprites {get; private set;} = new List<Sprite>();
    [field: SerializeField] public List<RuntimeAnimatorController> AllyAnimations {get; private set;} = new List<RuntimeAnimatorController>();
    [field: SerializeField] public List<AllyClass> AllyClasses {get; private set;} = new List<AllyClass>();
    [field: SerializeField] public Skill DebugSkill {get; private set;}

    [field: SerializeField] public int BattleNumber { get; private set; } = 0;

    /***************************************
    * Class functions *
    ***************************************/

    // TODO
    public void StartGame()
    {
        // Instantiate ally prefabs
        for (int i = 0; i < 3; i++)
        {
            // Instantiate, parented to PartyContainer
            Vector3 homePos = new Vector3(-4 - i, i - 1, 0);
            int sortingOrder = i*-1+2;
            GameObject tempObj = Instantiate(AllyPrefab, homePos, Quaternion.identity, PartyContainer.transform);
            Ally tempA = tempObj.GetComponent<Ally>();
            // Assign names
            // Assign default sprite
            // Assign animatior override
            // Assign class
            // Any debug skills also add them
            // Set animation home point and draw order
            tempA.InitializeData(AllyNames[i], AllyClasses[i], AllySprites[i], AllyAnimations[i], homePos, sortingOrder);
            tempA.AddSkill(DebugSkill);
            // Finally, add them
            Party.Add(tempA);
        }
        UIManager.Instance.SetStatusWindow();
        // Pull a random troop to start the game
        // Start a battle with those troops
        StartBattle();
    }

    // TODO
    // Start a battle with the chosen troop
    // public void StartBattle(Troop encounter)
    // This will involve spawning their prefabs, assigning their data, initializing their data, and starting the encounter

    public void StartBattle()
    {
        Debug.Log("GameManager: Starting battle!");
        
        BattleManager.Instance.InitializeBattle(Party, TestTroop.EnemyList);
        StartCoroutine(BattleManager.Instance.ProcessTurns());
    }

    // TODO
    public void EndBattle()
    {
        if (BattleManager.Instance.State == BattleState.Defeat)
        {
            // Return the game to the start screen
        }
        else if (BattleManager.Instance.State == BattleState.Victory)
        {
            // Yay!
            // If this is the last battle (BattleNumber 4) go back to the start screen
            if (BattleNumber >= 4)
            {
                // Go back to start
            }
            else
            {
                // Else, show an upgrade screen
                // The upgrade screen needs to wait for input
                // After input, the upgrade screen will tell the game manager to advance the game to the next battle
            }
        }
    }

    // TODO
    public void Progress()
    {
        BattleNumber++;
        if (BattleNumber >= 4)
        {
            // Start a boss
        }
        else
        {
            // Start a normal fight
        }
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
        // FOR DEBUG PURPOSES. REPLACE WITH A START SCREEN'S BUTTON.
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
