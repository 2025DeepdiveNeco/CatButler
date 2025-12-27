using UnityEngine;

public class ClickComponent : BaseInteractComponent
{
    void OnEnable()
    {
        OnInteractEnd += SetTrigger;
    }

    void OnDisable()
    {
        OnInteractEnd -= SetTrigger;
    }

    void SetTrigger() => animator.SetTrigger("interact");
}
