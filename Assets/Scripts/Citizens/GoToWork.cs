using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float GetTime();

public class GoToWork: GAction
{
    public static GetTime OnGetTime;
    public int workMoney = 100;

    public override bool PrePerform()
    {
        if (OnGetTime == null) {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        float time = OnGetTime();
        this.GetComponent<Citizens>().SetStartingWorkingTime(time);
        this.GetComponent<Citizens>().checkWorkTime = true;
        this.GetComponent<Citizens>().money += workMoney;
        beliefs.ModifyState("working", 0);
        return true;
    }
}

