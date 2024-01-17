using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrash : GAction
{
    public override bool PrePerform() {
        target = GWorld.Instance.RemoveTrashCan();
        if (target == null) {
            return false;
        }
        return true;
    }

    public override bool PostPerform() {
        GWorld.Instance.GetWorld().ModifyState("emptyTrashCan", -1);
        GWorld.Instance.GetWorld().ModifyState("trashCanFull", 1);
        GWorld.Instance.AddFullTrashCan(target);
        beliefs.RemoveState("throwTrash");
        return true;
    }
}
