using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Weight Variables")]
    [SerializeField] private int maxWeight = 50;
    [SerializeField] private int currentWeight = 0;

    // TODO: Create Data Classes
    // TODO: Create list of Loot
    // TODO: Create List of Key Items

    public bool AddLoot(int itemWeight) {
        if (currentWeight + itemWeight > maxWeight || currentWeight == maxWeight) {
            return false;
        }
        
        currentWeight += itemWeight;
        return true;
    }
}
