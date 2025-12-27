using UnityEngine;

public class SpikeComponent : BaseTriggerComponent
{
    protected override void OnTrigger(Collider2D collision)
    {
        base.OnTrigger(collision);

        GameManager.Instance.AddScore(-20);
    }
}
