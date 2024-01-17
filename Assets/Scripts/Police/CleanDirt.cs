using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDirt : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveDirtyFloor();
        if (target == null) {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("floorIsDirty", -1);
        Destroy(target);
        return true; 
    }
}
