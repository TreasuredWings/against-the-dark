using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffectBase : ScriptableObject
{
    public abstract bool Use(Unit user, List<Unit> targets);
}