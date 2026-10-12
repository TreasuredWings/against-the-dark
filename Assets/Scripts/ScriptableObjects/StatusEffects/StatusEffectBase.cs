using UnityEngine;

public enum StatusTiming { Start, End, StartCaster }
public enum StatusType { Positive, Negative, Neutral }

public abstract class StatusEffectBase : ScriptableObject
{
    [field: SerializeField] public string Name {get; private set;} = "Status Effect";
    [field: SerializeField] public string Description {get; private set;} = "This is a status effect.";
    [field: SerializeField] public int Duration {get; private set;} = 3;
    [field: SerializeField] public StatusTiming Timing {get; private set;} = StatusTiming.Start;
    [field: SerializeField] public StatusType Type {get; private set;} = StatusType.Positive;

    public virtual bool Application(Unit afflicted)
    {
        return false;
    }

    public virtual bool Trigger(Unit afflicted)
    {
        return false;
    }

    public virtual bool Removal(Unit afflicted)
    {
        return false;
    }
}
