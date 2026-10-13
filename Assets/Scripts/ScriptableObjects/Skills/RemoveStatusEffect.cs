using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoveStatusEffect", menuName = "Scriptable Objects/Skill Effects/RemoveStatusEffect")]
public class RemoveStatusEffect : SkillEffectBase
{
    [field: SerializeField] public StatusType RemovalType {get; private set;} = StatusType.Negative;

    public override bool Use(Unit user, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            if (target.IsKnockedOut)
            {
                continue;
            }
            
            for (int i = 0; i < target.AppliedStatuses.Count; i++)
            {
                if (target.AppliedStatuses[i].Type == RemovalType)
                {
                    target.RemoveStatus(i);
                    break;
                }
            }
        }
        SFXManager.Instance.PlaySFX(7, Vector3.zero, 1);
        return true;
    }
}
