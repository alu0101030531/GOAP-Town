using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        goals.Add(new SubGoal("cleanedTrashCan", 1, false), 3);   
        goals.Add(new SubGoal("isWalking", 1, false), 3);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
