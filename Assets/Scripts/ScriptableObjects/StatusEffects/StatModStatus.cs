using UnityEngine;

public enum StatMod { OutDamage, InDamage, SpeedMod }

[CreateAssetMenu(fileName = "StatModStatus", menuName = "Scriptable Objects/Status Effect/StatModStatus")]
public class StatModStatus : StatusEffectBase
{
    [field: SerializeField] public StatMod TargetStat {get; private set;} = StatMod.OutDamage;
    [field: SerializeField] public float Change {get; private set;} = 1f;

    public override bool Application(Unit afflicted)
    {
        switch (TargetStat)
        {
            case StatMod.OutDamage:
                afflicted.OutDamageMod *= Change;
                break;
            case StatMod.InDamage:
                afflicted.IncDamageMod *= Change;
                break;
            case StatMod.SpeedMod:
                afflicted.SpeedMod *= Change;
                break;
        }
        return true;
    }

    public override bool Removal(Unit afflicted)
    {
        switch (TargetStat)
        {
            case StatMod.OutDamage:
                afflicted.OutDamageMod /= Change;
                break;
            case StatMod.InDamage:
                afflicted.IncDamageMod /= Change;
                break;
            case StatMod.SpeedMod:
                afflicted.SpeedMod /= Change;
                break;
        }
        return true;
    }
}
