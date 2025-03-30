using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCondition : MonoBehaviour
{
    public event System.Action<int> OnChange;
    public abstract int Current { get; }
    public abstract int Max { get; }
    public abstract bool Check();
    protected void Execute()
    {
        OnChange?.Invoke(Current);
    }
}
