using System;
using UnityEngine;

public class BaseHoldComponent : MonoBehaviour, IHoldable
{

    bool hoding;

    public bool Holding => throw new NotImplementedException();

    public event Action OnHoldEnd;

    public void Hold()
    {
        OnHold();
    }

    protected virtual void OnHold()
    {
        Debug.Log("잡고 있는 중...");
    }
}
