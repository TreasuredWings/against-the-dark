using UnityEngine;

[CreateAssetMenu(fileName = "SkillUpgrade", menuName = "Scriptable Objects/Upgrades/SkillUpgrade")]
public class SkillUpgrade : Upgrade
{
    [field: SerializeField] public Skill SkillToAdd {get; private set;}

    public override bool Application(Unit target)
    {
        target.AddSkill(SkillToAdd);
        return true;
    }
}
