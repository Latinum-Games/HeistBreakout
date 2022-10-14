using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Weight Variables")]
    [SerializeField] private int maxWeight = 50;
    [SerializeField] private int currentWeight = 0;

    // Private Arguments
    [SerializeField] private List<Item> itemList;
    
    // TODO: CREATE UI TO DISPLAY THE ITEMS
    // TODO: CREATE A FUNCTION TO DROP THE ITEMS

    public bool AddLoot(Item item) {
        if (currentWeight + item.weight > maxWeight || currentWeight == maxWeight) {
            return false;
        }

        itemList.Add(item);
        currentWeight += item.weight;
        return true;
    }
}
