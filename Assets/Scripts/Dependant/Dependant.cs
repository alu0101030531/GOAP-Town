using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependant : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        goals.Add(new SubGoal("Recommended", 1, false), 3); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
