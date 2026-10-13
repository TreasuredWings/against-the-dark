using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance {get; private set;}

    // I need references to the possible upgrade pools
    public List<UpgradeDeck> UpgradeDecks {get; private set;} = new List<UpgradeDeck>();
    // I need storage for the three chosen upgrades
    [field: SerializeField] public List<Upgrade> RolledUpgrades {get; private set;} = new List<Upgrade>();
    // I need storage for the current upgrade target
    [field: SerializeField] public List<int> EligibleAllies {get; private set;}
    [field: SerializeField] public int ChosenAlly {get; private set;}
    // I need references to the three button texts
    [field: Header("Object References")]
    [field: SerializeField] public TextMeshProUGUI Header {get; private set;}
    [field: SerializeField] public List<TextMeshProUGUI> ButtonName {get; private set;}
    [field: SerializeField] public List<TextMeshProUGUI> ButtonDesc {get; private set;}

    // Initializes the lists of upgrades that may be chosen at random
    public void InitializePools()
    {
        for (int i = 0; i <= GameManager.Instance.Party.Count - 1; i++)
        {
            EligibleAllies.Add(i);
        }
        for (int i = 0; i <= 2; i++)
        {
            UpgradeDecks.Add( new UpgradeDeck(new List<Upgrade>( GameManager.Instance.Party[i].Class.UpgradePool ) ) );
        }
    }

    // Resets the eligibility list, so allies can be chosen again
    public void ResetEligible()
    {
        for (int i = EligibleAllies.Count - 1; i >= 0; i--)
        {
            EligibleAllies.RemoveAt(i);
        }
        for (int i = 0; i <= GameManager.Instance.Party.Count - 1; i++)
        {
            EligibleAllies.Add(i);
        }
    }

    public void RollUpgrades()
    {
        // Pick an ally, one who hasn't recieved an upgrade yet
        if (EligibleAllies.Count <= 0)
        {
            ResetEligible();
        }
        ChosenAlly = EligibleAllies[ Random.Range(0, EligibleAllies.Count - 1) ];
        UpgradeDecks[ChosenAlly].Shuffle();
        // Pick 3 different upgrades from their class's upgrade pool
        for (int i = 0; i <= 2; i++)
        {
            RolledUpgrades.Add( UpgradeDecks[ChosenAlly].Draw() );
        }
        // Display them all on the buttons
        Header.text = GameManager.Instance.Party[ChosenAlly].Name + " is getting an upgrade!";
        for (int i = 0; i <= RolledUpgrades.Count - 1; i++)
        {
            ButtonName[i].text = RolledUpgrades[i].Name;
            string desc = RolledUpgrades[i].Description;
            if (RolledUpgrades[i].GetType() == typeof(SkillUpgrade))
            {
                SkillUpgrade temp = (SkillUpgrade) RolledUpgrades[i];
                desc = temp.SkillToAdd.Description;
            }
            ButtonDesc[i].text = desc;
        }
    }

    public void ChooseUpgrade(int index)
    {
        // Find the right upgrade that was clicked based on index
        // Apply it to the current upgradee
        RolledUpgrades[index].Application(GameManager.Instance.Party[ChosenAlly]);
        SFXManager.Instance.PlaySFX(12, Vector3.zero, 1);
        // If that upgrade was a StatUpgrade, add it back to the pool
        if (RolledUpgrades[index].GetType() == typeof(StatUpgrade))
        {
            UpgradeDecks[ChosenAlly].Return(RolledUpgrades[index]);
        }
        // Add the other upgrades back into the pool
        for (int i = 0; i <= 2; i++)
        {
            if (i != index)
            {
                UpgradeDecks[ChosenAlly].Return(RolledUpgrades[i]);
            }
        }
        EligibleAllies.Remove(ChosenAlly);
        // Tell UIManager to hide upgrades
        UIManager.Instance.UpgradeScreen(false);
        // Tell GameManager to proceed
        GameManager.Instance.Progress();
    }

    void Awake()
    {
        Instance = this;
    }
}
