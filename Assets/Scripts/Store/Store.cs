using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Store : MonoBehaviour
{
    public GameObject[] itemsGameObject;
    public int[] itemsCount;
    private Dictionary<GameObject, int> items;
    // Start is called before the first frame update
    void Start()
    {
        items = new Dictionary<GameObject, int>();
        foreach (var item in itemsGameObject.Zip(itemsCount, (i, c) => new {gameObject = i, count = c})) {
            items.Add(item.gameObject, item.count);
        }
    }

    public bool HasFood(GameObject food) {
        if (food == null) {
            return false;
        }
        foreach (var item in items) {
            if (item.Key.name == food.name) {
                return true;
            }
        }
        return false;
    }

    public GameObject GetFood(GameObject food) {
        foreach (var item in items) {
            if (item.Key.name == food.name) {
                return item.Key;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
