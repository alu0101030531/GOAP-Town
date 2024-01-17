using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> stores;
    private static Queue<GameObject> hungryCitizens;
    private static Queue<GameObject> trashCans;
    private static Queue<GameObject> fullTrashCan;
    private static Queue<GameObject> dirtyFloors;
    private static Day day;

    static GWorld()
    {
        world = new WorldStates();
        fullTrashCan = new Queue<GameObject>();
        hungryCitizens = new Queue<GameObject>();
        dirtyFloors = new Queue<GameObject>();
        stores = new Queue<GameObject>(GameObject.FindGameObjectsWithTag("Store"));
        trashCans = new Queue<GameObject>(GameObject.FindGameObjectsWithTag("TrashCan"));
        if (stores.Count > 0)
            world.ModifyState("openStore", stores.Count);
        if (trashCans.Count > 0)
            world.ModifyState("emptyTrashCan", trashCans.Count);
    }

    private GWorld()
    {
    }

    public Day GetDay() {
        return day;
    }

    public void SetDay(Day newDay) {
        day = newDay;
    }

    public void AddDirtyFloor(GameObject p) {
        dirtyFloors.Enqueue(p);
    }

    public GameObject RemoveDirtyFloor() {
        if (dirtyFloors.Count == 0) return null;
        return dirtyFloors.Dequeue();
    }
    public void AddFullTrashCan(GameObject p) {
        fullTrashCan.Enqueue(p);
    }

    public GameObject RemoveFullTrashCan() {
        if (fullTrashCan.Count == 0) return null;
        return fullTrashCan.Dequeue();
    }

    public void AddEmptyTrashCan(GameObject p) {
        trashCans.Enqueue(p);
    }

    public GameObject RemoveTrashCan() {
        if (trashCans.Count == 0) return null;
        return trashCans.Dequeue();
    }

    public void AddHungryCitizen(GameObject p)
    {
        hungryCitizens.Enqueue(p);
    }

    public GameObject RemoveHungryCitizen() {
        if (hungryCitizens.Count == 0) return null;
        return hungryCitizens.Dequeue();
    }

    public void AddStore(GameObject p)
    {
        stores.Enqueue(p);
    }

    public GameObject RemoveStore() {
        if (stores.Count == 0) return null;
        return stores.Dequeue();
    }

    public GameObject GetStoreWithSpecificFood(GameObject food) {
        //foreach (GameObject store in stores) {
        //    if (store.GetComponent<Store>().HasFood(food)) {
        //        return store;
        //    }
        //}
        return null;
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
