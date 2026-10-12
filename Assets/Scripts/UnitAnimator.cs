using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [field: SerializeField] public Animator Anim {get; private set;}
    [field: SerializeField] public Vector3 HomePoint {get; set;} = new Vector3(0, 0, 0);
    [field: SerializeField] public float ForwardDistance {get; private set;} = 20f;
    [field: SerializeField] public float DashSpeed {get; private set;} = 20f;

    public IEnumerator StepForward()
    {
        Vector3 destination = new Vector3(transform.position.x + ForwardDistance, transform.position.y, 0);
        while (transform.position != destination) {
            float step = DashSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                transform.position = destination;
            }
            yield return new WaitForNextFrameUnit();
        }
    }

    public void CheckIdle()
    {
        Anim.SetTrigger("Neutral");
    }

    public IEnumerator DamageAnim()
    {
        Anim.SetTrigger("Damage");
        yield return null;
        yield return new WaitUntil( () => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f );
        Anim.SetTrigger("Neutral");
    }
}
