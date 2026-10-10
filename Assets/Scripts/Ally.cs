using UnityEngine;



public class Ally : Unit
{

    [Header("Ally Parameters")]
    [SerializeField] private ClassType _class = ClassType.Freelancer;

    public ClassType Class { get => _class; set => _class = value; }
}
