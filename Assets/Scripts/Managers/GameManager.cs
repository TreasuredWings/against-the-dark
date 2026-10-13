using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // *** Properties ***

    public static GameManager Instance { get; private set; }

    [field: Header("Object References")]
    [field: SerializeField] public GameObject AllyPrefab { get; private set; }
    [field: SerializeField] public GameObject PartyContainer { get; private set; }

    [field: Header("Party")]
    [field: SerializeField] public List<Ally> Party { get; private set; } = new List<Ally>();

    [field: Header("Initial Party Settings")]
    [field: SerializeField] public List<string> AllyNames {get; private set;} = new List<string> { "Alex", "Victoria", "Sylvie" };
    [field: SerializeField] public List<Sprite> AllySprites {get; private set;} = new List<Sprite>();
    [field: SerializeField] public List<RuntimeAnimatorController> AllyAnimations {get; private set;} = new List<RuntimeAnimatorController>();
    [field: SerializeField] public List<AllyClass> AllyClasses {get; private set;} = new List<AllyClass>();
    [field: SerializeField] public int[] AssignedClasses {get; set;} = {0, 0, 0};
    [field: SerializeField] public Vector3[] PartyPositions {get; private set;} = { new Vector3(-4, 1, 0), new Vector3(-5, 0, 0), new Vector3(-6, -1, 0) };

    [field: Header("Run Settings")]
    [field: SerializeField] public Troop Battle1Troop { get; private set; }
    [field: SerializeField] public List<Troop> TroopPool {get; private set;}
    [field: SerializeField] public Troop BossTroop {get; private set;}
    [field: SerializeField] public int BattleNumber { get; private set; } = 1;

    [field: Header("Debug Settings")]
    [field: SerializeField] public Skill DebugSkill {get; private set;}
    [field: SerializeField] public List<Upgrade> DebugUpgrades {get; private set;} = new List<Upgrade>();

    /***************************************
    * Class functions *
    ***************************************/

    public void StartGame()
    {
        SFXManager.Instance.PlaySFX(13, Vector3.zero, 1);
        UIManager.Instance.StartScreen(false);
        UIManager.Instance.HUDWindows(true);
        // Instantiate ally prefabs
        for (int i = 0; i < 3; i++)
        {
            // Instantiate, parented to PartyContainer
            Vector3 homePos = PartyPositions[i];
            int sortingOrder = i;
            GameObject tempObj = Instantiate(AllyPrefab, homePos, Quaternion.identity, PartyContainer.transform);
            Ally tempA = tempObj.GetComponent<Ally>();
            // Assign names
            // Assign default sprite
            // Assign animatior override
            // Assign class
            // Any debug skills also add them
            // Set animation home point and draw order
            AllyClass chosenClass = AllyClasses[ AssignedClasses[i] ];
            tempA.InitializeData(AllyNames[i], chosenClass, AllySprites[i], AllyAnimations[i], homePos, sortingOrder);
            // Finally, add them
            Party.Add(tempA);
        }
        UIManager.Instance.SetStatusWindow();

        // Debug, add upgrades only to ally 1
        foreach (Upgrade upgrade in DebugUpgrades)
        {
            upgrade.Application(Party[0]);
        }

        UpgradeManager.Instance.InitializePools();

        // Pull a random troop to start the game
        // Start a battle with those troops
        StartBattle(Battle1Troop);
    }

    // Start a battle with the chosen troop
    // public void StartBattle(Troop encounter)
    public void StartBattle(Troop encounter)
    {
        Debug.Log("GameManager: Starting battle!");
        // Maybe play a transition effect
        BattleManager.Instance.InitializeBattle(Party, encounter.EnemyList);
        int music = 0;
        if (BattleNumber >= 10)
        {
            music = 1;
        }
        SFXManager.Instance.PlayMusic(music, 1);
        StartCoroutine(BattleManager.Instance.ProcessTurns());
    }

    public void EndBattle()
    {
        BattleManager.Instance.CleanUpField();
        if (BattleManager.Instance.State == BattleState.Defeat)
        {
            UIManager.Instance.DefeatScreen(false);
            // Return the game to the start screen
            UIManager.Instance.StartScreen(true);
        }
        else if (BattleManager.Instance.State == BattleState.Victory)
        {
            UIManager.Instance.VictoryScreen(false);
            // Heal the party
            foreach (Ally member in Party)
            {
                if (member.IsKnockedOut)
                {
                    member.Revive();
                }
                else
                {
                    member.TakeHealing( (int) (member.HPMax * 0.4f) );
                }
                member.GainMP( (int) (member.MPMax * 0.4f) );
            }
            // If this is the last battle (BattleNumber 10) go back to the start screen
            if (BattleNumber >= 10)
            {
                UIManager.Instance.HUDWindows(false);
                UIManager.Instance.StartScreen(true);
            }
            else
            {
                // Else, show an upgrade screen
                UIManager.Instance.UpgradeScreen(true);
            }
        }
    }

    // TODO
    public void Progress()
    {
        BattleNumber++;
        if (BattleNumber >= 10)
        {
            // Start a boss
            StartBattle(BossTroop);
        }
        else
        {
            // Randomly pick a troop out of the possible list
            // Start that battle
            Troop encounter = TroopPool[ Random.Range(0, TroopPool.Count) ];
            StartBattle(encounter);
        }
    }

    /***************************************
    * Unity Functions *
    ***************************************/

    void Awake()
    {
        Instance = this;
    }
}
