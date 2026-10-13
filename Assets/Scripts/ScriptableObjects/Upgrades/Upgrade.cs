using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [field: SerializeField] public string Name {get; private set;} = "Upgrade Name";
    [field: SerializeField] public string Description {get; private set;} = "This is an upgrade description.";
    [field: SerializeField] public ClassType Class {get; private set;} = ClassType.None;

    public abstract bool Application(Unit target);
}
