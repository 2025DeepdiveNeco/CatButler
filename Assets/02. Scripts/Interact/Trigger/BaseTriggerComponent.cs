using System;
using UnityEngine;

public class BaseTriggerComponent : MonoBehaviour, ITriggerable
{
    [Header("Time")]
    [SerializeField] float duration = 10000f;
    float elapsed = 0;
    bool isTrigger;


    public event Action EndTriggerAction;

    void Update()
    {
        if (elapsed >= duration)
            OffTrigger();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger)
            return;

        isTrigger = true;
         OnTrigger(collision);
    }

    protected virtual void OnTrigger(Collider2D collision)
    {
        StartTime();
    }

    void StartTime()
    {
        elapsed += Time.deltaTime;
    }

    protected virtual void OffTrigger()
    {
        EndTriggerAction?.Invoke();
    }
}
