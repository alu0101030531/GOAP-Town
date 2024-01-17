using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFood : GAction 
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("FoodInStore");
        if (target == null) {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
