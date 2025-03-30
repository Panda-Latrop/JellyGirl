using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTrigger : Trigger
{
    protected override void HandleEnter(Character destructible)
    {
        Debug.Log($"Enter {(destructible).name}");
        base.HandleEnter(destructible);
    }
    protected override void HandleStay(Character destructible)
    {
        Debug.Log($"Stay {(destructible).name}");
        base.HandleStay(destructible);
    }
    protected override void HandleExit(Character destructible)
    {
        Debug.Log($"Exit {(destructible).name}");
        base.HandleExit(destructible);
    }
}
