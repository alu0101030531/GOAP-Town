using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReturnHome : GAction
{

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("finishWork");
        beliefs.RemoveState("notWorked");
        beliefs.RemoveState("working");
        this.GetComponent<Citizens>().checkWorkTime = false;
        return true;
    }
}

