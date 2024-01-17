using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChooseFood : GAction
{
    public GameObject[] foods;
    private GameObject choosenFood;

    public override bool PrePerform()
    {
        target = this.gameObject;
        if (foods == null || target == null) {
            return false;
        }
        choosenFood = foods[Random.Range(0, foods.Length)];
        return true;
    }

    public override bool PostPerform()
    {
        this.GetComponent<Citizens>().SetChosenFood(choosenFood);
        GWorld.Instance.GetWorld().ModifyState("citizenHungry", 1);
        GWorld.Instance.AddHungryCitizen(this.gameObject);
        return true;
    }
}

