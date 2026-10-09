using UnityEngine;

public enum PartyClassType { None, Freelancer, Guardian, Striker, Breaker, Healer, Enhancer, Disruptor }

public class Ally : Unit
{

    [Header("Ally Parameters")]
    [SerializeField] private PartyClassType _class = PartyClassType.Freelancer;

    public PartyClassType Class { get => _class; set => _class = value; }
}
