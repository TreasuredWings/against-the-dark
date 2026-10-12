using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Skill: an object that can be Used to affect targets.
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    [Header("Base Skill Info")]
    [field: SerializeField] public string Name { get; private set; } = "Skill Name";
    [field: SerializeField] public string Description { get; private set; } = "This is a skill description.";
    // Skill icon sprite?
    
    [field: SerializeField] public SkillType Type { get; private set; } = SkillType.Basic;
    [field: SerializeField] public ClassType AssociatedClass { get; private set; } = ClassType.None;
    [field: SerializeField] public TargetType Target { get; private set; } = TargetType.Self;
    [field: SerializeField] public AnimType AnimationType { get; private set; } = AnimType.Physical;

    [field: SerializeField] public int MPCost { get; private set; } = 0;
    [field: SerializeField] public List<SkillEffectBase> EffectChain { get; private set; } = new List<SkillEffectBase>();

    public IEnumerator Effect(Unit user, List<Unit> targets)
    {
        Debug.Log("Effect start");
        foreach (SkillEffectBase effect in EffectChain)
        {
            effect?.Use(user, targets);
            // Wait half a second between effects
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Effect end");
    }

    public IEnumerator PreEffectAnim(Unit user, List<Unit> targets)
    {
        if (AnimationType == AnimType.Physical)
        {
            // Swing
            Debug.Log("Swing start");
            user.UnitAnim.Anim.SetTrigger("Physical");
            yield return new WaitUntil( () => user.UnitAnim.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 4.0f );
            Debug.Log("Swing end");
        }
        else
        {
            // Chant
            Debug.Log("Chant");
            user.UnitAnim.Anim.SetTrigger("Chant");
            yield return new WaitUntil( () => user.UnitAnim.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 3.0f );
            if (Target == TargetType.SingleEnemy || Target == TargetType.AllEnemies)
            {
                // Magic offense
                Debug.Log("Magic offense");
                user.UnitAnim.Anim.SetTrigger("MagicOffense");
                yield return new WaitUntil( () => user.UnitAnim.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f );
            }
            else
            {
                // Magic defense
                Debug.Log("Magic defense");
                user.UnitAnim.Anim.SetTrigger("MagicDefense");
                yield return new WaitUntil( () => user.UnitAnim.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f );
            }
            Debug.Log("Magic end");
        }
        
    }

    public IEnumerator PostEffectAnim(Unit user)
    {
        Debug.Log("Start return");
        user.UnitAnim.Anim.SetTrigger("Neutral");
        while (user.transform.position != user.UnitAnim.HomePoint)
        {
            float speed = user.UnitAnim.DashSpeed;
            Vector3 homePoint = user.UnitAnim.HomePoint;
            float step = speed * Time.deltaTime;
            user.transform.position = Vector3.MoveTowards(user.transform.position, homePoint, step);
            if (Vector3.Distance(user.transform.position, homePoint) < 0.001f)
            {
                user.transform.position = homePoint;
            }
            yield return new WaitForNextFrameUnit();
        }
        Debug.Log("End return");
    }
}
