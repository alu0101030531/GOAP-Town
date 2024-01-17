using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : GAction
{
    GameObject food;

    public override bool PrePerform()
    {
        if (beliefs.HasState("working")) {
            return false;
        }
        food = inventory.FindItemWithTag("FoodInStore");
        target = this.gameObject;
        if (food == null) {
            beliefs.ModifyState("buyFood", 0);
            return false;
        }
        if (target == null) {
            beliefs.RemoveState("needEat");
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        inventory.RemoveItem(food);
        beliefs.RemoveState("needEat");
        beliefs.ModifyState("throwTrash", 0);
        return true;
    } 
}
