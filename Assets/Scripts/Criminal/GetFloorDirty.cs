using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFloorDirty : GAction
{
    public GameObject dirtPrefab;
    // Start is called before the first frame update
    public override bool PrePerform()
    {

        target = this.gameObject;
        if (target == null) 
            return false;
        return true;
    }

    // Update is called once per frame
    public override bool PostPerform()
    {
        Vector3 location = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        GameObject dirt = Instantiate(dirtPrefab, location, Quaternion.identity);
        GWorld.Instance.AddDirtyFloor(dirt);
        GWorld.Instance.GetWorld().ModifyState("floorIsDirty", 1);
        beliefs.RemoveState("getFloorDirty");
        return true; 
    }
}
