using UnityEngine;

[CreateAssetMenu(fileName = "StunMod", menuName = "Scriptable Objects/Status Effect/StunMod")]
public class StunMod : StatusEffectBase
{
    [field: SerializeField] public float Change {get; private set;} = 2f;

    public override bool Application(Unit afflicted)
    {
        if (afflicted.GetType() == typeof(Enemy))
        {
            Enemy temp = (Enemy) afflicted;
            temp.StunMod *= Change;
            return true;
        }
        return false;
    }

    public override bool Removal(Unit afflicted)
    {
        if (afflicted.GetType() == typeof(Enemy))
        {
            Enemy temp = (Enemy) afflicted;
            temp.StunMod /= Change;
            return true;
        }
        return false;
    }
}
