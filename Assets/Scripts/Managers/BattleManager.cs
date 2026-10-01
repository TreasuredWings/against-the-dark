using UnityEngine;

/*
The BattleManager is responsible for managing turns, recieving inputs from the UI, and telling Units to perform skills.
*/

public enum BATTLE_STATE { START, INPUT, WAIT, VICTORY, DEFEAT }

public class BattleManager : MonoBehaviour
{
    /***************************************
    * Fields *
    ***************************************/

    // Battle stats
    private int roundNumber = 0;

    // Unit references
    private Unit[] allies;
    private Unit[] enemies;

    /***************************************
    * Class functions *
    ***************************************/

    // Flow control
    // StartBattle initializes a battle with an array of party members and enemies
    public void StartBattle()
    {
        
    }

    private void BattleLoop()
    {
        
    }

    // The battle loop functions as such:
    // Each round allows every unit to take a turn
    // At the start of a round, take every unit's current speed, vary them by 20%, and order them from largest to smallest
    // During a round, call the next unit's turn
    // When there are no more turns to call, start another round

    private void Round()
    {
        
    }

    // Continue doing rounds until all enemies or all party members are dead
    
    private void AllyTurn(int allyIndex)
    {
        
    }

    private void EnemyTurn(int enemyIndex)
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
