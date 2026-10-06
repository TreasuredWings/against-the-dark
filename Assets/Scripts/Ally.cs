using UnityEngine;



public class Ally : Unit
{

    public enum AllyClass { Freelancer, Guardian, Striker, Healer, Breaker, Enhancer, Disruptor }

    [Header("Ally Parameters")]
    [SerializeField] private AllyClass _class = AllyClass.Freelancer;

    public AllyClass Class { get => _class; set => _class = value; }
}
