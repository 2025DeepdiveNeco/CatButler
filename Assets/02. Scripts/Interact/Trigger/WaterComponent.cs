using UnityEngine;

public class WaterComponent : BaseTriggerComponent
{
    protected override void OnTrigger(Collider2D collision)
    {
        base.OnTrigger(collision);

        collision.GetComponent<CatMove>().OnWaterStep();
    }
}
