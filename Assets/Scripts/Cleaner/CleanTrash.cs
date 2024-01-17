using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTrash : GAction
{
    // Start is called before the first frame update
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveFullTrashCan();
        if (target == null) {
            return false;
        }
       return true; 
    }

    // Update is called once per frame
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("trashCanFull", -1);
        GWorld.Instance.GetWorld().ModifyState("emptyTrashCan", 1);
        GWorld.Instance.AddEmptyTrashCan(target);
       return true; 
    }
}
