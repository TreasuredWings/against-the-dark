using UnityEngine;

public class Enemy : Unit
{

    [Header("Enemy Parameters")]
    [SerializeField] private int _stun = 0;
    [SerializeField] private bool _stunState = false;

    public int Stun { get => _stun; set => _stun = value; }
    public bool StunState { get => _stunState; set => _stunState = value; }

    // TakeTurn automates the process of taking a turn for the enemy,
    // calling the correct action function from Unit.
    // Meant to be called by BattleManager to process enemy turns.
    public void TakeTurn()
    {
        Debug.Log("Enemy: " + Name + " is taking a turn.");
    }

    // TODO: OVERRIDE TakeDamage() TO HANDLE STUN AS WELL
}
