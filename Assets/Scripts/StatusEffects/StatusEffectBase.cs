using UnityEngine;

public enum StatusTiming { Start, End, StartCaster }

public abstract class StatusEffectBase : ScriptableObject
{
    [field: SerializeField] public int Duration {get; private set;} = 0;
    [field: SerializeField] public StatusTiming Timing {get; private set;} = StatusTiming.Start;

    public abstract bool Application(Unit afflicted);

    public abstract bool Trigger(Unit afflicted);

    public abstract bool Removal(Unit afflicted);

    public void DecreaseDuration(int by)
    {
        Duration -= by;
    }
}
