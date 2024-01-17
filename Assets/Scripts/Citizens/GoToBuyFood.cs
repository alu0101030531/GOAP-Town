using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBuyFood : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("CheckOut");
        GameObject food = inventory.FindItemWithTag("FoodInStore");
        if (target == null || food == null) {
            return false;
        }
        if (food.GetComponent<Food>().GetCost() > this.GetComponent<Citizens>().GetMoney()) {
            beliefs.RemoveState("buyFood");
            return false; 
        }
        
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
