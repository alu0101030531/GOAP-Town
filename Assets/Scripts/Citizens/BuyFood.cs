using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFood : GAction
{
    private int moneyToBuy;
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("CheckOut");
        GameObject food = inventory.FindItemWithTag("FoodInStore");
        if (target == null || food == null) {
            return false;
        }
        moneyToBuy = food.GetComponent<Food>().GetCost();
        return true;
    }

    public override bool PostPerform()
    {
        inventory.RemoveItem(target);
        this.GetComponent<Citizens>().SetMoney(-moneyToBuy);
        beliefs.RemoveState("buyFood");
        return true;
    } 
}
