using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Weight Variables")]
    [SerializeField] private int maxWeight = 50;
    [SerializeField] private int currentWeight = 0;
    
    // Private Arguments
    [Header("Foreign Components")]
    [SerializeField] private List<Item> itemList;
    [SerializeField] private InventoryUI inventoryUI;

    private void Awake() {
        inventoryUI.SetInventory(this);
    }

    public bool AddLoot(Item item) {
        if (currentWeight + item.weight > maxWeight || currentWeight == maxWeight) {
            return false;
        }

        if (item.IsStackable()) {
            var isItemInInventory = false;

            foreach (var inventoryItem in itemList) {
                if (inventoryItem.entity == item.entity) {
                    inventoryItem.amount += item.amount;
                    isItemInInventory = true;
                }
            }

            if (!isItemInInventory) {
                itemList.Add(item);
            }
        }
        else {
            itemList.Add(item);
        }
        
        currentWeight += item.weight;
        return true;
    }

    public List<Item> GetItemList() {
        return itemList;
    }

    public void RefreshInventory() {
        inventoryUI.OnInventoryChange();
    }
    
    // REMOVE TEMPORARY PUBLIC METHOD
    public int GetInventoryItemCount() {
        return itemList.Count;
    }
}
