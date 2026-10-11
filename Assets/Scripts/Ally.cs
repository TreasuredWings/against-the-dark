using UnityEngine;



public class Ally : Unit
{

    [Header("Ally Parameters")]
    [SerializeField] private ClassType _class = ClassType.Freelancer;

    public ClassType Class { get => _class; set => _class = value; }

    public override int TakeDamage(int incomingDamage)
    {
        int newHP = base.TakeDamage(incomingDamage);
        UIManager.Instance.UpdateAllStatusWindows();
        return newHP;
    }

    public override bool SpendMP(int cost)
    {
        bool result = base.SpendMP(cost);
        UIManager.Instance.UpdateAllStatusWindows();
        return result;
    }
}
