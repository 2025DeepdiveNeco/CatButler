using UnityEngine;

public class DontTouchComponent : BaseInteractComponent
{
    protected override void OnInteract()
    {
        base.OnInteract();
        GameManager.Instance.CheckGameOver();
    }
}
