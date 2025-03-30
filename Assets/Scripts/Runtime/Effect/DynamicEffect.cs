using Management.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicEffect : PoolObject
{
    [SerializeField] private bool isInherit;
    [SerializeField] private Timer timeToPush = new Timer(1f);

    public bool IsInherit => isInherit;
    public override void OnPop()
    {
        timeToPush.Run();
        Execute();
    }

    public override void OnPush()
    {
        Stop();
    }
    public abstract void Execute();
    public abstract void Stop();
    protected virtual void OnExecute()
    {
        if(timeToPush.Check())
            GameInstance.Instance.Pool.Push(this);
    }
    private void LateUpdate()
    {
        OnExecute();
    }


}
