using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        goals.Add(new SubGoal("floorDirty", 1, false), 3); 
        goals.Add(new SubGoal("isWalking", 1, false), 3); 
        Invoke("Misdoing", Random.Range(10, 20));
    }

    void Misdoing() {
        beliefs.ModifyState("getFloorDirty", 0);
        Invoke("Misdoing", Random.Range(10, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
