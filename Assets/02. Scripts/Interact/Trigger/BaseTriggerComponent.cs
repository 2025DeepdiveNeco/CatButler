using System;
using UnityEngine;

public class BaseTriggerComponent : MonoBehaviour, ITriggerable
{
    [Header("Time")]
    [SerializeField] float duration = 10000f;
    float elapsed = 0;


    public event Action EndTriggerAction;

    void Update()
    {
        if (elapsed >= duration)
            OffTrigger();
    }

    void OnTriggerEnter2D(Collider2D collision) => OnTrigger(collision);

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
