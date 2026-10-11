using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [field: SerializeField] public Enemy AssignedEnemy {get; private set;}
    [field: SerializeField] public StatBar HPDisplay {get; private set;}
    [field: SerializeField] public StatBar StunDisplay {get; private set;}

    public void SetEnemy(Enemy newEnemy)
    {
        AssignedEnemy = newEnemy;
    }

    public void UpdateValues()
    {
        if (AssignedEnemy != null)
        {
            HPDisplay.SetMaxValue(AssignedEnemy.HPMax);
            HPDisplay.SetValue(AssignedEnemy.HP);
            StunDisplay.SetMaxValue(100);
            StunDisplay.SetValue(AssignedEnemy.Stun);
        }
    }
}
