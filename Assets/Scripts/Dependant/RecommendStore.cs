using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecommendStore : GAction
{
    GameObject citizen;
    GameObject food;
    public override bool PrePerform()
    {
        target = this.gameObject;
        citizen = GWorld.Instance.RemoveHungryCitizen();
        if (target == null) {
            return false;
        }

        if (citizen != null) {
            food = citizen.GetComponent<Citizens>().GetChosenFood();
            if (this.GetComponent<Store>().HasFood(food))
                return true;
        }
        GWorld.Instance.AddHungryCitizen(citizen);
        target = null;
        return false;
    }

    public override bool PostPerform()
    {
        GameObject checkOutArea = this.gameObject.transform.Find("Check-Out Area").gameObject;
        GameObject foodInStore = this.GetComponent<Store>().GetFood(food);
        if (foodInStore == null)
            return false;
        citizen.GetComponent<GAgent>().inventory.AddItem(checkOutArea);
        citizen.GetComponent<GAgent>().inventory.AddItem(foodInStore);
        GWorld.Instance.GetWorld().ModifyState("citizenHungry", -1);
        return true;
    }
}
