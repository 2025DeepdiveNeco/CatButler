using UnityEngine;

public class NomalComponent : BaseInteractComponent
{
    protected override void ReduceDurability(int value)
    {
        base.ReduceDurability(value);

        animator.SetInteger("dura", durability);
        Debug.Log(animator.GetInteger("dura"));
    }
}
