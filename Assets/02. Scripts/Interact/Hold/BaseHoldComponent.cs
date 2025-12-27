using System;
using UnityEngine;

public class BaseHoldComponent : BaseInteract, IHoldable
{

    bool holding;
    bool canTouch;

    public bool Holding => holding;

    public bool Touchable() => canTouch = true;

    public bool UnTouchable() => canTouch = false;

    public event Action OnHoldEnd;

    public void Hold()
    {
        if (!canTouch)
            return;

        holding = true;
        OnHoldStart();
    }

    public void HoldUpdate()
    {
        if (!holding)
            return;

        OnHolding();
    }

    public void Release()
    {
        if (!holding)
            return;

        holding = false;
        OnHoldEnd?.Invoke();
        OnHoldReleased();
    }

    protected virtual void OnHoldStart()
    {
        Debug.Log("Hold 시작");
    }

    protected virtual void OnHolding()
    {

    }

    protected virtual void OnHoldReleased()
    {
        Debug.Log("Hold 종료");
    }
}
