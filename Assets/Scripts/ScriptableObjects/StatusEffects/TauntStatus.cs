using UnityEngine;

[CreateAssetMenu(fileName = "StatModStatus", menuName = "Scriptable Objects/Status Effect/TauntStatus")]
public class TauntStatus : StatusEffectBase
{
    public override bool Application(Unit afflicted)
    {
        if (afflicted.GetType() == typeof(Ally))
        {
            Ally temp = (Ally)afflicted;
            temp.Taunting = true;
            return true;
        }
        return false;
    }

    public override bool Removal(Unit afflicted)
    {
        if (afflicted.GetType() == typeof(Ally))
        {
            Ally temp = (Ally)afflicted;
            temp.Taunting = false;
            return true;
        }
        return false;
    }
}
