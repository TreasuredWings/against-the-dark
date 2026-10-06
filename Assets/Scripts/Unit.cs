using System.Runtime;
using System;
using UnityEngine;
using System.Diagnostics;

// Base class for all Unit-type classes.
public class Unit : MonoBehaviour
{
    /***************************************
    * Fields *
    ***************************************/

    // Unit stats
    [SerializeField] private string _name = "Default";
    [SerializeField] private int _level = 1;
    [SerializeField] private int _hp = 100;
    [SerializeField] private int _hpMax = 100;
    [SerializeField] private int _mp = 10;
    [SerializeField] private int _mpMax = 10;
    [SerializeField] private int _attack = 5;
    [SerializeField] private int _defense = 5;
    [SerializeField] private int _magic = 5;
    [SerializeField] private int _resist = 5;
    [SerializeField] private int _speed = 5;

    // Statuses
    [SerializeField] private bool _ally = false;
    [SerializeField] private bool _knockout = false;

    // In battle stats
    

    /***************************************
    * Properties *
    ***************************************/
    
    // Read-write properties
    // HP/MP must stay within bounds 0 < value < HP/MP max
    public int HP
    {
        get => _hp;
        set
        {
            if (value > _hpMax)
            {
                _hp = _hpMax;
            }
            else if (value < 0)
            {
                _hp = 0;
            }
            else
            {
                _hp = value;
            }
        }
    }

    public int MP
    {
        get => _mp;
        set
        {
            if (value > _mpMax)
            {
                _mp = _mpMax;
            }
            else if (value < 0)
            {
                _mp = 0;
            }
            else
            {
                _mp = value;
            }
        }
    }

    // Read-only properties
    public string Name => _name;
    public int Level => _level;
    public int HPMax => _hpMax;
    public int MPMax => _mpMax;
    public int Attack => _attack;
    public int Defense => _defense;
    public int Magic => _magic;
    public int Resist => _resist;
    public int Speed => _speed;

    public bool IsAlly => _ally;
    public bool IsKnockedOut => _knockout;

    /***************************************
    * Class Functions *
    ***************************************/

    // Perform a basic attack against the target Unit
    // Damage formula is currently:
    // damage = a.atk - (b.def * 0.5)
    // Damage defaults to 1 if at 0 or under.
    public void BasicAttack(Unit target)
    {
        int damage = (int)Math.Floor( Math.Max( this.Attack - (target.Defense * 0.5), 1 ));
        target.TakeDamage(damage);
        UnityEngine.Debug.Log(this.Name + " dealt " + damage + " damage to " + target.Name);
    }

    // Recieve damage, capped at this unit's current hp (handled in property)
    public void TakeDamage(int incomingDamage)
    {
        HP -= incomingDamage;
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
