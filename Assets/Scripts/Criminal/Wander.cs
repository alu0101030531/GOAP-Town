using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : GAction
{
    private int walkRadius = 20;

    public override bool PrePerform() {
        Vector3 randomDirection = new Vector3(Random.insideUnitSphere.x * walkRadius, 0f, Random.insideUnitSphere.z * walkRadius);
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        target = new GameObject("Agent destination");
        Debug.Log(hit.position);
        target.transform.position = hit.position;

        return true;
    }

    public override bool PostPerform() {
        Destroy(target);
        return true;
    }
}
