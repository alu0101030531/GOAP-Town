using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : GAgent
{
    new void Start()
    {
        base.Start();
        goals.Add(new SubGoal("dirtCleaned", 1, false), 3); 
        goals.Add(new SubGoal("isWalking", 1, false), 3); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
